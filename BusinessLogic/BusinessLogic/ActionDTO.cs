namespace BusinessLogic
{
	public class ActionDTO
	{
		public ActionDTO(VerbEnum verb)
		{
			Verb = verb;
		}

		public VerbEnum Verb { get; }
		public string? Specifier { get; set; }
	}
}