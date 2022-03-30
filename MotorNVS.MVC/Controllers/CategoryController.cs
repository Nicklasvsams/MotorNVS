using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.CategoryDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            List<CategoryResponse> responses = new List<CategoryResponse>();

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"];
            };

            try
            {
                responses = await _categoryService.GetAllCategories();

                return View(responses);
            }
            catch
            {
                return View(responses);
            };
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.CreateCategory(req);

                    TempData["shortMessage"] = "Entry succesfully created!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.Message = "An error occured, please try again.";

                    return View();
                };
            };

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                CategoryResponse res = await _categoryService.GetCategoryById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured trying to fetch the entry, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategory(id, req);

                    TempData["shortMessage"] = "Entry has been succesfully updated!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            };

            try
            {
                CategoryResponse res = await _categoryService.GetCategoryById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }
    }
}
