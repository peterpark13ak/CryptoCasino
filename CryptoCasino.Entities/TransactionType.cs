using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebCasino.Entities
{
    public class TransactionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(3,20)]
        public string Name { get; set; }
    }
}
