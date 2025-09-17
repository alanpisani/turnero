using Turnero.Domain.TurnoDomain;
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
			var result = await new CreateTurnoDomain(_unitOfWork).ValidarLogicaNegocio(dto); //Validador de logica de negocio

			if (!result.EsValido) //Valida a la lógica de negocio
			{
				return new ServiceResponse<Turno>
				{
					Errores = result.Errores
				};
			}

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				Turno turnoSolicitado = TurnoMapper.DeTurnoDtoATurno(dto); //Mapeo de dto a Turno model

				await _unitOfWork.Turnos.AddTurno(turnoSolicitado); //Lo añado a la bd

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
				await _unitOfWork.RollbackAsync(); //Volvamos para atrás, muchachos. Algo salio mal y cancelamos los cambios a la bd

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
