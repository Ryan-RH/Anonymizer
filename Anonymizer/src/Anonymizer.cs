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

    [PluginService] public static INamePlateGui NamePlateGui { get; private set; } = null!;
    [PluginService] public static IPartyList PartyListI { get; private set; } = null!;
    public Anonymizer(IDalamudPluginInterface pi)
    {
        P = this;
        ECommonsMain.Init(pi, this, Module.DalamudReflector);

        EzConfig.Migrate<Configuration>();
        Config = EzConfig.Init<Configuration>();
        EzConfigGui.Init(new MainWindow());

        EzCmd.Add("/an", OnChatCommand, "Toggles plugin interface");;

        NameManager.MainNameInit();

        NamePlateGui.OnNamePlateUpdate += NamePlatesHide.NamePlates;
        Svc.Chat.ChatMessage += ChatHide.ChatHandler;

        // Need to create separate class to initialise and dispose listeners as this sucks
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "_PartyList", PartyListHide.PartyListNames);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "_TargetInfoMainTarget", NamePlatesHide.TargetName);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostSetup, "Character", MenuHide.CharacterMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "CharacterInspect", MenuHide.CharaterInspectMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "CharaCard", MenuHide.AdventurerPlateMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostDraw, "ContextMenu", MenuHide.ContextMenu);
    }

    public void Dispose()
    {
        NamePlateGui.OnDataUpdate -= NamePlatesHide.NamePlates;
        Svc.Chat.ChatMessage -= ChatHide.ChatHandler;
        ECommonsMain.Dispose();
        P = null;
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostUpdate, "_PartyList", PartyListHide.PartyListNames);
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostUpdate, "_TargetInfoMainTarget", NamePlatesHide.TargetName);
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostSetup, "Character", MenuHide.CharacterMenu);
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostUpdate, "CharacterInspect", MenuHide.CharaterInspectMenu);
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostUpdate, "CharaCard", MenuHide.AdventurerPlateMenu);
        Svc.AddonLifecycle.UnregisterListener(AddonEvent.PostDraw, "ContextMenu", MenuHide.ContextMenu);
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
