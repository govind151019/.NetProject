using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using web_application_product.Models;
using web_application_product.Models.data;

namespace web_application_product.Controllers.Data.Models
{[Authorize]
    
    public class ProductsController : Controller
    {
        private readonly YourDbContext _context;
       

        public ProductsController(YourDbContext context) // (context) taking data from database
        {
            _context = context; //give to object _context
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.GProducts.ToListAsync());
        }

        public async Task<IActionResult> Electronics()
        {
            return View(await _context.GProducts.ToListAsync());
        }
        public async Task<IActionResult> Stationary()
        {
            return View(await _context.GProducts.ToListAsync());
        }
        public async Task<IActionResult> Apparels()
        {
            return View(await _context.GProducts.ToListAsync());
        }
      
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.GProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

                [HttpGet]
        public IActionResult Create()
        {
            var product = new Product { Id = 0, Name = "", Price = 0, Color = "", ProductCategories = new List<SelectListItem>() }; var categories = _context.ProductCategory.ToList();
            foreach (var item in categories)
            {
                    product.ProductCategories.Add(new SelectListItem
                    { Text = item.Pname, Value = Convert.ToString(item.Id) });
                
            }
            return View(product);
        }
      

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product products)
        {
           
           
                _context.Add(products);
                await _context.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
           
        }

       
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var products = await _context.GProducts.FindAsync(id);
            products.ProductCategories = new List<SelectListItem>();
            var categories = _context.ProductCategory.ToList();
            foreach (var item in categories)
            {
                    products.ProductCategories.Add(new SelectListItem
                    { Text = item.Pname, Value = Convert.ToString(item.Id) });
                
            }
            if (products == null)
            {
                return NotFound();
            }
            return View("~/Views/Products/Create.cshtml", products);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product products)
        {
           
            try
            {
                _context.Update(products);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(products.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.GProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            _context.GProducts.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ProductsExists(int id)
        {
            return _context.GProducts.Any(e => e.Id == id);
        }


        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
