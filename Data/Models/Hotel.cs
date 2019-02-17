using System;
using System.ComponentModel.DataAnnotations;

namespace ToursSoft.Data.Models
{
    public class Hotel: Model
    {
        public Hotel(Guid id, string name, string address, int phoneNumber) : base(id)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
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
        public int PhoneNumber { get; set; }
    }
}