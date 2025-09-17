using Turnero.Dto;
using Turnero.Models;

namespace Turnero.Mappers
{
	public class CoberturaMedicaMapper
	{
		public CoberturaMedica DeCoberturaDtoACobertura(CoberturaMedicaDto dto)
		{
			return new CoberturaMedica
			{
				Nombre = dto.Nombre,
				Direccion = dto.Direccion,
				Telefono = dto.Telefono,
				TipoCobertura = dto.TipoCobertura,
			};
		}
	}
}
