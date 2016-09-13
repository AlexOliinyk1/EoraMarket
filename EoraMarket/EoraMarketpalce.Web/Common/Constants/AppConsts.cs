namespace EoraMarketpalce.Web.Common.Constants
{
    public sealed class AppConsts
    {
        private AppConsts() { }

        #region UsersData
        public const string AdminRoleName = "Admin";
        public const string UserRoleName = "User";
        public const string AdminRoleDescription = "Administrator can create and manage shop content and can create new administrators.";
        public const string UserRoleDescription = "Users can create characters and can trade in shop.";

        public const string SuperUserName = "admin@eora.com";
        public const string SuperUserPass = "1234qwer!";
        #endregion
    }
}