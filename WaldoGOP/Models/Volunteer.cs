using System;
namespace WaldoGOP.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; }

        public string LastName { get; set;  }

        public string FirstName { get; set;  }

        public string Email { get; set;  }

        public string Phone { get; set;  }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set;  }

        public string State { get; set;  }

        public string Zip { get; set;  }
    }
}
