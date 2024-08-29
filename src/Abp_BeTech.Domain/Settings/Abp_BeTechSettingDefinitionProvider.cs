using Volo.Abp.Settings;

namespace Abp_BeTech.Settings;

public class Abp_BeTechSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(Abp_BeTechSettings.MySetting1));
    }
}
