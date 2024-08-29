namespace Abp_BeTech.Permissions;

public static class Abp_BeTechPermissions
{
    public const string GroupName = "Abp_BeTech";

    public static class Category
    {
        public const string Default = GroupName + ".Category";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Product
    {
        public const string Default = GroupName + ".Product";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

}
