using Turnero.Models;
using Turnero.Repositories.Interfaces;

namespace Turnero.Service
{
	public class EspecialidadService(IUnitOfWork unit)
	{
		private readonly IUnitOfWork _unitOfWork = unit;
		
		public async Task<ServiceResponse<List<Especialidad>>> MostrarTodasLasEspecialidades()
		{
			try
			{
				var especialidades = await _unitOfWork.Especialidades.ToListAsyncEspecialidades();

				if(especialidades.Count == 0 || especialidades == null) {
					return new ServiceResponse<List<Especialidad>>
					{
						Mensaje = "No se encontraron especialidades"
					};
				}

				return new ServiceResponse<List<Especialidad>>
				{
					Exito = true,
					Mensaje = "Lista de especialidades traida con éxito",
					Cuerpo = especialidades
				};
			}
			catch
			{
				return new ServiceResponse<List<Especialidad>>
				{
					Mensaje = "Hubo un error desconocido al traer la lista de especialidades. Inténtelo más tarde"
				};
			}
		}
		public async Task<ServiceResponse<Especialidad>> MostrarEspecialidadPorId(int id)
		{
			try
			{
				var especialidad = await _unitOfWork.Especialidades
					.FirstOrDefaultEspecialidadById(id);

				if (especialidad == null)
				{
					return new ServiceResponse<Especialidad>
					{
						Mensaje = "La especialidad ingresada no se encuentra en el sistema"
					};
				}

				return new ServiceResponse<Especialidad>
				{
					Exito = true,
					Mensaje = "Especialidad traida con éxito",
					Cuerpo = especialidad
				};
			}
			catch {
				return new ServiceResponse<Especialidad>
				{
					Mensaje = "hubo un error inesperado al intentar traer una especialidad. Inténtelo más tarde"
				};
			}
		}
	}
}
