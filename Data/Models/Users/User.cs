using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using ToursSoft.Data.Interface;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Models
{
    [Table("User")]
    public class User: Model
    {
        public User(Guid id, string name, string company, int phoneNumber, bool isAdmin) : base(id)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
        }

        public User()
        {
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// info about company name
        /// </summary>
        /// 
        public string Company { get; set; }

        /// <summary>
        /// User phone
        /// </summary>
        public int PhoneNumber { get; set; }
        
        /// <summary>
        /// attribute: 0 - manager, 1 - admin
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }
    }
}