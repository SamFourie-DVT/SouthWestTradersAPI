﻿using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Stock
    {
        public int StockId { get; set; }
        public int? ProductId { get; set; }
        public int? AvailableStock { get; set; }

        public virtual Product? Product { get; set; }
    }
}
