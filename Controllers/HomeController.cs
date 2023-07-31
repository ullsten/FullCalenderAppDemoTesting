using FullCalenderApp.Data;
using FullCalenderApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Select(e => new
            {
                eventId = e.EventId,
                title = e.Subject,
                start = e.Start,
                end = e.End,
                description = e.Description,
                themeColor = e.ThemeColor,
                isFullDay = e.IsFullDay,
              
            }).ToListAsync();

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> SaveEvent(Event e)
        {
            var status = false;

            try
            {
                if (e.EventId > 0)
                {
                    // Update the event
                    var v = await _context.Events.Where(a => a.EventId == e.EventId).FirstOrDefaultAsync();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                        v.Start = e.Start;
                        v.End = e.End ?? e.Start;
                    }
                }
                else
                {
                    _context.Events.Add(e);
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
        public async Task<IActionResult> UpdateEvent(Event e)
        {
            var status = false;

            try
            {
                if (e.EventId > 0)
                {
                    // Update the event
                    var existingEvent = await _context.Events.FindAsync(e.EventId);
                    if (existingEvent != null)
                    {
                        existingEvent.Subject = e.Subject;
                        existingEvent.Start = e.Start;
                        existingEvent.End = e.End;
                        existingEvent.Description = e.Description;
                        existingEvent.IsFullDay = e.IsFullDay;
                        existingEvent.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    await _context.Events.AddAsync(e);
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

            //return Json(new { status = status, message = "Event saved successfully." });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;

            var v = _context.Events.Where(a => a.EventId == eventID).FirstOrDefault();
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