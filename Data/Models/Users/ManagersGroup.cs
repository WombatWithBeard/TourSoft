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
        public Guid? PersonId { get; set; }
        [Required]
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        /// <summary>
        /// Manager guid from user table
        /// </summary>
        public Guid ManagerId { get; set; }
        [Required]
        [ForeignKey("ManagerId")]
        public virtual User User { get; set; }
    }
}