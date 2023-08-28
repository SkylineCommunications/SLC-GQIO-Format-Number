using System;
using Skyline.DataMiner.Analytics.GenericInterface;

[GQIMetaData(Name = "Format Number")]
public class MyCustomOperator : IGQIRowOperator, IGQIInputArguments
{
    private GQIColumnDropdownArgument _numberColumnArg = new GQIColumnDropdownArgument("Number column") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.Double, GQIColumnType.Int } };
    private GQIIntArgument _decimalsArg = new GQIIntArgument("Decimals") { IsRequired = true, DefaultValue = 0 };
    private GQIStringArgument _unitArg = new GQIStringArgument("Unit") { IsRequired = false, DefaultValue = String.Empty };

    private int _decimals;
    private string _unit;
    private GQIColumn _numberColumn;

    public GQIArgument[] GetInputArguments()
    {
        return new GQIArgument[] { _numberColumnArg, _decimalsArg, _unitArg };
    }

    public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
    {
        _numberColumn = args.GetArgumentValue(_numberColumnArg);
        _decimals = args.GetArgumentValue(_decimalsArg);
        _unit = args.GetArgumentValue(_unitArg);
        return new OnArgumentsProcessedOutputArgs();
    }

    public void HandleRow(GQIEditableRow row)
    {
        try
        {
            double value = Convert.ToDouble(row.GetValue(_numberColumn.Name));
            row.SetDisplayValue(_numberColumn, (Math.Round(value, _decimals) + " " + _unit).Trim());
        }
        catch (Exception)
        {
            // Catch empty cells
        }
    }
}