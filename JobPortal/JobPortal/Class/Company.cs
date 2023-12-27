﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Class
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }

        public Company(int companyID, string name, string adress, string description)
        {
            CompanyID = companyID;
            Name = name;
            Adress = adress;
            Description = description;
        }

        public Company(string name, string adress, string description)
        {
            Name = name;
            Adress = adress;
            Description = description;
        }

        public Company(string name)
        {
            Name = name;
        }
    }
}
