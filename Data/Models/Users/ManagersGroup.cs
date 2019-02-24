using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("ManagersGroup")]
    public class ManagersGroup: Model
    {
        public ManagersGroup(Guid id, Guid personId, Guid managerId) : base(id)
        {
            PersonId = personId;
            ManagerId = managerId;
        }

        public ManagersGroup()
        {
        }

        /// <summary>
        /// Person guid from person table
        /// </summary>
        [Required]
        [ForeignKey("Person")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// Manager guid from user table
        /// </summary>
        [Required]
        [ForeignKey("Manager")]
        public Guid ManagerId { get; set; }
        public virtual User Manager { get; set; }
    }
}