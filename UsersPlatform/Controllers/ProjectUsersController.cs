using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UsersPlatform.Areas.Identity.Data;
using UsersPlatform.Data;
using UsersPlatform.Models;

namespace UsersPlatform.Controllers
{
    [Authorize]
    public class ProjectUsersController : Controller
    {
        private readonly UsersPlatformContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string projectName { get; set; }
        public ProjectUsersController(UsersPlatformContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ProjectUsers
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userID =  _userManager.GetUserId(User);

            UserId = userID;

            var usersPlatformContext = _context.ProjectUser.Include(p => p.Project).Include(p => p.User);
            var usersProjectList = await usersPlatformContext.ToListAsync();
            List<AssignedProjects> assignedProjects = new List<AssignedProjects>();
            return View(await usersPlatformContext.ToListAsync());
        }

        // GET: ProjectUsers/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUser
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // GET: ProjectUsers/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "ID", "ID");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");

            var user =  _userManager.GetUserAsync(User);
            var userN =  _userManager.GetUserName(User);

            UserName = userN;


            //ViewData["UserName"] = new SelectList(_context.Project, "ProjectName", "ProjectName");
            //ViewData["ProjectName"] = new SelectList(_context.Users, "ProjectName", "ProjectName");
            return View(); 
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ProjectId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ID", "ID", projectUser.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectUser.UserId);

            var user = await _userManager.GetUserAsync(User);
            var userN = await _userManager.GetUserNameAsync(user);

            UserName = userN;

            return View(projectUser);
        }

        // GET: ProjectUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUser.FindAsync(id);
            if (projectUser == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ID", "ID", projectUser.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectUser.UserId);
            return View(projectUser);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,ProjectId")] ProjectUser projectUser)
        {
            if (id != projectUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectUserExists(projectUser.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ID", "ID", projectUser.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectUser = await _context.ProjectUser
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (projectUser == null)
            {
                return NotFound();
            }

            return View(projectUser);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var projectUser = await _context.ProjectUser.FindAsync(id);
            _context.ProjectUser.Remove(projectUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectUserExists(string id)
        {
            return _context.ProjectUser.Any(e => e.UserId == id);
        }
    }
}
