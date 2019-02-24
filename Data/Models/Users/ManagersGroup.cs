using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("ManagersGroup")]
    public class ManagersGroup: Model
    {
        public ManagersGroup(Guid id, Guid personId, Guid userId, Guid excursionId) : base(id)
        {
            PersonId = personId;
            UserId = userId;
            ExcursionId = excursionId;
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
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        
        [Required]
        [ForeignKey("Excursion")]
        public Guid ExcursionId { get; set; }
        public virtual Excursion Excursion { get; set; }
    }
}