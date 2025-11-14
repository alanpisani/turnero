using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Turnero.Common.Enums;
using Turnero.Common.Extensions;
using Turnero.Domain.TurnoDomain;
using Turnero.Dto;
using Turnero.Dto.TurnoDto;
using Turnero.Exceptions;
using Turnero.Exceptions.Turno;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
    public class TurnoService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ResponseDto<TurnoResponseDto>> SolicitarTurno(TurnoRequestDto dto, bool usarTransaccionExistente = false)
		{
			// Validar la lógica de negocio
			var result = await new CreateTurnoDomain(_unitOfWork).ValidarLogicaNegocio(dto);

			if (!result.EsValido)
				throw new BussinessErrorContentException(result.Errores);

			// Solo crear una nueva transacción si no hay una ya activa
			if (!usarTransaccionExistente)
				await _unitOfWork.BeginTransactionAsync();

			try
			{
				// Crear y guardar el turno
				var turnoSolicitado = TurnoMapper.DeTurnoDtoATurno(dto);
				await _unitOfWork.Turnos.AddTurno(turnoSolicitado);
				await _unitOfWork.CompleteAsync();

				// Buscar el turno con datos completos (profesional, especialidad, etc.)
				var turnoRenovado = await _unitOfWork.Turnos.FindOrDefaultTurno(turnoSolicitado.IdTurno);

				// cerramos acá si no estamos dentro de una transacción externa
				
				if (!usarTransaccionExistente)
					await _unitOfWork.CommitAsync();

				return new ResponseDto<TurnoResponseDto>
				{
					Success = true,
					Message = "Turno solicitado correctamente",
					Data = TurnoMapper.DeTurnoADto(turnoRenovado!)
				};
			}
			catch (Exception e)
			{
				if (!usarTransaccionExistente)
					await _unitOfWork.RollbackAsync();

				throw new Exception($"Error al solicitar turno. Inténtelo más tarde. Error: {e.Message}");
			}
		}


		public async Task<ResponseDto<TurnoResponseDto>> SolicitarTurnoRapido(TurnoRapidoRequestDto dto)
		{
			var result = await new CreateTurnoDomain(_unitOfWork).ValidarLogicaNegocio(dto); //Validador de logica de negocio

			if (!result.EsValido) throw new BussinessErrorContentException(result.Errores);

			var pacienteInDb = await _unitOfWork.Pacientes.GetPacienteWithDni(dto.DniPaciente);

			if (pacienteInDb == null) throw new NotFoundException($"No se encontró el paciente con dni: {dto.DniPaciente}");

			var requestDto = TurnoMapper.DeRapidoRequestARequestDto(dto, pacienteInDb.IdUsuario);

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				Turno turnoSolicitado = TurnoMapper.DeTurnoDtoATurno(requestDto); //Mapeo de dto a Turno model

				await _unitOfWork.Turnos.AddTurno(turnoSolicitado); //Lo añado a la bd

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();


				//Para que traiga los datos de la especialidad y del profesional

				var turnoRenovado = await _unitOfWork.Turnos.FindOrDefaultTurno(turnoSolicitado.IdTurno);

				return new ResponseDto<TurnoResponseDto>
				{
					Success = true,
					Message = "Turno solicitado correctamente",
					Data = TurnoMapper.DeTurnoADto(turnoRenovado!),
				};
			}
			catch (Exception e)
			{
				await _unitOfWork.RollbackAsync(); //Volvamos para atrás, muchachos. Algo salio mal y cancelamos los cambios a la bd

				throw new Exception($"Error al solicitar turno. Inténtelo más tarde. Error: {e}");
			}
		}

		public async Task<ResponseDto<TurnoResponseDto>> ModificarEstadoTurno(int idTurno, ModificarEstadoTurnoDto dto) //TODO: DESARROLLAR
		{
			Turno? turno = await _unitOfWork.Turnos.FindOrDefaultTurno(idTurno);

			if (turno == null) throw new NotFoundException($"No se encontró el turno con el ID: {idTurno}");

			var isRecepcionista = await _unitOfWork.Usuarios.AnyRecepcionistaByDni(dto.DniDelCancelador);

			if (turno.IdPacienteNavigation.Dni != dto.DniDelCancelador && !isRecepcionista)
				throw new ForbiddenException("No puede modificar turnos de otro paciente.");

			turno!.EstadoTurno = dto.NuevoEstado;

			_unitOfWork.Turnos.Actualizar(turno);

			await _unitOfWork.CompleteAsync();

			return new ResponseDto<TurnoResponseDto>
			{
				Success = true,
				Message = "Turno modificado correctamente",
				Data = TurnoMapper.DeTurnoADto(turno)
			};
		}

		public async Task<ResponseDto<List<TurnoResponseDto>>> TraerTurnosDelPaciente(int idPaciente)
		{
			bool hayPaciente = await _unitOfWork.Pacientes.AnyPaciente(idPaciente);

			if (!hayPaciente) throw new NotFoundException($"No hay paciente registrado con el ID: {idPaciente}");

			var turnos = await _unitOfWork.Turnos.GetTurnosByPaciente(idPaciente);

			if(turnos == null || turnos.Count == 0) throw new NoTurnoException("No tenés turnos reservados");

			var turnosDto = new List<TurnoResponseDto>();

			foreach (Turno turno in turnos)
			{
				var turnoDto = TurnoMapper.DeTurnoADto(turno);
				turnosDto.Add(turnoDto);
			}

			return new ResponseDto<List<TurnoResponseDto>>
			{
				Success = true,
				Message = "Turnos consultados correctamente",
				Data = turnosDto
			};
		}

		public async Task<ResponseDto<List<TurnoResponseDto>>> TraerTurnosDelPacientePorDni(int dniPaciente)
		{
			var pacienteInDb = await _unitOfWork.Pacientes.GetPacienteByDni(dniPaciente);

			if (pacienteInDb == null) throw new NotFoundException($"No se ha encontrado un paciente con el dni {dniPaciente}. Por favor, verificá que hayas ingresado el DNI correctamente.");

			var turnos = await _unitOfWork.Turnos.GetTurnosByPaciente(pacienteInDb.IdUsuario);

			if (turnos == null || turnos.Count == 0) throw new NoTurnoException($"Hola, {pacienteInDb.IdUsuarioNavigation.Nombre}. No tenés turnos reservados");


			var turnosDto = new List<TurnoResponseDto>();

			foreach(Turno turno in turnos)
			{
				var turnoDto = TurnoMapper.DeTurnoADto(turno);
				turnosDto.Add(turnoDto);
			}


			return new ResponseDto<List<TurnoResponseDto>>
			{
				Success = true,
				Message= $"¡Hola {pacienteInDb.IdUsuarioNavigation.Nombre}! Estos son tus próximos turnos, podés modificarlos o cancelarlos haciendo click en los botones",
				Data = turnosDto
			};
		}

		public async Task<ResponseDto<List<TurnsOfTheDayDto>>> TraerTurnosDeHoyPorProfesional(int idProfesional)
		{

			var existeProfesional = await _unitOfWork.Profesionales.AnyProfesional(idProfesional);

			if (!existeProfesional) throw new NotFoundException($"No se encontró el profesional con el id: {idProfesional}");


			var turnosDeHoy = await _unitOfWork.Turnos.GetTurnosByProfesionalAndFechaDeHoy(idProfesional);

			return new ResponseDto<List<TurnsOfTheDayDto>> { 
				Success = true,
				Message = turnosDeHoy!.Count() > 0 ? "Turnos del dia consultados con éxito" : "No tiene turnos en el dia de hoy",
				Data = turnosDeHoy!.Select(turno => TurnoMapper.ToOfTheDayDto(turno)).ToList()
			};
		}

		public async Task<ResponseDto<PagedResult<TurnoResponseDto>>> ConsultarTurnos(int pageNumber)
		{

			const int pageSize = 6;

			var query = _unitOfWork.Turnos.Query()
				.Include(t => t.IdEspecialidadNavigation)
				.Include(p => p.IdProfesionalNavigation)
				.ThenInclude(p => p.ProfesionalEspecialidads)
					.ThenInclude(pe => pe.IdEspecialidadNavigation);

			var pagedResult = await query.ToPagedResultAsync(pageNumber, pageSize);

			return new ResponseDto<PagedResult<TurnoResponseDto>>
			{
				Success = true,
				Message = "Turnos consultados con éxito",
				Data = new PagedResult<TurnoResponseDto>
				(
					pagedResult.Data
					.Select(t => TurnoMapper.DeTurnoADto(t)).ToList(),
					pagedResult.TotalRecords,
					pagedResult.PageNumber,
					pagedResult.PageSize
				)

			};
		}
	} 
}
