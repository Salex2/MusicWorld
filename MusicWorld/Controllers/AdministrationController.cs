using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.ViewModels;

namespace MusicWorld.Controllers
{
    [Route("Administration")]
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; //manage users:create ,delete etc
        private readonly RoleManager<IdentityRole> _roleManager; //manage roles:create, delete etc roles

        

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
          
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id={id} does not exist";
                return RedirectToAction("Error", "Administration");
            }
            else
            {
              var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                   return RedirectToAction("ListUsers", "Administration");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return RedirectToAction("ListUsers", "Administration");
        }




        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"User with id={id} does not exist";
                return RedirectToAction("Error", "Administration");
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return RedirectToAction("ListRoles", "Administration");
        }



        //Retrieves all users from identity Db
        [Route("ListUsers")]
        public IActionResult ListUsers()
        {
           var users = _userManager.Users;
            return View(users);
        }



        [Route("EditUser")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"User with id={id} does not exist";
                return RedirectToAction("Error", "Administration");
            }

            //retrieve the data for Claims and Roles
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(x => x.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }




        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id={model.Id} does not exist";
                return RedirectToAction("Error", "Administration");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);

            }
        } 
            
        



        [Route("CreateRole")]
        public IActionResult CreateRole()
        {

            return View();
        }

        



        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel createRole)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = createRole.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(" ", error.Description);
                }
            }


            return View(createRole);
        }




        [HttpGet]
        [Route("ListRoles")]
        public IActionResult ListRoles()
        {
            //from RoleManager we use Roles property to get the collection of roles
            var roles = _roleManager.Roles;

            return View(roles);
        }




        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }




        [Route("EditRole")]
        [HttpGet] //it will get the Id from the URL
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={id} cannot be found";
                return RedirectToAction("Error", "Administration");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name

            };

            //retrieve all the users from the database with userManager
            foreach (var user in _userManager.Users)
            {
                //here I check if the user that we are editing belongs to this role
                if (await _userManager.IsInRoleAsync(user, role.Name))//returns true or false if the user is in this user Role
                {
                    model.Users.Add(user.UserName); //add the user to the List<Users>
                }
            }


            return View(model);
        }


        //We need the Identity API UserMangerService to populate the list of users


        [Route("EditRole")]
        [HttpPost] //it will get the Id from the URL
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={model.Id} cannot be found";
                return RedirectToAction("Error", "Administration");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Adminstration");
                }
            }


            return View(model);
        }




        [HttpGet]
        [Route("EditUsersInRole")]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            //we store roleId in ViewBag so we can access it in the View when required
            ViewBag.RoleId = roleId;

            //using the roleId we retrieve the respective role from DB using RoleManager
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={roleId} cannot be found";
                return RedirectToAction("Error", "Administration");
            }

            var model = new List<UserRoleViewModel>();

            //_userManager.Users return a list of all registred users
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName

                };

                //check if the user is in a certain role;returns true if user  is in role
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }





        [HttpPost]
        [Route("EditUsersInRole")] //roleId parametres is coming from the URL
        public async Task<IActionResult> EditUserInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={roleId} cannot be found";
                return RedirectToAction("Error", "Administration");
            }

            //loop through each UserRoleViewModel
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                //add the user to the role only if the user is not already in that role
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);  //the user that we want to add and the roleId of the role we want to give
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue; // continue for going to the next user
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) // check if there are more users we need to update; check if they are checked-box
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = role.Id });

        }





        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        { 
            return View();
        }
    }
}