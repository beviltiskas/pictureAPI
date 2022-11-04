namespace pictureAPI.Auth.Model
{
    public static class UserRoles
    {
        public const string Admin = nameof(Admin);
        public const string AppUser = nameof(AppUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, AppUser };
    }
}
