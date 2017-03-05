using Microsoft.AspNetCore.Mvc;
using exam4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace exam4.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;
        public LoginController(Context context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.view = "Login";
            ViewBag.other = "Register";
            ViewBag.showLink = "showRegister";
            return View();
        }
        [HttpGet]
        [Route("showRegister")]
        public IActionResult ShowRegister()
        {
            ViewBag.view = "Register";
            ViewBag.other = "Login";
            ViewBag.showLink = "showLogin";
            return View("Index");
        }
        [HttpGet]
        [Route("showLogin")]
        public IActionResult ShowLogin()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password){
            User user = _context.Users.SingleOrDefault(userA => userA.email == email);

            if(user != null && password != null && password.Length > 7)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(user, user.password, password))
                {
                    HttpContext.Session.SetInt32("userId", user.id);
                    return RedirectToAction("Profile", "Profile");
                }
            }
            ViewBag.view = "Login";
            ViewBag.other = "Register";
            ViewBag.showLink = "showRegister";
            ViewBag.confirmFail = "Username or password does not match.";
            return View("Index");
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user, string confirm){
            var emailCheck = _context.Users.SingleOrDefault(firstUser => firstUser.email == user.email);

            if(user.password != confirm || !ModelState.IsValid || emailCheck != null){
                ViewBag.view = "Register";
                ViewBag.other = "Login";
                ViewBag.showLink = "showLogin";
                if(user.password != confirm){
                    ViewBag.confirmFail = "Passwords do not match.";
                }
                else if (emailCheck != null){
                    ViewBag.confirmFail = "Email already registered";
                }
                return View("Index");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.password = Hasher.HashPassword(user, user.password);
            user.updated_at = DateTime.Now;
            _context.Add(user);            
            _context.SaveChanges();
            HttpContext.Session.SetInt32("userId", user.id);
            return RedirectToAction("Profile", "Profile");
        }
        [HttpGet]
        [Route("showUser/{id}")]
        public IActionResult ShowUser(int id){
            User showUser = _context.Users.Single(user => user.id == id);
            ViewBag.showUserName = showUser.first + " " + showUser.last;
            ViewBag.showUserDescription = showUser.description;
            return View();
        }
        [HttpGet]
        [Route("allUsers")]
        public IActionResult AllUsers(int id){
            User sessionUser = _context.Users.Single(u => u.id == HttpContext.Session.GetInt32("userId"));

            //All users excluding sessionUser
            IEnumerable<User> allOtherUsers = _context.Users.Where(u => u.id != sessionUser.id);

            //Connections involving sessionUser
            IEnumerable<Connection> sessionUserConnections = _context.Connections
                .Include(c => c.user)
                .Include(c => c.connection)
                .Where(c => c.userId == sessionUser.id || c.connectionId == sessionUser.id);

            //Invites involving sessionUser
            IEnumerable<Invite> sessionUserInvites = _context.Invites
                .Include(i => i.user)
                .Include(i => i.invited)
                .Where(i => i.userId == sessionUser.id || i.invitedId == sessionUser.id);
            
            List<User> knownUsers = new List<User>();

            //Add sessionUserConnections that are NOT sessionUser to knownUsers
            foreach(Connection connection in sessionUserConnections){
                if(connection.userId == sessionUser.id){
                    knownUsers.Add(connection.connection);
                } else {
                    knownUsers.Add(connection.user);               
                }
            }

            //Add sessionUserConnections that are NOT sessionUser to knownUsers            
            foreach(Invite invite in sessionUserInvites){
                if(invite.userId == sessionUser.id){
                    knownUsers.Add(invite.invited);
                } else {
                    knownUsers.Add(invite.user);               
                }
            }

            IEnumerable<User> notKnownUsers = allOtherUsers.Except(knownUsers);

            //Cannot just pass full name as id is need to go to show 1 user page. Should I use a dictionary with id key?
            List<UserNameDisplay> notKnownUserNames = new List<UserNameDisplay>();
            
            foreach(User user in notKnownUsers){
                notKnownUserNames.Add(new UserNameDisplay(user.id, user.first + " " + user.last));
            }

            ViewBag.notKnownUsers = notKnownUserNames;
            return View();
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
