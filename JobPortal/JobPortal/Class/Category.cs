﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Class
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public Category(int CategoryID, string Name)
        {
            this.CategoryID = CategoryID;
            this.Name = Name;
        }

        public Category(string Name)
        {
            this.Name = Name;
        }
    }
}
