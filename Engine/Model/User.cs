using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Engine.Model
{
    public class User : IIdentity, IModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [BindNever]
        public string Password { get; set; }

        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}