namespace BusinessLogic;

public class ItemName : OrdinalIgnoreCaseName
{
	public ItemName(string value) : base(value) { }
	public static ItemName FromExecutionTarget(ExecutionTarget target) => new(target.Value);
}