using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Entities
{
	[Owned]
	[ComplexType]
	public class FileContent
	{
		public FileContent(byte[] data, string mimeType)
		{
			Data = data;
			MimeType = mimeType;
		}

		[Required]
		[Column("Data")]
		public byte[] Data { get; set; }

		[Required]
		[Column("MimeType")]
		public string MimeType { get; set; }

		public string EncodeInBase64() => string.Format($"data:{MimeType};base64,{Convert.ToBase64String(Data)}");
	}
}