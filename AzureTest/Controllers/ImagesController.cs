using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureTest.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        public ImageStore imageStore { get; set; }

        public ImagesController(ImageStore store)
        {
            this.imageStore = store;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if (image != null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await imageStore.SaveImage(stream);
                    return RedirectToAction("Show", new { imageId});
                }
            }


            return View();
        }


        [HttpGet("{imageId}")]
        public IActionResult Show(string imageId)
        {
            var model = new ShowModel() { Uri = imageStore.UriFor(imageId) };
            return View(model);
        }

    }
}