using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Shared;

namespace Api
{
    public class NapsterSecrets : ISecret
    {
        public string NapsterId { get; set; }
        public string NapsterSecret { get; set; }

        public NapsterSecrets() { }
        public AuthenticationHeaderValue GetBasicAuthenticationHeaderValue()
        {
            throw new NotImplementedException();
        }
    }
}
