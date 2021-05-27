using EKomplet.Generated.gRPC.Salesman;
using EkompletGRPCExample.Models;

namespace EkompletGRPCExample.Conversion
{
    public static partial class Conversion
    {
        /// <summary>
        /// Extension method to convert from a model-type salesman to a message type for gRPC. 
        /// </summary>
        /// <param name="salesman"> The object from the database</param>
        /// <returns>A fully populated salesmanMsg</returns>
        public static SalesmanMsg ToSalesmanMsg(this Salesman salesman)
        {
            return new()
            {
                Id = (uint) salesman.SalesmanID,
                Email = salesman.Email ?? "",
                FirstName = salesman.FirstName ?? "",
                LastName = salesman.LastName ?? "",
                PhoneNumer = (uint) salesman.PhoneNumber
            };
        }
    }
}