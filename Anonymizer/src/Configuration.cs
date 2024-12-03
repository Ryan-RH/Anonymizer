using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Plugin.Services;
using ECommons.ChatMethods;
using ECommons.Configuration;

namespace Anonymizer;

public class Configuration : IEzConfig
{
    public bool Debug = false;
}
