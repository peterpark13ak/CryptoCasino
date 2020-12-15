using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Entities.Base
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}
