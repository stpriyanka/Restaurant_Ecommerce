using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ecomerce_Restaurant.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecomerce_Restaurant.Controllers
{
	public class SuperadminController : AccountController
	{
		// GET: Superadmin
		[Authorize(Roles = "SuperAdmin")]
		public ActionResult Index() 
		{
			var db = new ApplicationDbContext();
			//get users 
			List<ApplicationUser> users = db.Users.ToList();
			//----
			var roles = db.Roles.ToList();

			var userRolDictionary = new Dictionary<string, string>();
			foreach (var user in users)
			{
				var userRole = user.Roles.FirstOrDefault();
				if (userRole != null)
				{
					var userRoleId = userRole.RoleId;
					var roleName = roles.FirstOrDefault(x => x.Id == userRoleId);
					if (roleName != null) userRolDictionary.Add(user.UserName, roleName.Name);
				}
			}

			ViewBag.roles = new List<string>() { "General", "Admin" };
			return View(userRolDictionary);
		}


		[HttpPost]

		public async Task<ActionResult> updateRole(string userName, string new_role, string oldRole)
		{

			using (var db = new ApplicationDbContext())
			{
				var userid = db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();

				if (!await RoleManager.RoleExistsAsync(new_role))
				{
					var identityrole = new IdentityRole
					{
						Id = Guid.NewGuid().ToString(),
						Name = new_role
					};
					await RoleManager.CreateAsync(identityrole);
				}
				await UserManager.RemoveFromRoleAsync(userid, oldRole);
				await UserManager.AddToRoleAsync(userid, new_role);
			}
			return RedirectToAction("Index","Home");
		}
	}
}