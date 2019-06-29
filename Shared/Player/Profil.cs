using System;

namespace Shared.Player
{
    public class Profil : IToDto<LoginDto>
    {
        public Guid SessionId { get; }
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public Profil(Database.Player player)
        {
            this.Email = player.Email;
            this.FirstName = player.FirstName;
            this.LastName = player.LastName;
            this.SessionId = Guid.NewGuid();
        }

        public LoginDto GetDto() => new LoginDto {Profil = this};
    }
}
