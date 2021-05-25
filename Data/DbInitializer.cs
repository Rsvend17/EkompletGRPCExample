﻿using EkompletGRPCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkompletGRPCExample.Data
{
    public class DbInitializer
    {

        public static bool Initialize(DatabaseContext context)
        {
            if (context.Database.EnsureCreated())
            {

                var salesmen = new List<Salesman>
                {
                    new() {FirstName ="Ronnie", LastName ="Svendsen", Email = "Klaus@32.com", PhoneNumber = 23232323},
                    new() {FirstName ="Brian", LastName ="Svendsen", Email = "Kirsten@32.com", PhoneNumber = 23232323},
                    new() {FirstName ="Karl", LastName ="Svendsen", Email = "Bo@32.com", PhoneNumber = 23123122},
                    new() {FirstName ="Emil", LastName ="Svendsen", Email = "Trine@32.com", PhoneNumber = 23232323}

                };

                context.Salesmen.AddRange(salesmen);
            }

            if(context.SaveChanges() == 0)
            {
                return false;
            }
            else { return true; }

            
        }
    }
}
