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

        public Credential(Database.Player playedEntity)
        {
            email = playedEntity.Email;
            password = playedEntity.Password;
        }

        public bool CheckCredential(CredentialDto credentialDto)
        {
            return email == credentialDto.Email 
                   && password == credentialDto.Password;
        }

        public CredentialDto GetDto()
        {
            return new CredentialDto{Email = this.email, Password = this.password};
        }
    }
}