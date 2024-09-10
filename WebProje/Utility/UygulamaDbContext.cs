using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Utility
{
    public class UygulamaDbContext : IdentityDbContext //DbContext sınıfından türetildi
    {
        //Constructor tanımlandı
        //options parametresi veritabanı bağlantı bilgileri vb. seçenekleri içerecek
        //base(option) ile parametreyi DbContext sınıfının yapıcı metoduna gönderecek
        //EF Core'un veritabanına nasıl bağlanacağını bilmesini sağlar
        
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }
        public DbSet<FilmTuru> FilmTurleri { get; set; }
        public DbSet<Film> Filmler {  get; set; }
    }
}
