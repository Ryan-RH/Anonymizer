using Dalamud.Game.Text.SeStringHandling.Payloads;
using ECommons.Configuration;
using ECommons.EzIpcManager;
using ECommons.SimpleGui;
using ECommons.Singletons;
using Anonymizer.Services;
using Anonymizer.UI;

namespace Anonymizer;

public unsafe class Anonymizer : IDalamudPlugin
{
    internal static Anonymizer P;
    internal Configuration Config;
    public Anonymizer(IDalamudPluginInterface pi)
    {
        // Plugin Initialisation
        P = this;
        ECommonsMain.Init(pi, this, Module.DalamudReflector);


        // Config Window
        EzConfig.Migrate<Configuration>();
        Config = EzConfig.Init<Configuration>();
        EzConfigGui.Init(new MainWindow());

        // Command + IPC
        EzCmd.Add("/an", OnChatCommand, "Toggles plugin interface");;
        SingletonServiceManager.Initialize(typeof(ServiceManager));
    }

    public void Dispose()
    {
        ECommonsMain.Dispose();
        P = null;
    }

    private void OnChatCommand(string command, string arguments)
    {
        arguments = arguments.Trim();

        if (arguments == string.Empty)
        {
            EzConfigGui.Window.Toggle();
        }
        else if (arguments == "debug")
        {
            Config.Debug = !Config.Debug;
            PluginLog.Information($"Debug: {Config.Debug}");
        }
    }
}
