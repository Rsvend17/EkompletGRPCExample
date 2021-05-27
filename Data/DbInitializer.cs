using EkompletGRPCExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EkompletGRPCExample.Data
{
    public class DbInitializer
    {
        public static bool Initialize(DatabaseContext context, ILogger<Startup> logger)
        {
            logger.LogInformation("Starting to fill database");

            logger.LogInformation("Creating Salesmen to database..");
            var salesmen = new List<Salesman>
            {
                new() {FirstName = "Ronnie", LastName = "Svendsen", Email = "Ronnie@32.com", PhoneNumber = 23232323},
                new() {FirstName = "Brian", LastName = "Levorsen", Email = "Brian@32.com", PhoneNumber = 41233465},
                new() {FirstName = "Karl", LastName = "Ingorsdattor", Email = "Karl@32.com", PhoneNumber = 43242321},
                new() {FirstName = "Lasse", LastName = "Hunnarsen", Email = "Lasse@32.com", PhoneNumber = 65468923},
                new() {FirstName = "Birgit", LastName = "Damsen", Email = "Birgit@32.com", PhoneNumber = 41235645},
                new() {FirstName = "Søren", LastName = "Ingeborg", Email = "Søren@323.com", PhoneNumber = 85345345},
                new()
                {
                    FirstName = "Karsten", LastName = "Lassensen", Email = "Karsten@543.com", PhoneNumber = 23578224
                },
                new() {FirstName = "Morten", LastName = "Kaarsbo", Email = "Morten@32.com", PhoneNumber = 36345323},
                new() {FirstName = "Emil", LastName = "Svendsen", Email = "Emil@32.com", PhoneNumber = 34029564}
            };

            context.Salesmen.AddRange(salesmen);


            if (context.SaveChanges() == 0)
            {
                logger.LogCritical("Something went wrong when adding salesmen");
                return false;
            }
            else
            {
                logger.LogInformation("Filled database with salesmen");
                return true;
            }
        }
    }
}