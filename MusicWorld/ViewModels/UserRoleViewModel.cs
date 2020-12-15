
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.ViewModels
{
    //we need roleId and UserId for giving certain users a role <->;
    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set;}
        public bool IsSelected { get; set; }
    }
}
