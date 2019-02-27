using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("ManagersGroup")]
    public class ExcursionGroup: Model
    {
        public ExcursionGroup(Guid id, Guid personId, Guid userId, Guid excursionId) : base(id)
        {
            PersonId = personId;
            UserId = userId;
            ExcursionId = excursionId;
        }

        /// <summary>
        /// Person guid from person table
        /// </summary>
        [Required(ErrorMessage = "User ID is missed")]
        [ForeignKey("Person")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        /// <summary>
        /// Manager guid from user table
        /// </summary>
        [Required(ErrorMessage = "User ID is missed")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        
        /// <summary>
        /// Excursion guid
        /// </summary>
        [Required(ErrorMessage = "Excursion ID is missed")]
        [ForeignKey("Excursion")]
        public Guid ExcursionId { get; set; }
        public virtual Excursion Excursion { get; set; }
    }
}