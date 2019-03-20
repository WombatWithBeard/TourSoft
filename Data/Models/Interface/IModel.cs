using System;
using System.ComponentModel.DataAnnotations;

namespace ToursSoft.Data.Models.Interface
{
    public interface IModel
    {
        [Key]
        Guid Id { get; }
    }
}