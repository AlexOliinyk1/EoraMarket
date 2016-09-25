﻿namespace EoraMarketpalce.Web.Common.Constants
{
    /// <summary>
    ///     Application constants
    /// </summary>
    internal sealed class AppConsts
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

        public const string CHARACTER_STORE_NAME = "characterId";

        public const int START_COSTS = 1000;
        public const int MONEY_MULTIPLIER = 100;
        public const string DEFAULT_IMAGE_URL = "~/Content/Images/no-image.png";
    }
}