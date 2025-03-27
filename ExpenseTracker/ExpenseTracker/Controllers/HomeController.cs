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
            List<Expense> allExpenses = _dbContext.Expenses.ToList();
            return View(allExpenses);
        }

        public IActionResult CreateEdit(int? id)
        {
            if(id != null)
            {
                List<Expense> allExpenses = _dbContext.Expenses.ToList();
                var myExpense = allExpenses.Where(expense => expense.Id == id).FirstOrDefault();
                return View(myExpense);
            }
            
            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            List<Expense> allExpenses = _dbContext.Expenses.ToList();

            var expenseItem = allExpenses.Where(expense => expense.Id == id).FirstOrDefault();

            if (expenseItem != null)
            {
                _dbContext.Expenses.Remove(expenseItem);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditPost(Expense expense)
        {
            if (expense.Id != 0)
            {
                _dbContext.Expenses.Update(expense);
            }
            else
            {
                _dbContext.Expenses.Add(expense);
            }
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
