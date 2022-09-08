using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Utilities.Result.Abstract;

namespace Business.Repositories.CustomerRelotionshipRepository
{
    public interface ICustomerRelotionshipService
    {
        Task<IResult> Add(CustomerRelationships customerRelotionship);
        Task<IResult> Update(CustomerRelationships customerRelotionship);
        Task<IResult> Delete(CustomerRelationships customerRelotionship);
        Task<IDataResult<List<CustomerRelationships>>> GetList();
        Task<IDataResult<CustomerRelationships>> GetById(int id);
    }
}
