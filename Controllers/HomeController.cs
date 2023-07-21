using FullCalenderApp.Data;
using FullCalenderApp.Models;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult GetEvents()
        {
            var events = _context.Events.Select(e => new
            {
                eventId = e.EventId,
                title = e.Subject,
                start = e.Start,
                end = e.End,
                description = e.Description,
                themeColor = e.ThemeColor,
                isFullDay = e.IsFullDay,
              
            }).ToList();

            return Json(events);
        }


        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;

            try
            {
                if (e.EventId > 0)
                {
                    // Update the event
                    var v = _context.Events.Where(a => a.EventId == e.EventId).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    _context.Events.Add(e);
                }

                _context.SaveChanges();
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

            return Json(new { status = status, message = "Event saved successfully." });
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