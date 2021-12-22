namespace BusinessLogic;

public class OutputData
{
	public OutputData(OutputDataType type) => Type = type;

	public OutputDataType Type { get; }
	public string? Specifier { get; set; }
}