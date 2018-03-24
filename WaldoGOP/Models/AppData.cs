using System;
namespace WaldoGOP.Models
{
    public class AppData
    {
        public string DeviceID
        {
            get; set;
        }

        public string Access_Token
        {
            get; set; 
        }

        public DateTime Expires_In
        {
            get; set;
        }

        public bool Verified
        {
            get; set;
        }
    }
}
