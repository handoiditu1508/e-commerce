using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ECommerce.Application.Services
{
	public static class ImageValidationService
	{
		public const int AllowedSize = 5242880;//5Mb

		public const short RatioX = 1;

		public const short RatioY = 1;

		private static readonly byte[][] allowedByteHeader = new byte[][]
		{
			new byte[]{ 137, 80, 78, 71, 13, 10, 26, 10 },//png
			Encoding.ASCII.GetBytes("GIF"),//gif
			new byte[] { 255, 216, 255 }//jpg
		};

		public static bool IsValidType(byte[] data)
			=> allowedByteHeader.Any(b => b.SequenceEqual(data.Take(b.Length)));

		public static bool IsSizeValid(byte[] data) => data.Length > AllowedSize ? false : true;

		public static bool IsAspectRatioValid(byte[] data)
		{
			using(var image = Image.FromStream(new MemoryStream(data)))
			{
				return (image.Width/image.Height) == (RatioX/RatioY);
			}
		}

		public static bool IsValid(byte[] data, out ICollection<string> errors)
		{
			errors = new List<string>();
			if (!IsValidType(data))
			{
				errors.Add("Image is invalid");
				return false;
			}
			if (!IsSizeValid(data))
			{
				errors.Add($"Size can not larger than {AllowedSize} bytes {data.Length}");
				return false;
			}
			/*if (!IsAspectRatioValid(data))
			{
				errors.Add($"Images aspect ratio must be {RatioX}:{RatioY}");
				return false;
			}*/
			return true;
		}
	}
}