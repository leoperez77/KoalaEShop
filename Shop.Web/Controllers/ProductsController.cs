using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Entities;
using Shop.Web.Helpers;
using Shop.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly IUserHelper userHelper;

        public IProductRepository productRepository { get; }

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper )
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await productRepository.GetByIdAsync(id.GetValueOrDefault());
            
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", view.ImageFile.FileName);
                    path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Products",
                            file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }
                    //path = $"~/images/products/{view.ImageFile.FileName}";
                    path = $"~/images/Products/{file}";
                }

                var product = this.ToProduct(view, path);

                
                product.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await productRepository.CreateAsync(view);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }



        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await productRepository.GetByIdAsync(id.GetValueOrDefault());
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var view = this.ToProductViewModel(product);
            return View(view);
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel view)
        {
            if (id != view.Id)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", view.ImageFile.FileName);
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }                        

                        //path = $"~/images/products/{view.ImageFile.FileName}";
                        path = $"~/images/Products/{file}";
                    }

                    var product = ToProduct(view, path);
                    await productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await productRepository.ExistAsync(view.Id))
                    {
                        return new NotFoundViewResult("ProductNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await productRepository.GetByIdAsync(id.Value);
                        
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            await productRepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailabe = product.IsAvailabe,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };

        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            var p = view as Product;
            p.ImageUrl = path;
            return p;
        }

        public IActionResult ProductNotFound()
        {
            return this.View();
        }

    }
}
