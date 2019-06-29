using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Profil : IToDto<LoginDto>
    {
        public Guid SessionId { get; }
        public string email { get; }

        public Profil(CredentialDto credentialDto)
        {
            this.email = credentialDto.Email;
            this.SessionId = Guid.NewGuid();
        }


        public LoginDto GetDto()
        {
            return new LoginDto {Profil = this};
        }
    }
}
