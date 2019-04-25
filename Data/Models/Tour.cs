using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    /// <summary>
    /// Tour class: this class about info of tours
    /// </summary>
    [Table("Tour")]
    public class Tour: Model
    {
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="capacity"></param>
        /// <param name="description"></param>
        public Tour(Guid id, string name, int capacity, string description) : base(id)
        {
            Name = name;
            Capacity = capacity;
            Description = description;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Tour()
        {
        }

        /// <summary>
        /// Info about name of the tour
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Info about tour persons capacity
        /// </summary>
        [Required]
        public int Capacity { get; set; }

        /// <summary>
        /// Info about tour
        /// </summary>
        public string Description { get; set; }
        
//        public ICollection<Excursion> Excursions { get; set; }
//        public ICollection<TourPrice> TourPrices { get; set; }
    }
}