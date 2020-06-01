﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string Id { get; set; }                          // property
        public string Category { get; set; }                    // property

        public ProductCategory()                                // consructor
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}