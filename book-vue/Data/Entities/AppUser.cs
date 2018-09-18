using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace book_vue.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
    public AppUser(string firstName, string lastName) 
        {
          this.FirstName = firstName;
              this.LastName = lastName;
               
        }
                public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FullName{
            get {return $"{FirstName} ${LastName}";}
        }
    }
}