using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.UI.Pages.Account.Manage.Internal;
using ToursSoft.Data.Models.Models;

namespace ToursSoft.Data.Models
{
    public abstract class User: Model
    {
        protected User(Guid id = default(Guid), string name = null, string company = null, int phoneNumber = 0, bool isAdmin = false) : base(id)
        {
            Name = name;
            Company = company;
            PhoneNumber = phoneNumber;
            IsAdmin = isAdmin;
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// Просмотр экскурсий
        /// </summary>
        public abstract Dictionary<DateTime, Excursion> ExcursionViewing();

        /// <summary>
        /// Регистрация на экскурсию
        /// </summary>
        public abstract void ExcursionRegistration(Person person, Guid ExcursionId);
    }
}