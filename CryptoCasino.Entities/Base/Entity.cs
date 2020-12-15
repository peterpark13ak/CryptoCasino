using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebCasino.Entities.Base
{
    public class Entity : IDeletable, IModifiable
    {
        [Key]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
