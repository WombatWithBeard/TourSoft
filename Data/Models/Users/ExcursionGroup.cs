using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ToursSoft.Data.Contexts;

namespace ToursSoft.Data.Models.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Excursion group class: class whos connecting user, their persons(group) and specific excursion
    /// </summary>
    [Table("ExcursionGroup")]
    public class ExcursionGroup: Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personId"></param>
        /// <param name="userId"></param>
        /// <param name="excursionId"></param>
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
        /// <summary>
        /// link on person table
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Manager guid from user table
        /// </summary>
        [Required(ErrorMessage = "User ID is missed")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        /// <summary>
        /// link on user table
        /// </summary>
        public virtual User User { get; set; }
        
        /// <summary>
        /// Excursion guid
        /// </summary>
        [Required(ErrorMessage = "Excursion ID is missed")]
        [ForeignKey("Excursion")]
        public Guid ExcursionId { get; set; }
        /// <summary>
        /// link on excursion table
        /// </summary>
        public virtual Excursion Excursion { get; set; }
        
        ////TODO: do it
        /// <summary>
        /// Check capacity of excursion
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int GetCapacity(DataContext context)
        {
            //TODO:CHECK again
            var person = context.Persons.FirstOrDefault(p => p.Id == PersonId) ?? Person;
            return person.BabyCount + person.AdultCount + person.ChildrenCount;
        }
    }
}