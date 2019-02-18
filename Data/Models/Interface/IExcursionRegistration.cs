using System;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Interface
{
    public interface IExcursionRegistration
    {
        void ExcursionRegistration(Person person, Guid ExcursionId);
    }
}