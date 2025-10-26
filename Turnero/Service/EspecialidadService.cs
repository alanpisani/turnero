using Turnero.Exceptions;
using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
	public class EspecialidadService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		
		public async Task<List<Especialidad>> MostrarTodasLasEspecialidades()
		{
			try
			{
				var especialidades = await _unitOfWork.Especialidades.ToListAsyncEspecialidades();

				return especialidades;
			}
			catch
			{
				throw new Exception("Hubo un error desconocido al traer la lista de especialidades. Inténtelo más tarde.");
			}
		}
		public async Task<Especialidad> MostrarEspecialidadPorId(int id)
		{
			try
			{
				var especialidad = await _unitOfWork.Especialidades
					.FirstOrDefaultEspecialidadById(id);

				if (especialidad == null) throw new NotFoundException("La especialidad ingresada no se encuentra en el sistema");

				return especialidad;

			}

			catch
			{
				throw new Exception("hubo un error inesperado al intentar traer una especialidad. Inténtelo más tarde");
			}
		}
	}
}
