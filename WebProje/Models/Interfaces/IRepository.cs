using System.Linq.Expressions;

namespace WebProje.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //hangi türde çalışıyorsa o türde bir veri döndürür. ÖR:Film
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null); //veritabanında şarta göre arama yapar
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralik(IEnumerable<T> entities);
    }
}
