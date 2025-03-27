using System.Diagnostics;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpenseTrackerDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ExpenseTrackerDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _dbContext.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEdit()
        {
            return View();
        }

        public IActionResult CreateEditPost(Expense expense)
        {
            _dbContext.Expenses.Add(expense);
            _dbContext.SaveChanges();

            return RedirectToAction("Expenses");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
