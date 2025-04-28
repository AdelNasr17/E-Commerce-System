

using System.ComponentModel.DataAnnotations;

namespace Shared.Identity.Dto
{
    public class UserDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!;

        public string Token { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
