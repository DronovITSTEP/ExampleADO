﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleADO.Models
{
    public class City
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public int CountryId { get; set; }
        public bool IsCapital { get; set; }

    }
}
