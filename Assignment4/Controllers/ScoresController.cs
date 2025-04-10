using Assignment4.Models;
using Microsoft.AspNetCore.Mvc;
namespace Assignment4.Controllers
{
    public class ScoresController : Controller
    {
        private readonly QuizContext _context;

        public ScoresController(QuizContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var scores = _context.QuizResults
                .OrderByDescending(s => s.CompletedAt)
                .ToList();

            return View(scores);
        }
    }
}