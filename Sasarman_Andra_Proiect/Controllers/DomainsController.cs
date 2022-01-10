using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sasarman_Andra_Proiect.Data;
using Sasarman_Andra_Proiect.Models;
using Sasarman_Andra_Proiect.Models.LibraryViewModels;


namespace Sasarman_Andra_Proiect.Controllers
{
    [Authorize(Policy = "OnlyAdmin")]
    public class DomainsController : Controller
    {
        private readonly LibraryContext _context;

        public DomainsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Domains
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            var viewModel = new DomainIndexData();
            viewModel.Domains = await _context.Domains
            .Include(i => i.PublishedCourses)
            .ThenInclude(i => i.Course)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.DomainName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["DomainID"] = id.Value;
                Domain domain = viewModel.Domains.Where(
                i => i.ID == id.Value).Single();
                viewModel.Courses = domain.PublishedCourses.Select(s => s.Course);
            }
            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                viewModel.Orders = viewModel.Courses.Where(
                x => x.ID == courseID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Domains/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domains
                .FirstOrDefaultAsync(m => m.ID == id);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        // GET: Domains/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Domains/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DomainName,Description")] Domain domain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(domain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(domain);
        }

        // GET: Domains/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domains
                .Include(i => i.PublishedCourses).ThenInclude(i => i.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (domain == null)
            {
                return NotFound();
            }
            PopulatePublishedCourseData(domain);
            return View(domain);
        }
        private void PopulatePublishedCourseData(Domain domain)
        {
            var allCourses = _context.Courses;
            var publishedCourses = new HashSet<int>(domain.PublishedCourses.Select(c => c.CourseID));
            var viewModel = new List<PublishedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new PublishedCourseData
                {
                    CourseID = course.ID,
                    Title = course.Title,
                    IsPublished = publishedCourses.Contains(course.ID)
                });
            }
            ViewData["Courses"] = viewModel;
        }


        // POST: Domains/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }
            var domainToUpdate = await _context.Domains
             .Include(i => i.PublishedCourses)
             .ThenInclude(i => i.Course)
             .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Domain>(domainToUpdate, "", i => i.DomainName, i => i.Description))
            {
                UpdatePublishedCourses(selectedCourses, domainToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedCourses(selectedCourses, domainToUpdate);
            PopulatePublishedCourseData(domainToUpdate);
            return View(domainToUpdate);
        }
        private void UpdatePublishedCourses(string[] selectedCourses, Domain domainToUpdate)
        {
            if (selectedCourses == null)
            {
                domainToUpdate.PublishedCourses = new List<PublishedCourse>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var publishedCourses = new HashSet<int>
            (domainToUpdate.PublishedCourses.Select(c => c.Course.ID));
            foreach (var course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.ID.ToString()))
                {
                    if (!publishedCourses.Contains(course.ID))
                    {
                        domainToUpdate.PublishedCourses.Add(new PublishedCourse
                        {
                            DomainID =
                       domainToUpdate.ID,
                            CourseID = course.ID
                        });
                    }
                }
                else
                {
                    if (publishedCourses.Contains(course.ID))
                    {
                        PublishedCourse bookToRemove = domainToUpdate.PublishedCourses.FirstOrDefault(i
                       => i.CourseID == course.ID);
                        _context.Remove(bookToRemove);
                    }
                }

            }
        }
        // GET: Domains/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await _context.Domains
                .FirstOrDefaultAsync(m => m.ID == id);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        // POST: Domains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domain = await _context.Domains.FindAsync(id);
            _context.Domains.Remove(domain);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DomainExists(int id)
        {
            return _context.Domains.Any(e => e.ID == id);
        }
    }
}
