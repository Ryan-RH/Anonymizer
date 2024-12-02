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
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using Anonymizer.PartyList;

namespace Anonymizer.NamePlates;

internal unsafe static class NamePlatesHide
{
    internal static void NamePlates(INamePlateUpdateContext context, IReadOnlyList<INamePlateUpdateHandler> handlers)
    {
        PartyListHide.PartyListCollect();
        foreach (var handler in handlers)
        {
            if (handler.NamePlateKind == NamePlateKind.PlayerCharacter)
            {
                handler.RemoveFreeCompanyTag();
                handler.RemoveTitle();
                handler.RemoveLevelPrefix();
                handler.RemoveStatusPrefix();

                if (handler.BattleChara != null && Svc.ClientState.LocalPlayer != null)
                {
                    if (handler.GameObjectId == Svc.ClientState.LocalPlayer.GameObjectId) handler.Name = P!.Config.MainNames[0];
                    else
                    {
                        for (int i = 1; i < 8; i++)
                        {
                            if (handler.BattleChara.EntityId == P!.Config.partyEntityId[i])
                            {
                                handler.Name = P.Config.MainNames[i];
                                break;
                            }
                            else
                            {
                                handler.Name = "Hidden Player";
                                handler.NameIconId = -1;
                            }
                        }
                    }
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
        if (Svc.ClientState.LocalPlayer != null && Svc.ClientState.LocalPlayer.TargetObject != null 
            && Svc.ClientState.LocalPlayer.TargetObject.ObjectKind == ObjectKind.Player 
            && Svc.ClientState.LocalPlayer.TargetObject.ObjectKind != ObjectKind.Companion) // Last condition is redundant but I'm confused
                targetNode->NodeText.SetString("Hidden Player"); // Sometimes makes companions "Hidden Player", not sure why.
    }
}
