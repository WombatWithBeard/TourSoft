using System;
using System.Collections.Generic;

namespace ToursSoft.Data.Models.Users
{
    public class Admin: User
    {    
        //TO DO:
        public Admin(Guid id, string name, string company, int phoneNumber, bool isAdmin) 
            : base(id, name, company, phoneNumber, isAdmin)
        {
        }
        
        public override Dictionary<DateTime, Excursion> ExcursionViewing()
        {
            throw new NotImplementedException();
        }

        public override void ExcursionRegistration(Person person, Guid ExcursionId)
        {
            throw new NotImplementedException();
        }
    }
}