using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.UI.Shared.Extensions;

namespace ECommerce.UI.Shared.Models.ViewModels
{
	public class ReturnMessagesViewModel : PageModel
	{
		public IEnumerable<string> Messages { get; set; }
		public MessageType MessageType { get; set; }

		public string ConfirmString { get; set; }
		public string RedirectUrl { get; set; }

		public ReturnMessagesViewModel()
			:base()
		{
			Messages = new List<string>();
			MessageType = MessageType.Info;
			ConfirmString = "OK";
			RedirectUrl = Url.HomePage();
		}
	}

	public enum MessageType
	{
		Success,
		Error,
		Info
	}
}