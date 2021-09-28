using System;

namespace WebApi.Auth
{
    public class Token
    {
        public string TokenString { get; set; }
        public string UserName { get; set; }
        public DateTime ExpireDate { get; set; }

        public Token(string userName)
        {
            UserName = userName;
            TokenString = Guid.NewGuid().ToString();
            ExpireDate = DateTime.Now.AddMinutes(1);
        }
    }
}