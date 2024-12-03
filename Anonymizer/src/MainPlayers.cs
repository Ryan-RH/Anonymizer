using Anonymizer.Names;
using FFXIVClientStructs.FFXIV.Client.Game.Character;
using FFXIVClientStructs.FFXIV.Client.Game.Group;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonymizer;

public unsafe class MainPlayers
{
    public class MainCharsInfo
    {
        public bool? isAvailable { get; set; }
        public string? PlayerName { get; set; }
        public string? PseudoName { get; set; }
        public uint? EntityId { get; set; }
        public bool? IsLocal { get; set; }

        public MainCharsInfo() { }

        public void Populate(HudPartyMember pMember)
        {
            isAvailable = true;
            PlayerName = pMember.Object->NameString;
            PseudoName = NameManager.RandomNameGenerate();
            EntityId = pMember.Object->EntityId;
            IsLocal = pMember.Object->EntityId == Svc.ClientState.LocalPlayer!.EntityId;
        }
    }

    private static MainCharsInfo[] CreateCharArray(int size)
    {
        MainCharsInfo[] CharArray = new MainCharsInfo[size];
        for (int i = 0; i < size; i++)
            CharArray[i] = new MainCharsInfo();
        return CharArray;
    }

    public static MainCharsInfo[] SavedCharsInfo = CreateCharArray(8);

    public static void Update()
    {
        var pAgentHUD = Framework.Instance()->GetUIModule()->GetAgentModule()->GetAgentHUD();
        MainCharsInfo[] b_SavedCharsInfo = CreateCharArray(8);
        if (Svc.ClientState.LocalPlayer != null)
        {
            if (pAgentHUD->PartyMemberCount > 0)
            {
                for (int j = 0; j < pAgentHUD->PartyMemberCount; j++)// iterate through current party members
                {
                    var pMember = pAgentHUD->PartyMembers[j];
                    var ppFound = false;
                    
                    if (pMember.Object != null)
                    {
                        for (byte i = 0; i < 8; i++)
                        {
                            if (pMember.Object->EntityId == SavedCharsInfo[i].EntityId) // check if previous saved is within party
                            {
                                b_SavedCharsInfo[pMember.Index] = SavedCharsInfo[i]; // if so define that index in the buffer with the matched entity
                                ppFound = true;
                                break;
                            }
                        }
                        if (!ppFound)
                        {
                            b_SavedCharsInfo[pMember.Index].Populate(pMember); // new party member, create a new name and save in buffer
                            var test = b_SavedCharsInfo[pMember.Index];
                        }
                    }
                    else
                    {
                        b_SavedCharsInfo[pMember.Index].isAvailable = false;
                    }
                }
                SavedCharsInfo = b_SavedCharsInfo; // overwrite saved with buffer
            }
            else
            {
                PluginLog.Information("Party was DESTROYED");
            }
        }
    }
}
