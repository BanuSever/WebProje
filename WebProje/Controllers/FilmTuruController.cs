using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProje.Models;
using WebProje.Models.Interfaces;
using WebProje.Utility;
using static System.Collections.Specialized.BitVector32;

namespace WebProje.Controllers
{
    [Authorize(Roles = UsersRole.AdminRol)]
    public class FilmTuruController : Controller
    {
        private readonly IFilmTuruRepository _turRepository;
        public FilmTuruController(IFilmTuruRepository turRepository)
        {
            _turRepository = turRepository;
        }

        public IActionResult Index()
        {
            List<FilmTuru> FilmTuruList = _turRepository.GetAll().ToList();
            return View(FilmTuruList);
        }
        public IActionResult FilmTuruEkle()
        {
            return View();
        }

        //Attribute, methodun sadece bir HTTP POST isteğiyle çağrılabileceğini belirtir
        [HttpPost]
        public IActionResult FilmTuruEkle(FilmTuru filmTuru)
        {
            //Bu kod sunucuda çalışıyor, ancak bu işlemi frontend'de yapmak istersek
            //js ile validation dosyasını kullanırız -> FilmTuruEkle.cshtml

            //    @section Scripts{
            //@{
            //    < partial name = "_ValidationScriptsPartial" />
            //}
            //    }

            if (ModelState.IsValid)
            {
                _turRepository.Ekle(filmTuru);
                _turRepository.Kaydet();
                return RedirectToAction("Index", "FilmTuru");
            }
            return View();
        }

        public IActionResult Guncelle(int? id) //? null da olabilir demek
        {
            if(id==null || id==0)
            {
                return NotFound();
            }

            // ? ile bu değer null da olabilir dedik
            FilmTuru? filmturu = _turRepository.Get(u=>u.TurID==id);// Expression, filtreleme
            if(filmturu == null)
            {
                return NotFound();
            }
            return View(filmturu);
        }

        [HttpPost]
        public IActionResult Guncelle(FilmTuru filmturu)
        {
            if (ModelState.IsValid)
            {
                _turRepository.Guncelle(filmturu);
                _turRepository.Kaydet();
                return RedirectToAction("Index", "FilmTuru");
            }
            // <input asp-for="TurID" hidden />  Guncelleme cshtml sayfasında bu kodu yazmazsak ID okuyamıyor
            return View();
        }

        // Get Action
        public IActionResult FilmTuruSil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            FilmTuru? filmturu = _turRepository.Get(u => u.TurID == id);
            if (filmturu == null)
            {
                return NotFound();
            }
            return View(filmturu);
        }

        [HttpPost, ActionName("FilmTuruSil")]
        public IActionResult FilmTuruSilPost(int? id)
        {
            FilmTuru? filmTuru = _turRepository.Get(u => u.TurID == id);
            if(filmTuru == null)
            {
                return NotFound();
            }
            _turRepository.Sil(filmTuru);
            _turRepository.Kaydet();
            return RedirectToAction("Index", "FilmTuru");
        }
    }
}
