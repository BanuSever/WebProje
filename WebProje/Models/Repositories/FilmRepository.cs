using System.Linq.Expressions;
using WebProje.Models.Interfaces;
using WebProje.Utility;

namespace WebProje.Models.Repositories
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(UygulamaDbContext uygDbContext) : base(uygDbContext)
        {
        }

        public void Guncelle(Film film)
        {
            dbSet.Update(film);
        }

        public void Kaydet()
        {
             _UygDbContext.SaveChanges();
        }
    }
}
