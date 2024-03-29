﻿using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Application.Services
{
	public static class EncryptionService
	{
		public static string Encrypt(string plainText)
		{
			MD5 md5 = new MD5CryptoServiceProvider();

			//compute hash from the bytes of text
			md5.ComputeHash(Encoding.ASCII.GetBytes(plainText));

			//get hash result after compute it
			byte[] result = md5.Hash;

			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < result.Length; i++)
			{
				//change it into 2 hexadecimal digits
				//for each byte
				strBuilder.Append(result[i].ToString("x2"));
			}

			return strBuilder.ToString();
		}
	}
}