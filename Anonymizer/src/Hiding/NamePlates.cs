using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Addon.Lifecycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Anonymizer.Names;
using Dalamud.Game.Gui.NamePlate;
using Dalamud.Plugin.Services;
using Dalamud.Game.ClientState.Objects.Enums;

namespace Anonymizer.NamePlates;

internal unsafe static class NamePlatesHide
{
    internal static void NamePlates(INamePlateUpdateContext context, IReadOnlyList<INamePlateUpdateHandler> handlers)
    {
        foreach (var handler in handlers)
        {
            if (handler.NamePlateKind == NamePlateKind.PlayerCharacter)
            {
                handler.RemoveFreeCompanyTag();
                handler.RemoveTitle();
                handler.RemoveLevelPrefix();
                handler.RemoveStatusPrefix();
                if (handler.PlayerCharacter != null && Svc.ClientState.LocalPlayer != null && handler.GameObjectId != Svc.ClientState.LocalPlayer.GameObjectId)// && handler.PlayerCharacter.StatusFlags != StatusFlags.PartyMember)
                {
                    handler.Name = "Hidden Player";
                    handler.NameIconId = -1;
                }
                else
                {
                    handler.Name = P.Config.MainNames[0];
                }
            }
            else if (handler.NamePlateKind == NamePlateKind.EventNpcCompanion)
            {
                handler.RemoveTitle();
            }
        }
    }

    internal static void TargetName(AddonEvent type, AddonArgs args)
    {
        var addon = (AtkUnitBase*)args.Addon;
        var targetNode = addon->GetTextNodeById(10);
        targetNode->NodeText.SetString("Hidden Player");
    }
}
