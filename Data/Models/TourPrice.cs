using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Models
{
    [Table("TourPrice")]
    public class TourPrice: Model
    {
        public TourPrice(Guid id, Guid tourId, Guid managerId, double price) : base(id)
        {
            TourId = tourId;
            ManagerId = managerId;
            Price = price;
        }

        public TourPrice()
        {
        }

        /// <summary>
        /// Link on the tour
        /// </summary>
        /// 
        [Required]
        [ForeignKey("Tour")]
        public Guid TourId { get; set; }
        public virtual Tour Tour { get; set; }

        /// <summary>
        /// Link on the manager
        /// </summary>
        /// 
        [Required]
        [ForeignKey("Manager")]
        public Guid ManagerId { get; set; }
        public virtual User Manager { get; set; }

        /// <summary>
        /// Info about price of the manager on the tour
        /// </summary>
        /// 
        [Required]
        public double Price { get; set; }
    }
}