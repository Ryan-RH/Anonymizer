using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Addon.Lifecycle;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Anonymizer.Names;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
namespace Anonymizer.PartyList;

internal unsafe static class PartyListHide
{
    internal static void PartyListNames(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        MainPlayers.Update();
        for (uint i = 0; i < 8; i++)
        {
            var targetNode = addon->GetNodeById(i+10);
            var componentNode = targetNode->GetComponent();
            var nameNode = componentNode->GetTextNodeById(17);
            if (nameNode != null && MainPlayers.SavedCharsInfo[i].PseudoName != null)
                nameNode->GetAsAtkTextNode()->NodeText.SetString(MainPlayers.SavedCharsInfo[i].PseudoName);
        }
    }
}
