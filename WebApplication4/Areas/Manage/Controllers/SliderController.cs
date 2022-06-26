using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.DAL;
using WebApplication4.Models;
using WebApplication4.Utilites;

namespace WebApplication4.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;


        }
        public IActionResult Index()
        {
           List< Slider> slider = _context.sliders.ToList();
            return View(slider);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Modelstate is valid xetasi");

                return View();


            }
            bool check=_context.sliders.Any(x=>x.Name==slider.Name);
            if (check)
            {
                ModelState.AddModelError("", "Eyni adda insan yarada bilmezsen");
                return View();

            }
            if (slider.Photo!=null)
            {
                if (!slider.Photo.CheckSize(200))
                {
                    ModelState.AddModelError("Photo", "200kb limiti asilib");
                    return View();


                }
                if (!slider.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "ig formatinda bir sey at  ");
                    return View();


                }

                slider.ImgUrl = await slider.Photo.SavaFileAsync(Path.Combine(_env.WebRootPath, "assest", "image"));

            }
            else
            {
                ModelState.AddModelError("Photo", "sekil bosdu");
                return View();
            }
           
            _context.sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            Slider s = _context.sliders.Find(Id);
            if (s == null)
            {
                return BadRequest();

            }
            return View(s);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id, Slider s)
        {

            Slider s1 = _context.sliders.Find(Id);
            if (s1 == null)
            {
                return BadRequest();

            }

            s1.Degre = s.Degre;
            s1.ImgUrl = s.ImgUrl;
            s1.Name = s.Name;
            s1.isDelete = s.isDelete;
            s1.Photo = s.Photo;
            if (s1.Photo != null)
            {

                if (!s.Photo.CheckSize(200))
                {
                    ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                    return View();
                }
                if (!s.Photo.CheckType("image/"))
                {
                    ModelState.AddModelError("Photo", "photo limini 200 kb kecib");
                    return View();


                }
                s.ImgUrl = await s.Photo.SavaFileAsync(Path.Combine(_env.WebRootPath, "assest", "image"));


            }
            else
            {
                ModelState.AddModelError("Photo", "sekil elave edin");
                return View();
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            Slider slider = _context.sliders.Find(Id);
            if (slider == null)
            {
                return NotFound();

            }
            _context.sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
