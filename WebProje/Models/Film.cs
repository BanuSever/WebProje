using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProje.Models
{
    public class Film
    {
        [Key]
        public int FilmID { get; set; }

        [Required]
        public string FilmAdi { get; set; }

        [ValidateNever]
        public int FilmTuruID { get; set; }
        [ForeignKey("FilmTuruID")]

        [ValidateNever]
        public FilmTuru FilmTuru { get; set; }

        public string Aciklama { get; set; }

        public string Yonetmen { get; set; }

        [ValidateNever]
        public string? PosterUrl {  get; set; }
    }
}
