using NinjectPractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjectPractice.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();
    }
}
