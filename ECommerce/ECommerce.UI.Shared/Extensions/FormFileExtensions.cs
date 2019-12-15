using Microsoft.AspNetCore.Http;
using System;

namespace ECommerce.UI.Shared.Extensions
{
	public static class FormFileExtensions
	{
		public static string GenerateRandomNameWithExtension(this IFormFile file)
			=> $"{Guid.NewGuid()}.{file.ContentType.Substring(file.ContentType.IndexOf('/') + 1)}";
	}
}