namespace ECommerce.Models.Messages
{
	public class StringMessage : Message<string>
	{
		public StringMessage(string result = null) : base(result)
		{ }
	}
}
