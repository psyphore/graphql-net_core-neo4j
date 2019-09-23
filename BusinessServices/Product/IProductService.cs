using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAll();
        Task<ProductModel> Get(string id);
    }
}