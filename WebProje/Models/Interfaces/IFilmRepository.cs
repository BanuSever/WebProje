namespace WebProje.Models.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        void Guncelle(Film film);
        void Kaydet();
    }
}
