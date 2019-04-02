using Microsoft.AspNetCore.Identity;

namespace book_vue.Data.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole(){}

        public AppRole(string name){
            Name = name;
        }
    }
}