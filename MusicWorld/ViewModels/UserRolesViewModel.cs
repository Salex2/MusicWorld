using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.ViewModels
{
    //one to many relationship from the user to the role
    public class UserRolesViewModel
    {
        //In the controller use VIEWBAG to pass UserId data to View instead of duplicate prop for UserId

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
