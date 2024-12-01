using ECommons.ChatMethods;
using ECommons.Configuration;

namespace Anonymizer;

public class Configuration : IEzConfig
{
    public bool Debug = false;
    public string[] MainNames = new string[9];
}
