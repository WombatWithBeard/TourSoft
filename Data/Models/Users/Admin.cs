using System;
using System.Collections.Generic;

namespace ToursSoft.Data.Models.Models.Users
{
    public class Admin: User
    {
        public override Dictionary<DateTime, Excursion> ExcursionViewing()
        {
            throw new NotImplementedException();
        }

        public override void ExcursionRegistration(Person person, Guid ExcursionId)
        {
            throw new NotImplementedException();
        }
        
        //TO DO:
    }
}