using Microsoft.EntityFrameworkCore;

namespace Turnero.Common.Extensions
{
	public static class IQueryableExtensions
	{
		public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
			this IQueryable<T> query,
			int pageNumber,
			int pageSize
		)
		{
			if (pageNumber < 1) pageNumber = 1;
			if (pageSize < 1) pageSize = 1;

			var totalRecords = await query.CountAsync();

			var data = await query
				.OrderBy(e => 0)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return new PagedResult<T>(data, totalRecords, pageNumber, pageSize);
		}

	}
}
