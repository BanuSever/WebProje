using System.ComponentModel.DataAnnotations;

namespace WebProje.Models
{
    public class FilmTuru
    {
        [Key] // primary key tanımlama
        public int TurID { get; set; }
        
        [MaxLength(20)] //Max 25 karakter girebilsin
        [Required(ErrorMessage ="Lütfen bir tür adı giriniz.")] // not null tanımlama
        public string TurAdi { get; set; }
    }
}
