﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Library.Models
{
    public class Lookup
    {
        public int Id { get; set; }
        public string StatusEn { get; set; }
        public string StatusAr { get; set; }
        public string ParentId { get; set; }
        [Key]
        public string Code { get; set; }


    }
}
