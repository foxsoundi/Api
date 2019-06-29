namespace Shared.Player
{
    internal class Credential : IToDto<CredentialDto>
    {
        private readonly string password;
        private readonly string email;

        public Credential(CredentialDto credentialDto)
        {
            this.email = credentialDto.Email;
            this.password = credentialDto.Password;
        }

        public bool isTheSameThan(CredentialDto credentialDto)
        {
            return email == credentialDto.Email;
        }

        public CredentialDto GetDto()
        {
            return new CredentialDto{Email = this.email, Password = this.password};
        }
    }
}