using Turnero.Common.Enums;
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

		public async Task<ResponseDto<TurnoResponseDto>> SolicitarTurno(TurnoRequestDto dto)
		{
			var result = await new CreateTurnoDomain(_unitOfWork).ValidarLogicaNegocio(dto); //Validador de logica de negocio

			if (!result.EsValido) throw new BussinessErrorContentException(result.Errores);
			

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				Turno turnoSolicitado = TurnoMapper.DeTurnoDtoATurno(dto); //Mapeo de dto a Turno model

				await _unitOfWork.Turnos.AddTurno(turnoSolicitado); //Lo añado a la bd

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();


				//Para que traiga los datos de la especialidad y del profesional

				var turnoRenovado= await _unitOfWork.Turnos.FindOrDefaultTurno(turnoSolicitado.IdTurno);

				return new ResponseDto<TurnoResponseDto>
				{
					Success = true,
					Message = "Turno solicitado correctamente",
					Data = TurnoMapper.DeTurnoADto(turnoRenovado!),
				};
			}
			catch(Exception e)
			{
				await _unitOfWork.RollbackAsync(); //Volvamos para atrás, muchachos. Algo salio mal y cancelamos los cambios a la bd

				throw new Exception($"Error al solicitar turno. Inténtelo más tarde. Error: {e}");
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

		public async Task<ResponseDto<TurnoResponseDto>> CancelarTurno(int idTurno, CancelarTurnoDto dto) //TODO: DESARROLLAR
		{
			Turno? turno = await _unitOfWork.Turnos.FindOrDefaultTurno(idTurno);

			if (turno == null) throw new NotFoundException($"No se encontró el turno con el ID: {idTurno}");

			var isRecepcionista = await _unitOfWork.Usuarios.AnyRecepcionistaByDni(dto.DniDelCancelador);

			if (turno.IdPacienteNavigation.Dni != dto.DniDelCancelador && !isRecepcionista) 
				throw new ForbiddenException("No puede cancelar turnos de otro paciente.");

			turno!.EstadoTurno = EnumEstadoTurno.Cancelado.ToString();

			_unitOfWork.Turnos.Actualizar(turno);

			await _unitOfWork.CompleteAsync();

			return new ResponseDto<TurnoResponseDto>
			{
				Success = true,
				Message = "Turno cancelado correctamente",
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
	} 
}
