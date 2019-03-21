using System;

namespace ToursSoft.Data.Models.Interface
{
    /// <summary>
    /// Class Id interface
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// default id
        /// </summary>
        Guid Id { get; }
    }
}