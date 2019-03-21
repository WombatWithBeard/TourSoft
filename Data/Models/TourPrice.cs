using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Data.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Tourprice class: this class about price of different tours of specific user 
    /// </summary>
    [Table("TourPrice")]
    public class TourPrice: Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tourId"></param>
        /// <param name="userId"></param>
        /// <param name="price"></param>
        public TourPrice(Guid id, Guid tourId, Guid userId, double price) : base(id)
        {
            TourId = tourId;
            UserId = userId;
            Price = price;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TourPrice()
        {
        }

        /// <summary>
        /// Link on the tour
        /// </summary>
        [Required]
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }
        /// <summary>
        /// link on tour table
        /// </summary>
        public virtual Tour Tour { get; set; }

        /// <summary>
        /// Link on the manager
        /// </summary>
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        /// <summary>
        /// link on user table
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Info about price of the manager on the tour
        /// </summary>
        /// 
        [Required]
        public double Price { get; set; }
    }
}