using Anonymizer.NamePlates;
using Anonymizer.PartyList;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Plugin.Services;
using Anonymizer.Menu;

namespace Anonymizer.Services;

public static class Listeners
{
    private class ListenerInfo
    {
        public AddonEvent EventType { get; set; }
        public string AddonName { get; set; }
        public IAddonLifecycle.AddonEventDelegate Handler { get; set; }

        public ListenerInfo(AddonEvent eventType, string addonName, IAddonLifecycle.AddonEventDelegate handler)
        {
            EventType = eventType;
            AddonName = addonName;
            Handler = handler;
        }
    }

    private static readonly ListenerInfo[] EventListeners =
    {
        new ListenerInfo(AddonEvent.PostUpdate, "_PartyList", PartyListHide.PartyListNames), // Test _PartyList's AddonEvent -> When party REFRESH on playerjoin, can initialise on plugin setup
        new ListenerInfo(AddonEvent.PostUpdate, "_TargetInfoMainTarget", NamePlatesHide.TargetName),
        new ListenerInfo(AddonEvent.PostSetup, "Character", MenuHide.CharacterMenu),
        new ListenerInfo(AddonEvent.PostUpdate, "CharacterInspect", MenuHide.CharaterInspectMenu),
        new ListenerInfo(AddonEvent.PostUpdate, "CharaCard", MenuHide.AdventurerPlateMenu),
        new ListenerInfo(AddonEvent.PostDraw, "ContextMenu", MenuHide.ContextMenu)
    };


    public static void Init()
    {
        foreach (var e in EventListeners)
        {
            PluginLog.Information($"{e.AddonName} Event Listener added.");
            Svc.AddonLifecycle.RegisterListener(e.EventType, e.AddonName, e.Handler);
        }
    }

    public static void Dispose()
    {
        foreach (var e in EventListeners)
        {
            PluginLog.Information($"{e.AddonName} Event Listener removed.");
            Svc.AddonLifecycle.UnregisterListener(e.EventType, e.AddonName, e.Handler);
        }
    }
}
