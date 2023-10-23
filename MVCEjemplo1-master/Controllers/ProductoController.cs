using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ejemplo1.Utils;
using Ejemplo1.Models;
using Ejemplo1.Services;

namespace Ejemplo1.Controllers
{
    public class ProductoController : Controller
    {

        private readonly IAPIService _apiService;

        public ProductoController(IAPIService apiService)
        {
            _apiService = apiService;
        }


            // GET: ProductoController
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Producto> productos = await _apiService.GetProductos();
                return View(productos);

            }
            catch(Exception ex)
            {
                return View(new List<Producto>());

            }
           
        }

        // GET: ProductoController/Details/5
        public IActionResult Details(int IdProducto)
        {
            Producto producto = Utils.Utils.ListaProductos.Find(x => x.IdProducto == IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // GET: ProductoController/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            try
            {
                Producto createdProducto = await _apiService.PostProducto(producto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí si es necesario
                ViewBag.ErrorMessage = "Error al crear el producto.";
                return View();
            }
        }



        // GET: ProductoController/Edit/5
        public IActionResult Edit(int IdProducto)
        {
            Producto producto = Utils.Utils.ListaProductos.Find(x => x.IdProducto == IdProducto);
            if (producto != null)
            {
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            try
            {
                Producto updatedProducto = await _apiService.PutProducto(producto.IdProducto, producto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí si es necesario
                ViewBag.ErrorMessage = "Error al actualizar el producto.";
                return View();
            }
        }




        // GET: ProductoController/Delete/5
        public async Task<IActionResult> Delete(int IdProducto)
        {
            try
            {
                string result = await _apiService.DeleteProducto(IdProducto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí si es necesario
                ViewBag.ErrorMessage = "Error al eliminar el producto.";
                return RedirectToAction("Index");
            }
        }



    }
}
