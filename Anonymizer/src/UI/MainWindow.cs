using Anonymizer.Names;
using Dalamud.Interface.Utility;
using ECommons.GameFunctions;
using ECommons.SimpleGui;
using System.Collections.Generic;
using FFXIVClientStructs.FFXIV.Client.Game.Object;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.UI;
using Anonymizer.Services;

namespace Anonymizer.UI;

public unsafe class MainWindow : ConfigWindow
{
    public MainWindow() : base() { }

    public override void Draw()
    {
        if (ImGui.Button("Change Main Names"))
        {
            NameManager.RegenerateNames();
            FurtherSvc.NamePlateGui.RequestRedraw();
        }

        var customName = "";
        ImGui.Text("Enter Custom Name");
        if (ImGui.InputText("##customName", ref customName, 50, ImGuiInputTextFlags.EnterReturnsTrue))
        {
            if (customName.Length > 0)
            {
                foreach (var SavedChar in MainPlayers.SavedCharsInfo)
                {
                    if (SavedChar.IsLocal == true)
                    {
                        SavedChar.PseudoName = customName;
                        FurtherSvc.NamePlateGui.RequestRedraw();
                    }
                }
            }

        }

        //ImGui.Text(P.Config.OrigNames[0]);
    }
}
