using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ToursSoft.Data.Models.Users
{
    [Table("UserRole")]
    public class UserRole: Model
    {
        public UserRole(Guid id, Guid roleId, Guid userId): base(id)
        {
            RoleId = roleId;
            UserId = userId;
        }

        [ForeignKey("Role")]
        [Required]
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        [ForeignKey("User")]
        [Required]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}