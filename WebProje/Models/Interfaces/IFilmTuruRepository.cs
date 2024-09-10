namespace WebProje.Models.Interfaces
{
    public interface IFilmTuruRepository : IRepository<FilmTuru>
    {
        void Guncelle(FilmTuru filmTuru);
        void Kaydet();
    }
}
