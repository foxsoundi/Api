using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class Profil
    {
        public Guid SessionId { get; }
        private string email;

        public Profil(LoginDto loginDto)
        {
            this.email = loginDto.Email;
            this.SessionId = Guid.NewGuid();
        }
    }
}
