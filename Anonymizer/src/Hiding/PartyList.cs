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

namespace Anonymizer.PartyList;

internal unsafe static class PartyListHide
{
    internal static void PartyListNames(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        for (uint i = 0; i < 8; i++)
        {
            var targetNode = addon->GetNodeById(i+10);
            var componentNode = targetNode->GetComponent();
            var nameNode = componentNode->GetTextNodeById(17);
            if (nameNode != null)
                nameNode->GetAsAtkTextNode()->NodeText.SetString(P.Config.MainNames[i]);
        }
    }
}
