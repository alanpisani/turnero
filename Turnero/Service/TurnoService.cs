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

		public async Task<Turno> SolicitarTurno(TurnoRequestDto dto)
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

				return turnoSolicitado;
			}
			catch
			{
				await _unitOfWork.RollbackAsync(); //Volvamos para atrás, muchachos. Algo salio mal y cancelamos los cambios a la bd

				throw new Exception("Error al solicitar turno. Inténtelo más tarde.");
			}

		}

		public async Task<Turno> CancelarTurno(int idTurno) //TODO: DESARROLLAR
		{
			Turno? turno = await _unitOfWork.Turnos.FindOrDefaultTurno(idTurno);

			if (turno == null) throw new NotFoundException($"No se encontró el turno con el ID: {idTurno}");

			turno!.IdEstadoTurno = 4;

			_unitOfWork.Turnos.Actualizar(turno);

			await _unitOfWork.CompleteAsync();

			return turno;
		}

		public async Task<List<Turno>> TraerTurnosDelPaciente(int idPaciente)
		{
			bool hayPaciente = await _unitOfWork.Pacientes.AnyPaciente(idPaciente);

			if (!hayPaciente) throw new NotFoundException($"No hay paciente registrado con el ID: {idPaciente}");

			var turnos = await _unitOfWork.Turnos.GetTurnosByPaciente(idPaciente);

			if(turnos == null || turnos.Count == 0) throw new NoTurnoException("No tenés turnos reservados");

			return turnos;
		}

		public async Task<ResponseDto<List<Turno>>> TraerTurnosDelPacientePorDni(int dniPaciente)
		{
			var pacienteInDb = await _unitOfWork.Pacientes.GetPacienteByDni(dniPaciente);

			if (pacienteInDb == null) throw new NotFoundException($"No se ha encontrado un paciente con el dni {dniPaciente}. Por favor, verificá que hayas ingresado el DNI correctamente.");

			var turnos = await _unitOfWork.Turnos.GetTurnosByPaciente(pacienteInDb.IdUsuario);

			if (turnos == null || turnos.Count == 0) throw new NoTurnoException($"Hola, {pacienteInDb.IdUsuarioNavigation.Nombre}. No tenés turnos reservados");

			return new ResponseDto<List<Turno>>
			{
				Success = true,
				Message= $"¡Hola {pacienteInDb.IdUsuarioNavigation.Nombre}! Estos son tus próximos turnos, podés modificarlos o cancelarlos haciendo click en los botones",
				Data = turnos
			};

		}
	} 
}
