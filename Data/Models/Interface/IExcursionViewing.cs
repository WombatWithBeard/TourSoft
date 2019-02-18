using System;
using System.Collections.Generic;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Interface
{
    public interface IExcursionViewing
    {
        Dictionary<DateTime, Excursion> ExcursionViewing();
    }
}