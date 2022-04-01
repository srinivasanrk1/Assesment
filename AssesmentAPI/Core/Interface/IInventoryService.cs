using AssesmentAPI.Core.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssesmentAPI.Core.Interface
{
    public interface IInventoryService
    {
        Dictionary<string, int> ProductCountByStatus();
        Task<Tuple<bool, string>> UpdateProductStatus(Guid productId, StatusEnum statusEnum);
        Task<Tuple<bool,string>> SellProduct(Guid productId);
    }
}
