using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.ViewModels
{
    
    public class EditRoleViewModel
    {
        //initialize the List via constructor
        public EditRoleViewModel()
        {
            Users = new List<string>();
        }
        //You can`t edit Id ;We will edit the role based on the Id
        public string Id { get; set; }

        [Required(ErrorMessage ="Role name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

    }
}
