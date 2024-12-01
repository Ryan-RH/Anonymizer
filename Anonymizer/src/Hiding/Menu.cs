using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Addon.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace Anonymizer.Menu;

internal unsafe static class MenuHide
{
    internal static void CharacterMenu(AddonEvent type, AddonArgs args)
    {
        // This crashed me but Idk why
        //var addon = (AtkUnitBase*)args.Addon;
        //var targetNode = addon->GetTextNodeById(2);
        //targetNode->NodeText.SetString(P.Config.MainNames[0]);
    }


}
