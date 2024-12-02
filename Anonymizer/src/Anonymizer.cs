using Dalamud.Game.Text.SeStringHandling.Payloads;
using ECommons.Configuration;
using ECommons.EzIpcManager;
using ECommons.SimpleGui;
using ECommons.Singletons;
using Anonymizer.Services;
using Anonymizer.UI;
using FFXIVClientStructs.FFXIV.Client.UI.Info;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Dalamud.Game.Addon.Lifecycle;
using Anonymizer.PartyList;
using System.Runtime.CompilerServices;
using Anonymizer.Names;
using Anonymizer.NamePlates;
using FFXIVClientStructs.FFXIV.Client.UI;
using Dalamud.IoC;
using Dalamud.Plugin.Services;
using Dalamud.Game.Gui.NamePlate;
using Dalamud.Game.ClientState.Party;
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

        NameManager.MainNameInit();

        Svc.Framework.Update += Framework_Update;
        NamePlateGui.OnNamePlateUpdate += NamePlatesHide.NamePlates;
        Svc.Chat.ChatMessage += ChatHide.ChatHandler;
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "_PartyList", PartyListHide.PartyListNames);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "_TargetInfoMainTarget", NamePlatesHide.TargetName);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostSetup, "Character", MenuHide.CharacterMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "CharacterInspect", MenuHide.CharaterInspectMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostUpdate, "CharaCard", MenuHide.AdventurerPlateMenu);
        Svc.AddonLifecycle.RegisterListener(AddonEvent.PostDraw, "ContextMenu", MenuHide.ContextMenu);
    }

    private void Framework_Update(object framework)
    {

    }


    public void Dispose()
    {
        Svc.Framework.Update -= Framework_Update;
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
