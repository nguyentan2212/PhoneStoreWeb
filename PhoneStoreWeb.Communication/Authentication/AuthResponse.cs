using System;

namespace PhoneStoreWeb.Communication
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public long TokenExpirationTime { get; set; }
    }
}
