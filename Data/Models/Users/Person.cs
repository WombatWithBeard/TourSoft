using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models.Users
{
    [Table("Person")]
    public class Person: Model
    {
        public Person(Guid id, int adultCount, int childrenCount, int babyCount, double prepayment, double leftToPay, 
            bool photosession, string comment, Guid hotelId, List<string> hotelRoom, DateTime pickUpTime) : base(id)
        {
            AdultCount = adultCount;
            ChildrenCount = childrenCount;
            BabyCount = babyCount;
            Prepayment = prepayment;
            LeftToPay = leftToPay;
            Photosession = photosession;
            Comment = comment;
            HotelId = hotelId;
            HotelRoom = hotelRoom;
            PickUpTime = pickUpTime;
        }

        public Person()
        {
        }
        
        /// <summary>
        /// Info about the number of adult persons
        /// </summary>
        public int AdultCount { get; set; }

        /// <summary>
        /// Info about the number of childrens
        /// </summary>
        public int ChildrenCount { get; set; }
        
        /// <summary>
        /// Info about the number of babies
        /// </summary>
        public int BabyCount { get; set; }

        /// <summary>
        /// Summ of prepayment of these persons
        /// </summary>
        public double Prepayment { get; set; }

        /// <summary>
        /// Debt information of these persons
        /// </summary>
        public double LeftToPay { get; set; }

        /// <summary>
        /// Info about photosession: 0 -refused , 1 - agreed
        /// </summary>
        public bool Photosession { get; set; }

        /// <summary>
        /// Some info about these persons
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Info about hotel of these persons
        /// </summary>
        [ForeignKey("Hotel")]
        [Required]
        public Guid HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }

        /// <summary>
        /// Room(s) of these persons
        /// </summary>
        public List<string> HotelRoom { get; set; }

        /// <summary>
        /// Time, when need to pick up mates
        /// </summary>
        public DateTime PickUpTime { get; set; }
    }
}