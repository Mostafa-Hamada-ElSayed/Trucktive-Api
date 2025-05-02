using Microsoft.AspNetCore.Http;
using Trucktive.Core.Abstractions;

namespace Trucktive.Core.Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentials
            = new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);
    }

}
