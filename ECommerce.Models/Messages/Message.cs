namespace ECommerce.Models.Messages
{
	public class Message<T> : Message
	{
		public T Result { get; set; }

		public Message() : base()
		{ }

		public Message(T result) : base()
		{
			Result = result;
		}
	}

	public class Message
	{
		public ErrorsManager Errors { get; set; }

		public Message()
		{
			Errors = new ErrorsManager();
		}
	}
}
