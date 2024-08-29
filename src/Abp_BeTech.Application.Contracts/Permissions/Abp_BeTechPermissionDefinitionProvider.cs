using Abp_BeTech.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp_BeTech.Permissions;


public class Abp_BeTechPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var Abp_BeTechGroup = context.AddGroup(Abp_BeTechPermissions.GroupName, L("Permission:Abp_BeTech"));

        var CategorysPermission = Abp_BeTechGroup.AddPermission(Abp_BeTechPermissions.Category.Default, L("Permission:Categorys"));
        CategorysPermission.AddChild(Abp_BeTechPermissions.Category.Create, L("Permission:Categorys.Create"));
        CategorysPermission.AddChild(Abp_BeTechPermissions.Category.Edit, L("Permission:Categorys.Edit"));
        CategorysPermission.AddChild(Abp_BeTechPermissions.Category.Delete, L("Permission:Categorys.Delete"));

        var ProductsPermission = Abp_BeTechGroup.AddPermission(Abp_BeTechPermissions.Product.Default, L("Permission:Products"));
        ProductsPermission.AddChild(Abp_BeTechPermissions.Product.Create, L("Permission:Products.Create"));
        ProductsPermission.AddChild(Abp_BeTechPermissions.Product.Edit, L("Permission:Products.Edit"));
        ProductsPermission.AddChild(Abp_BeTechPermissions.Product.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Abp_BeTechResource>(name);
    }
}
