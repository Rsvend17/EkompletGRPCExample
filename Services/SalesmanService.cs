using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKomplet.Generated.gRPC.RequestUtility;
using EKomplet.Generated.gRPC.Salesman;
using EkompletGRPCExample.Data;
using EkompletGRPCExample.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace EkompletGRPCExample.Services
{
    public class SalesmanService : SalesmanHandler.SalesmanHandlerBase
    {
        private readonly DatabaseContext _databasecontext;
        private readonly ILogger<SalesmanService> _logger;

        public SalesmanService(DatabaseContext databasecontext, ILogger<SalesmanService> logger)
        {
            _databasecontext = databasecontext;
            _logger = logger;
        }

        public override Task<RequestReplyWithSalesman> RequestSalesmanById(salesmanRequestById request, ServerCallContext context)
        {


                _logger.LogDebug("[RequestListingById] request.ID is empty");
                return Task.FromResult(new RequestReplyWithSalesman
                {
                    Result  = new RequestResult
                    {
                        Succeeded = false,
                        Errors = {"Null or empty"}
                    }
                });
        }

        public override Task<RequestReplySalesmen> RequestSalesmen(Empty request, ServerCallContext context)
        {
            return base.RequestSalesmen(request, context);
        }
    }
}
