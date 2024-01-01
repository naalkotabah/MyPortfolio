using Core.Entities;
using Core.Intrtfaces;
using Microsoft.AspNetCore.Mvc;
using porject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace porject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<Portfolioitem> _portfolio;

        public HomeController(IUnitOfWork<Owner> owner,IUnitOfWork<Portfolioitem> portfolio)
        {
           _owner = owner;
           _portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                Portfolioitems = _portfolio.Entity.GetAll().ToList()
           };

            return View(homeViewModel);
        }
    }
}
