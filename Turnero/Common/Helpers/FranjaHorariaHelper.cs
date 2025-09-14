namespace Turnero.Common.Helpers
{
	public class FranjaHorariaHelper
	{
		public static IEnumerable<TimeOnly> FranjaHorariaProfesional(TimeOnly inicio, TimeOnly fin, int duracionMinutos)
		{
			var actual = inicio;

			while (actual < fin)
			{
				yield return actual;
				actual = actual.AddMinutes(duracionMinutos);
			}
		}
	}
}
