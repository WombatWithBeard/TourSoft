using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    /// <summary>
    /// Hotel class: all info about hotels. using with excursion group persons
    /// </summary>
    [Table("Hotel")]
    public class Hotel: Model
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        public Hotel(Guid id, string name, string address, int phoneNumber) : base(id)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Hotel()
        {
        }

        /// <summary>
        /// Info about name of the hotel
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Info about address of the hotel
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Info about phone number of the hotel
        /// </summary>
        public long PhoneNumber { get; set; }
        
//        public ICollection<Person> Persons { get; set; }
    }
}