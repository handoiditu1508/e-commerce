using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Models
{
	public class ErrorsManager : ICollection<string>
	{
		private ICollection<string> Errors { get; set; }

		public ErrorsManager()
		{
			Errors = new List<string>();
		}

		public int Count => Errors.Count;

		public bool IsReadOnly => Errors.IsReadOnly;

		public void Add(string error)
		{
			Errors.Add(error);
		}

		public override string ToString() => ToString(AppConsts.errorSeparator);

		public string ToString(char separator)
		{
			string result = string.Empty;
			foreach (string error in Errors)
				result += error + separator;
			return result.Substring(0, result.Count() - 1);
		}

		public void Clear()
		{
			Errors.Clear();
		}

		public bool Contains(string item)
		{
			return Errors.Contains(item);
		}

		public void CopyTo(string[] array, int arrayIndex)
		{
			Errors.CopyTo(array, arrayIndex);
		}

		public bool Remove(string item)
		{
			return Errors.Remove(item);
		}

		public IEnumerator<string> GetEnumerator()
		{
			return Errors.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Errors.GetEnumerator();
		}
	}
}
