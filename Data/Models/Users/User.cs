using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ToursSoft.Data.Interface;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Models
{
    [Table("User")]
    public class User: Model
    {
        private string _password;

        public User(Guid id, string name, string company, int phoneNumber, string login, string password, bool isAdmin) 
            : base(id)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            Login = login;
            Password = password;
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
        /// User login
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password
        {
            get => _password;
            set => _password = Convert.ToBase64String(MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// attribute: 0 - manager, 1 - admin
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }
    }
}