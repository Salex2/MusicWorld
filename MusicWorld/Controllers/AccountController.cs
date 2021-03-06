﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Models;

namespace MusicWorld.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;  //create,delete,update async etc
        private readonly SignInManager<IdentityUser> signInManager; //signIn, signOut, IsSignedIn  async etc

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

             var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) //check if the user is created
                {
                    if(signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                  await  signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);  //Will be displayed in Register view validation-summarry errors
                }

            }

            return View(model);

        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password,
                                         model.RememberMe, false);

                if (result.Succeeded) 
                {
                    
                    return RedirectToAction("Index", "Home");
                }

            
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");  
                

            }

            return View(model);

        }
    }
}
