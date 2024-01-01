using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using porject.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace porject.Controllers
{
    public class PortfolioitemsController : Controller
    {
        private readonly IUnitOfWork<Portfolioitem> _portfolio;
        private readonly IHostingEnvironment _hosting;

        public PortfolioitemsController(IUnitOfWork<Portfolioitem> portfolio,IHostingEnvironment hosting)
        {
            _portfolio = portfolio;
            _hosting = hosting;
        }

        // GET: Portfolioitems
        public IActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: Portfolioitems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioitem = _portfolio.Entity.GetById(id);
            if (portfolioitem == null)
            {
                return NotFound();
            }

            return View(portfolioitem);
        }

        // GET: Portfolioitems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Portfolioitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                Portfolioitem portfolioitem = new Portfolioitem
                {
                   
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portfolio.Entity.Insert(portfolioitem);
                _portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Portfolioitems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioitem = _portfolio.Entity.GetById(id);
            if (portfolioitem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = portfolioitem.Id,
                Description = portfolioitem.Description,
                ImageUrl = portfolioitem.ImageUrl,
                ProjectName= portfolioitem.ProjectName

            };  
 


            return View(portfolioViewModel);
        }

        // POST: Portfolioitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    Portfolioitem portfolioitem = new Portfolioitem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portfolio.Entity.Update(portfolioitem);
                    _portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioitemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Portfolioitems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioitem = _portfolio.Entity.GetById(id);
            if (portfolioitem == null)
            {
                return NotFound();
            }

            return View(portfolioitem);
        }

        // POST: Portfolioitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioitemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
