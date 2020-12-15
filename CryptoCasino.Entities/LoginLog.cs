using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebCasino.Entities.Base;

namespace WebCasino.Entities
{
    public class LoginLog : Entity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        [Range(7,15)]
        public string Ip { get; set; }

        [Range(2,74)]
        public string Country { get; set; }
    }
}
