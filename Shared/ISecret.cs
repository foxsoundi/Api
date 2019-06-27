using System.Net.Http.Headers;

namespace Shared
{
    public interface ISecret
    {
        AuthenticationHeaderValue GetBasicAuthenticationHeaderValue();
    }
}