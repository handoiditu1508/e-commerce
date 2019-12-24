namespace ECommerce.Models.Messages
{
	public class BoolMessage : Message<bool>
	{
		public BoolMessage(bool result = false) : base(result)
		{ }
	}
}
