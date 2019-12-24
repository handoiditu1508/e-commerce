using System;

namespace ECommerce.UI.Shared.Models
{
	public class PagingInfo
	{
		public static short DefaultRecordsPerPage { get; set; } = 6;
		public static short DefaultMaxPageLength { get; set; } = 7;

		public int TotalRecords { get; set; }
		public short RecordsPerPage { get; set; } = DefaultRecordsPerPage;
		public short CurrentPage { get; set; } = 1;
		public short MaxPageLength { get; set; } = DefaultMaxPageLength;

		public int TotalPages => (int)Math.Ceiling((decimal)TotalRecords / RecordsPerPage);
	}
}