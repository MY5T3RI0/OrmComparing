namespace OrmComparing.Comparing;

public class ComparingInfo
{
    public ComparingInfo(Action operation, string comparingTypeName, string operationName)
    {
        Operation = operation;
        ComparingTypeName = comparingTypeName;
        OperationName = operationName;
    }

    public Action Operation { get; set; }
    public string ComparingTypeName { get; set; }
    public string OperationName { get; set; }
}
