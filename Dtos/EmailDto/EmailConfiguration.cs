namespace TO_DO_LIST.Dtos.EmailDto
{
    public class EmailConfiguration
    {
   
        public String FromName { get; set; }
        public String From { get; set; }
        public String SmtpServer { get; set; }
        public String Port { get; set; }

        public String Username { get; set; }
        public String Password { get; set; }
    }
}
