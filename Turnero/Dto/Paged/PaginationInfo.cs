public class PaginationInfo
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalRecords { get; set; }
	public int TotalPages { get; set; }

	public PaginationInfo(int pageNumber, int pageSize, int totalRecords)
	{
		PageNumber = pageNumber;
		PageSize = pageSize;
		TotalRecords = totalRecords;
		TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
	}
}