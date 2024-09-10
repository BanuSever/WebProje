using System.Linq.Expressions;
using WebProje.Models.Interfaces;
using WebProje.Utility;

namespace WebProje.Models.Repositories
{
    public class FilmTuruRepository : Repository<FilmTuru>, IFilmTuruRepository
    {
        private UygulamaDbContext _uygDbContext;
        public FilmTuruRepository(UygulamaDbContext uygDbContext) : base(uygDbContext)
        {
            _uygDbContext = uygDbContext;
        }

        public void Guncelle(FilmTuru filmTuru)
        {
            _uygDbContext.Update(filmTuru);
        }

        public void Kaydet()
        {
            _uygDbContext.SaveChanges();
        }
    }
}
