namespace TO_DO_LIST.Service.UserPassHashSaltService
{
    public interface IUserPassHashSaltService
    {
        //Kreiate only PaswordSalt i Hash
        public byte[] CreateOnlyPasswordSalt(out byte[] passwordSalt);

        public byte[] CreateOnlyPasswordHash(string passwprd, out byte[] passwordHash);
        
    }
}
