using System.ComponentModel.DataAnnotations;

namespace WebCasino.Entities
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(3)]
        public string Name { get; set; }
    }
}