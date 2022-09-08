using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.CustomerRelotionshipRepository;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.CustomerRelotionshipRepository
{
    public class EfCustomerRelotionshipDal : EfEntityRepositoryBase<CustomerRelationships, SimpleContextDb>, ICustomerRelotionshipDal
    {
    }
}
