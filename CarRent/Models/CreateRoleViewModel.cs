using CarRent.Common;
using System.ComponentModel.DataAnnotations;

namespace CarRent.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public RoleType RoleName { get; set; }
    }
}
