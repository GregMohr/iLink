using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using exam4.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace exam4.Controllers
{
    public class ProfileController : Controller
    {
        private Context _context;
        public ProfileController(Context context){
            _context = context;
        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            User sessionUser = _context.Users.Include(u => u.connections).ThenInclude(c => c.connection).Include(u => u.invites).ThenInclude(i => i.invited).Single(u => u.id == (int)HttpContext.Session.GetInt32("userId"));
            ViewBag.sessionUserName = sessionUser.first + " " + sessionUser.last;
            ViewBag.sessionUserDescription = sessionUser.description;

            IEnumerable<Connection> allConnections = _context.Connections.Include(c => c.user);
            foreach(Connection connection in allConnections){
                if(connection.connectionId == sessionUser.id){
                    sessionUser.connections.Add(connection);
                }
            }
            ViewBag.sessionUserConnections = sessionUser.connections;
            ViewBag.sessionUserInvites = _context.Invites.Include(i => i.user).Where(i => i.invitedId == sessionUser.id);
            ViewBag.sessionUserId = sessionUser.id;
            return View();
        }
        [HttpGet]
        [Route("sendInvite/{id}")]
        public IActionResult SendInvite(int id){
            //if invite exists going the other direction, autoconnect, although anyone that has sent an invite should not show up on the invited's other users list
            Invite newInvite = new Invite();
            newInvite.invitedId = id;
            newInvite.userId = (int)HttpContext.Session.GetInt32("userId");
            _context.Add(newInvite);
            _context.SaveChanges();
            return RedirectToAction("AllUsers", "Login");
        }
        [HttpGet]
        [Route("acceptInvite/{id}")]
        public IActionResult AcceptInvite(int id){
            Invite accepted = _context.Invites.Single(i => i.id == id);
            Connection newConnection = new Connection();
            newConnection.userId = accepted.userId;
            newConnection.connectionId = accepted.invitedId;
            _context.Add(newConnection);
            _context.Remove(accepted);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }
        [HttpGet]
        [Route("ignoreInvite/{id}")]
        public IActionResult IgnoreInvite(int id){
            _context.Remove(_context.Invites.Single(i => i.id == id));
            return RedirectToAction("Profile");
        }
    }
}
