using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models.Users
{
    /// <summary>
    /// User role class: for access to excursion and another
    /// </summary>
    [Table("Role")]
    public class Role: Model
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Role(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
            //UserRoles = new List<UserRole>();
        }

        /// <inheritdoc />
        public Role()
        {
        }

        /// <summary>
        /// role name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// info about role
        /// </summary>
        public string Description { get; set; }
        
        public IList<UserRole> UserRoles { get; set; }
    }
}