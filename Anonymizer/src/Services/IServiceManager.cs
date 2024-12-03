using Dalamud.IoC;
using Dalamud.Plugin.Services;

namespace Anonymizer.Services;

public class FurtherSvc
{
    [PluginService] public static INamePlateGui NamePlateGui { get; private set; } = null!;
    [PluginService] public static IPartyList PartyListI { get; private set; } = null!;

    public static void Init(IDalamudPluginInterface pi)
    {
        try
        {
            pi.Create<FurtherSvc>();
        }
        catch (Exception ex)
        {
            ex.Log();
        }
    }
}
