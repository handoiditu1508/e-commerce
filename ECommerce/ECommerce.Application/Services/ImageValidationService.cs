using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.Application.Services
{
	public static class ImageValidationService
	{
		public static int AllowedSize { get; set; } = 2048000;

		private static readonly byte[][] allowedByteHeader = new byte[][]
		{
			new byte[]{ 137, 80, 78, 71, 13, 10, 26, 10 },//png
			Encoding.ASCII.GetBytes("GIF"),//gif
			new byte[] { 255, 216, 255 }//jpg
		};

		public static bool IsValid(byte[] data)
			=> allowedByteHeader.Any(b => b.SequenceEqual(data.Take(b.Length)));

		public static bool IsSizeValid(byte[] data) => data.Length > AllowedSize ? false : true;
	}
}