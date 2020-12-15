using System;
using System.Collections.Generic;
using System.Text;

namespace WebCasino.Entities.Base
{
    public interface IModifiable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
