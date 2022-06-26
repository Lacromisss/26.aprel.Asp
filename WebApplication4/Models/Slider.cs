using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degre { get; set; }
        public bool isDelete { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile  Photo { get; set; }
    }
}
