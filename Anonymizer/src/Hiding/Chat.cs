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
        PluginLog.Information("Chat Message Incoming");
        TextPayload tPayload = new TextPayload("Hidden Player");
        sender.Payloads.Clear();
        sender.Payloads.Add(tPayload);
    }
}
