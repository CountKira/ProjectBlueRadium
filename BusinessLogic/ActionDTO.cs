namespace BusinessLogic
{
	public class ActionDTO
	{
		public ActionDTO(Verb verb)
		{
			Verb = verb;
		}

		public Verb Verb { get; }
		public string Specifier { get; set; }
	}
}