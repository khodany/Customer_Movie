using Customer_Movie.Models;
using Customer_Movie.Repos;
using Customer_Movie.ViewModels;
using CustomerMoviess.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;


namespace Customer_Movie.Controllers
{
    public class CustomersController : Controller
    {
        private Icustomer _customer;
        public CustomersController()
        {
            _customer = new CustomerRepository(new ApplicationDbContext());
        }

        public ActionResult New()
        {
            var membershipTypes = _customer.GetMemberShipType();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _customer.GetMemberShipType()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                _customer.SaveCustomer(customer);
            else
                _customer.updateCustomer(customer);

            return RedirectToAction("Index", "Customers");
        }

        public ViewResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _customer.GetCustomerById(id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _customer.GetCustomerByIdUpdate(id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _customer.GetMemberShipType()
            };

            return View("CustomerForm", viewModel);
        }
    }
}