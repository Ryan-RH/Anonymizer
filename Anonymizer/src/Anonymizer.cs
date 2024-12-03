using ECommons.Configuration;
using ECommons.SimpleGui;
using ECommons.Singletons;
using Anonymizer.Services;
using Anonymizer.UI;
using Dalamud.Game.Addon.Lifecycle;
using Anonymizer.PartyList;
using Anonymizer.Names;
using Anonymizer.NamePlates;
using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Anonymizer.Chat;
using Anonymizer.Menu;


namespace Anonymizer;


public unsafe class Anonymizer : IDalamudPlugin
{
    internal static Anonymizer? P;
    internal Configuration Config;
    public Anonymizer(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, this, Module.DalamudReflector);
        FurtherSvc.Init(pi);

        EzConfig.Migrate<Configuration>();
        Config = EzConfig.Init<Configuration>();
        EzConfigGui.Init(new MainWindow());

        EzCmd.Add("/an", OnChatCommand, "Toggles plugin interface");;

        MainPlayers.Update();

        FurtherSvc.NamePlateGui.OnNamePlateUpdate += NamePlatesHide.NamePlates;
        Svc.Chat.ChatMessage += ChatHide.ChatHandler;

        Listeners.Init();
    }

    public void Dispose()
    {
        FurtherSvc.NamePlateGui.OnDataUpdate -= NamePlatesHide.NamePlates;
        Svc.Chat.ChatMessage -= ChatHide.ChatHandler;
        ECommonsMain.Dispose();
        P = null;
        Listeners.Dispose();
    }

    private void OnChatCommand(string command, string arguments)
    {
        arguments = arguments.Trim();

        switch (arguments)
        {
            case "debug":
                Config.Debug = !Config.Debug;
                PluginLog.Information($"Debug: {Config.Debug}");
                break;
            default:
                EzConfigGui.Window.Toggle();
                break;
        }
    }
}
