using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToursSoft.Data.Models
{
    [Table("Person")]
    public class Person: Model
    {
        public Person(Guid id, int adultCount, int childrenCount, int babyCount, double prepayment, double leftToPay, 
            bool photosession, string comment, Guid hotelGuid, List<string> hotelRoom, 
            DateTime pickUpTime, Guid managerId) : base(id)
        {
            AdultCount = adultCount;
            ChildrenCount = childrenCount;
            BabyCount = babyCount;
            Prepayment = prepayment;
            LeftToPay = leftToPay;
            Photosession = photosession;
            Comment = comment;
            HotelGuid = hotelGuid;
            HotelRoom = hotelRoom;
            PickUpTime = pickUpTime;
            ManagerId = managerId;
        }

        public Person()
        {
        }
        //TO DO:
        
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
        public Guid HotelGuid { get; set; }

        /// <summary>
        /// Room(s) of these persons
        /// </summary>
        public List<string> HotelRoom { get; set; }

        /// <summary>
        /// Time, when need to pick up mates
        /// </summary>
        public DateTime PickUpTime { get; set; }

        /// <summary>
        /// Link to the manager
        /// </summary>
        public Guid ManagerId { get; set; }
    }
}