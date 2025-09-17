using Turnero.Dto;
using Turnero.Mappers;
using Turnero.Models;
using Turnero.Repositories.Interfaces;
using Turnero.Validators.TurnoValidators;

namespace Turnero.Service
{
	public class TurnoService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;

		public async Task<ServiceResponse<Turno>> SolicitarTurno(TurnoDto dto)
		{
			await _unitOfWork.BeginTransactionAsync();

			try
			{
				Turno turnoSolicitado = TurnoMapper.DeTurnoDtoATurno(dto);

				await _unitOfWork.Turnos.AddTurno(turnoSolicitado);

				await _unitOfWork.CompleteAsync();
				await _unitOfWork.CommitAsync();

				return new ServiceResponse<Turno>
				{
					Exito = true,
					Mensaje = "Turno Solicitado con éxito",
					Cuerpo = turnoSolicitado
				};
			}
			catch
			{
				await _unitOfWork.RollbackAsync();

				return new ServiceResponse<Turno>
				{
					Mensaje = "Error inesperado al solicitar un turno. Inténtelo más tarde",
				};
			}

		}

		public async Task<ServiceResponse<string>> CancelarTurno(int idTurno) //TODO: DESARROLLAR
		{
			Turno? turno = await _unitOfWork.Turnos.FindOrDefaultTurno(idTurno);

			turno!.IdEstadoTurno = 4;

			_unitOfWork.Turnos.Actualizar(turno);

			await _unitOfWork.CompleteAsync();

			return new ServiceResponse<string>
			{
				Mensaje = "Turno cancelado",
				Exito = true
			};
		}

		public async Task<ServiceResponse<List<Turno>>> TraerTurnosDelPaciente(int idPaciente)
		{
			var turnos = await _unitOfWork.Turnos.GetTurnosByPaciente(idPaciente);

			return new ServiceResponse<List<Turno>> { 
				Exito=true,
				Mensaje="Lista de turnos del paciente traida con éxito",
				Cuerpo = turnos
			};
		} 
	} 
}
