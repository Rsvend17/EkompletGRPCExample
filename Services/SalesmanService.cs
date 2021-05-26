using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKomplet.Generated.gRPC.RequestUtility;
using EKomplet.Generated.gRPC.Salesman;
using EkompletGRPCExample.Data;
using EkompletGRPCExample.Models;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EkompletGRPCExample.Services
{
    public class SalesmanService : SalesmanHandler.SalesmanHandlerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<SalesmanService> _logger;

        public SalesmanService(DatabaseContext databaseContext, ILogger<SalesmanService> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        public override Task<RequestReplyWithSalesman> RequestSalesmanById(salesmanRequestById request, ServerCallContext context)
        {

            if (string.IsNullOrEmpty(request.Id))
            {

                _logger.LogDebug("[RequestListingById] request.ID is empty");
                return Task.FromResult(new RequestReplyWithSalesman
                {
                    Result = new RequestResult
                    {
                        Succeeded = false,
                        Errors = {"Request is Null or empty"}
                    }
                });
            }

            Salesman salesmanResult;
            try
            {
                salesmanResult = _databaseContext.Salesmen.FirstOrDefault(salesman => salesman.SalesmanID.ToString() == request.Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("[RequestSalesmanById] Could not find listing with ID: {ID}", request.Id);
                return Task.FromResult(new RequestReplyWithSalesman
                {
                    Result = new RequestResult
                    {
                        Succeeded = false,
                        Errors = {"Could not find Salesman"}
                    }
                });
            }

            if (salesmanResult?.PhoneNumber != null)
            {
                var salesmanMsg = new SalesmanMsg
                {
                    Email = salesmanResult?.Email,
                    FirstName = salesmanResult?.FirstName,
                    LastName = salesmanResult?.LastName,
                    PhoneNumer = (uint) salesmanResult?.PhoneNumber,
                    Id = (uint) salesmanResult?.SalesmanID,
                };

                return Task.FromResult(new RequestReplyWithSalesman
                {
                    Result = new RequestResult
                    {
                        Succeeded = true,
                        Errors = { }
                    },
                    Salesman = salesmanMsg
                });
            }
            _logger.LogError("[RequestSalesmanById] Could not find listing with ID: {ID}", request.Id);
            return Task.FromResult(new RequestReplyWithSalesman
            {
                Result = new RequestResult
                {
                    Succeeded = false,
                    Errors = {"Could not find Salesman"}
                }
            });
        }

        public override Task<RequestReplySalesmen> RequestSalesmen(Empty request, ServerCallContext context)
        {

            List<Salesman> salesmanResult;
            try
            {
                salesmanResult = _databaseContext.Salesmen.ToList();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError("[RequestSalesmen] Could not find any listings");
                return Task.FromResult(new RequestReplySalesmen
                {
                    Result = new RequestResult
                    {
                        Succeeded = false,
                        Errors = {"Could not find Salesman"}
                    }
                });
            }
            
            var repeatedSalesmanMsgs = new RepeatedField<SalesmanMsg>();
            
            // TODO: Create converter for this. 
            repeatedSalesmanMsgs.AddRange(salesmanResult.Select(l => l.));
            return Task.FromResult(new RequestReplySalesmen
            {
                Result = new RequestResult
                {
                    Succeeded = true,
                    Errors ={}
                },
                Salesmen = {repeatedSalesmanMsgs}
            });

        }
    }
}
