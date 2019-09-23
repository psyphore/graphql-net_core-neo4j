using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Product
{
    public interface IProductRepository
    {
        Task<Product> Get(string id);

        Task<IEnumerable<Product>> All();

        Task<Product> Add(Product product);

        Task<Product> Update(Product product);

        Task<string> Delete(string id);
    }
}