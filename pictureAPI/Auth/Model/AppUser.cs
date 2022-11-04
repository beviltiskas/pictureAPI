using Microsoft.AspNetCore.Identity;

namespace pictureAPI.Auth.Model
{
    public class AppUser : IdentityUser
    {
        [PersonalData]
        public string? AdditionalInfo { get; set; }
    }
}
