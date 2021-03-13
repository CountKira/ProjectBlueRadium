namespace BusinessLogic
{
	public class CreatureName : OrdinalIgnoreCaseName
	{
		public CreatureName(string value) : base(value) { }
		public static CreatureName FromExecutionTarget(ExecutionTarget target) => new(target.Value);
		public override string ToString() => Value;
	}
}