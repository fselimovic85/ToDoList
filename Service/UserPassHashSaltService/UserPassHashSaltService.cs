namespace TO_DO_LIST.Service.UserPassHashSaltService
{
    public class UserPassHashSaltService : IUserPassHashSaltService
    {
        public byte[] CreateOnlyPasswordHash(string passwprd, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwprd));

            }
        }

        public byte[] CreateOnlyPasswordSalt(out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                return passwordSalt = hmac.Key;

            }
        }
    }
}
