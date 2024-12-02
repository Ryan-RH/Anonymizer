using Anonymizer.Names;
using Dalamud.Interface.Utility;
using ECommons.GameFunctions;
using ECommons.SimpleGui;
using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.UI;

namespace Anonymizer.UI;

public unsafe class MainWindow : ConfigWindow
{
    public MainWindow() : base() { }

    public override void Draw()
    {
        if (ImGui.Button("Change Main Names"))
        {
            NameManager.MainNameInit();
            NamePlateGui.RequestRedraw();
        }

        if (ImGui.Button("Change Everyone's Names"))
        {
            
        }

        ImGui.Text(P.Config.partyEntityId[0].ToString());
        ImGui.Text(Svc.ClientState.LocalPlayer.EntityId.ToString());

    }
}
