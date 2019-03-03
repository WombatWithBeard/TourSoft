using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("Hotel")]
    public class Hotel: Model
    {
        public Hotel(Guid id, string name, string address, int phoneNumber) : base(id)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }

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
    }
}