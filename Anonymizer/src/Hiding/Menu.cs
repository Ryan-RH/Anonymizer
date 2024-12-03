using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Addon.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Dalamud.Game.Gui.ContextMenu;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using ECommons.UIHelpers.AtkReaderImplementations;
using FFXIVClientStructs.FFXIV.Client.System.Framework;

namespace Anonymizer.Menu;

internal unsafe static class MenuHide
{
    internal static void CharacterMenu(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        foreach (var x in MainPlayers.SavedCharsInfo)
            if (x.IsLocal != null && x.IsLocal == true)
                addon->GetNodeById(2)->GetAsAtkTextNode()->NodeText.SetString(x.PseudoName);
    }

    internal static void CharaterInspectMenu(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        addon->GetNodeById(6)->GetAsAtkTextNode()->ToggleVisibility(false); // Name
        addon->GetNodeById(7)->GetAsAtkTextNode()->ToggleVisibility(false); // Title
        addon->GetNodeById(3)->GetAsAtkComponentButton()->GetImageNodeById(2)->ToggleVisibility(false); // DescImg
        addon->GetNodeById(3)->ToggleVisibility(false); // DescButAtk (I knew what this meant at the time, idk what now)
        addon->GetNodeById(10)->GetAsAtkTextNode()->ToggleVisibility(false); // World
        addon->GetNodeById(9)->GetAsAtkImageNode()->ToggleVisibility(false); // WorldImg
        addon->GetNodeById(25)->ToggleVisibility(false); // FC
        addon->GetNodeById(22)->ToggleVisibility(false); // GC
    } 

    internal static void AdventurerPlateMenu(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        addon->GetNodeById(11)->ToggleVisibility(false); // Desc
        addon->GetNodeById(8)->ToggleVisibility(false); // FC
        addon->GetNodeById(7)->ToggleVisibility(false); // GC
        addon->GetNodeById(5)->ToggleVisibility(false); // World
        addon->GetNodeById(4)->ToggleVisibility(false); // Name
    }

    internal static void ContextMenu(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        var baseNode = addon->GetNodeById(2)->GetAsAtkComponentList();
        if (baseNode != null)
        {
            var textNode = baseNode->GetItemRenderer(2)->GetTextNodeById(3)->GetAsAtkTextNode()->NodeText;
            var collisionNode = baseNode->GetItemRenderer(2)->AtkResNode->GetAsAtkCollisionNode();
            if (textNode.ToString() == "Send Tell")
            {
                textNode.SetString("Send Tell [Disabled]");
                collisionNode->ToggleVisibility(false);
            }
            else collisionNode->ToggleVisibility(true);
        }

    }
}
