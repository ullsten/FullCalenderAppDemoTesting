using FullCalenderApp.Data;
using FullCalenderApp.Models;
using FullCalenderApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FullCalenderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var colors = await _context.Colors.ToListAsync();
            ViewBag.ColorsList = new SelectList(colors, "ColorName", "ColorName");
            return View();
        }

        public async Task<ActionResult<EventGetViewModel>> GetEvents()
        {
            var events = await _context.Events
                .Select(e => new EventGetViewModel
                {
                    EventId = e.EventId,
                    Title = e.Subject,
                    Start = e.Start,
                    End = e.End,
                    Description = e.Description,
                    ThemeColor = e.ThemeColor,
                    IsFullDay = e.IsFullDay,
              
                }).ToListAsync();

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent(SaveEventViewModel model)
        {
            var status = false;

            try
            {
                if (model.EventId > 0)
                {
                    // Update the event
                    var v = await _context.Events.Where(a => a.EventId == model.EventId).FirstOrDefaultAsync();
                    if (v != null)
                    {
                        v.Subject = model.Subject;
                        v.Description = model.Description;
                        v.IsFullDay = model.IsFullDay;
                        v.ThemeColor = model.ThemeColor;
                        v.Start = model.Start;
                        v.End = model.End ?? model.Start;
                    }
                }
                else
                {
                    var newEvent = new Event
                    {
                        Subject = model.Subject,
                        Description = model.Description,
                        IsFullDay = model.IsFullDay,
                        ThemeColor = model.ThemeColor,
                        Start = model.Start,
                        End = model.End ?? model.Start
                    };

                    _context.Events.Add(newEvent);
                }

                await _context.SaveChangesAsync();
                status = true;
            }
            catch (Exception ex)
            {
                // Handle the exception
                // You can log the exception or provide an error message to the user
                // For example, you can set status to false and return an error message
                status = false;
                return Json(new { status = status, message = "An error occurred while saving the event." });
            }

            // return Json(new { status = status, message = "Event saved successfully." });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteEvent(DeleteEventViewModel model)
        {
            var status = false;

            var v = _context.Events.Where(a => a.EventId == model.EventId).FirstOrDefault();
            if (v != null)
            {
                _context.Events.Remove(v);
                _context.SaveChanges();
                status = true;
            }

            return Json(new { status = status, message = "Event deleted successfully." });
        }
    }
}