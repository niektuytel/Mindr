using Mindr.Core.Enums;

namespace Mindr.WebUI.Models;

public class HttpVariable
{
    public VariablePosition Location { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
}