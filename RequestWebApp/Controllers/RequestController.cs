using RequestWebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RequestWebApp.Controllers
{
    public class RequestController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public RequestController()
        {
            _dbContext = new DbContextFactory().Create();
        }
        [HttpGet]
        [Route("Request/{SubmitRequest}")]
        public ActionResult SubmitRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitRequest(Request request)
        {
            if(ModelState.IsValid)
            {
                request.CreatedAt = DateTime.Now;
                request.Status = 0;
                request.UpdatedAt = null;
                _dbContext.Requests.Add(request);
                _dbContext.SaveChanges();
                return RedirectToAction("Confirmation", "Home");
                
            }
            return View(request);
        }
        public ActionResult MyRequests()
        {
            var userRequests = _dbContext.Requests
                                    .OrderByDescending(r => r.CreatedAt)
                                    .ToList();

            return View(userRequests);
        }
    }
}