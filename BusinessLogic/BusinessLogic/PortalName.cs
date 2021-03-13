namespace BusinessLogic
{
	public class PortalName : OrdinalIgnoreCaseName
	{
		public PortalName(string value) : base(value) { }
		public static PortalName FromExecutionTarget(ExecutionTarget target) => new(target.Value);
		public override string ToString() => Value;
	}
}