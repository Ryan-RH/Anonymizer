using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anonymizer.Chat;

internal unsafe static class ChatHide
{
    internal static void ChatHandler(XivChatType type, int a2, ref SeString sender, ref SeString message, ref bool isHandled)
    {
        sender.Payloads.Clear();
        sender.Payloads.Add(new TextPayload("Hidden Player"));

        if (type == XivChatType.StandardEmote || type == XivChatType.CustomEmote)
        {
            message.Payloads.Clear();
            message.Payloads.Add(new TextPayload("Player Emoted."));
            sender.Payloads.Clear();
        }

        // Find PF Payload and remove (contains names)
        // PlayerTrack shows names
    }
}
