using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProje.Models;
using WebProje.Models.Interfaces;
using WebProje.Utility;

namespace WebProje.Controllers
{
    //Sadece adminler bu kısıma erişebilir
    [Authorize(Roles = UsersRole.AdminRol)]
    public class FilmController : Controller
    {
        private readonly IFilmRepository _filmRepository;
        //Bir controller'da birden fazla repository kullanmak
        private readonly IFilmTuruRepository _filmTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public FilmController(IFilmRepository filmRepository, IFilmTuruRepository filmTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _filmRepository = filmRepository;
            _filmTuruRepository = filmTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Film> FilmList = _filmRepository.GetAll(includeProps:"FilmTuru").ToList();
            return View(FilmList);
        }

        public IActionResult FilmEkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> FilmTuruList = _filmTuruRepository.GetAll()
            .Select(k => new SelectListItem //Film türünü filmturu repo'dan çekme ve listeye atma
            {
                Text = k.TurAdi,
                Value = k.TurID.ToString()
            });
            ViewBag.FilmTuruList = FilmTuruList;

            if (id==null || id==0)
            {
                return View();
            }
            else
            {
                // ? ile bu değer null da olabilir dedik
                Film? film = _filmRepository.Get(u => u.FilmID == id);// Expression, filtreleme
                if (film == null)
                {
                    return NotFound();
                }
                return View(film);
            }
        }

        //Attribute, methodun sadece bir HTTP POST isteğiyle çağrılabileceğini belirtir
        [HttpPost]
        public IActionResult FilmEkleGuncelle(Film film, IFormFile? file)
        {
            //Bu kod sunucuda çalışıyor, ancak bu işlemi frontend'de yapmak istersek
            //js ile validation dosyasını kullanırız -> FilmTuruEkle.cshtml

            //@section Scripts{
            //  @{
            //      < partial name = "_ValidationScriptsPartial" />
            //  }
            //}
            //hataların ne olduğunu görmek için kullanırız
            var errors = ModelState.Values.SelectMany(x => x.Errors).ToList();

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string filmPath = Path.Combine(wwwRootPath, @"posters");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(filmPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    film.PosterUrl = @"\posters\" + file.FileName;
                }


                if (film.FilmID == 0)
                {
                    _filmRepository.Ekle(film);
                }
                else
                {
                    _filmRepository.Guncelle(film);
                }
                _filmRepository.Kaydet();
                return RedirectToAction("Index", "Film");
            }
            return View();
        }

        /*
        public IActionResult FilmGuncelle(int? id) //? null da olabilir demek
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);

            if (id == null || id == 0)
            {
                return NotFound();
            }
            // ? ile bu değer null da olabilir dedik
            Film? film = _filmRepository.Get(u => u.FilmID == id);// Expression, filtreleme
            if (film == null)
            {
                return NotFound();
            }
            return View(film);

        }

        [HttpPost]
        public IActionResult FilmGuncelle(Film film)
        {
            if (ModelState.IsValid)
            {
                _filmRepository.Guncelle(film);
                _filmRepository.Kaydet();
                return RedirectToAction("Index", "Film");
            }
            return View();
        }
        */

        // Get Action
        public IActionResult FilmSil(int? id)
        {
            IEnumerable<SelectListItem> FilmTuruList = _filmTuruRepository.GetAll()
            .Select(k => new SelectListItem //Film türünü filmturu repo'dan çekme ve listeye atma
            {
                Text = k.TurAdi,
                Value = k.TurID.ToString()
            });
            ViewBag.FilmTuruList = FilmTuruList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Film? film = _filmRepository.Get(u => u.FilmID == id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        [HttpPost, ActionName("FilmSil")]
        public IActionResult FilmSilPost(int? id)
        {
            Film? film = _filmRepository.Get(u => u.FilmID == id);
            if (film == null)
            {
                return NotFound();
            }
            _filmRepository.Sil(film);
            _filmRepository.Kaydet();
            return RedirectToAction("Index", "Film");
        }
    }
}
