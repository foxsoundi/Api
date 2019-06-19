using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Nancy;

namespace Api.Modules
{
    public class ArtistModule : NancyModule
    {
        public ArtistModule() : base("v1/music/artist")
        {
            Get("", async Areas_Identity_Pages_Account_AccessDenied =);
        }
    }
}
