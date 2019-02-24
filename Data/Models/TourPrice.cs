using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("TourPrice")]
    public class TourPrice: Model
    {
        public TourPrice(Guid id, Tour tour, User user, double price) : base(id)
        {
            Tour = tour;
            User = user;
            Price = price;
        }

        public TourPrice()
        {
        }

        /// <summary>
        /// Link on the tour
        /// </summary>
        /// 
        public Guid? TourId { get; set; }
        [Required]
        [ForeignKey("TourId")]
        public virtual Tour Tour { get; set; }

        /// <summary>
        /// Link on the manager
        /// </summary>
        /// 
        public Guid? ManagerId { get; set; }
        [Required]
        [ForeignKey("ManagerId")]
        public User User { get; set; }

        /// <summary>
        /// Info about price of the manager on the tour
        /// </summary>
        /// 
        [Required]
        public double Price { get; set; }
    }
}