using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebProje.Models.Interfaces;
using WebProje.Utility;

namespace WebProje.Models.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //UygulamaDbContext tipinde bir alan oluşturuldu
        internal readonly UygulamaDbContext _UygDbContext;
        internal DbSet<T> dbSet;

        //Constructor UygulamaDbContext tipinde bir parametre alır
        public Repository(UygulamaDbContext uygDbContext)
        {
            _UygDbContext = uygDbContext;
            dbSet = _UygDbContext.Set<T>();
            //Herhangi bir foreign key bağlantısı varsa getir
            _UygDbContext.Filmler.Include(f => f.FilmTuru);
        }

        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(prop);
                }
            }
            sorgu = sorgu.Where(filtre); // birden fazla getirebilir 
            return sorgu.FirstOrDefault(); // ilk gelen ya da varsayılanı döndür
        }

        //Tüm kayıtları getirir
        //Foreing key kısmı parametre
        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> sorgu = dbSet;
            //Foreign key alanları bulup sorguya ekle
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var prop in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(prop);
                }
            }
            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
