using EkompletGRPCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EkompletGRPCExample.Data
{
    public class DbInitializer
    {

        public static void Initialize(DatabaseContext context)
        {
            if (context.Database.EnsureCreated())
            {

                var salesmen = new List<Salesman>
                {
                    new() {FirstName ="Ronnie", LastName ="Svendsen", Email = "Klaus@32.com", PhoneNumber = "23232323"}
                };
            }

            
        }
    }
}
