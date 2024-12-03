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


        //ImGui.Text(P.Config.OrigNames[0]);
    }
}
