using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.CustomerRelotionshipRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.CustomerRelotionshipRepository.Validation;
using Business.Repositories.CustomerRelotionshipRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.CustomerRelotionshipRepository;

namespace Business.Repositories.CustomerRelotionshipRepository
{
    public class CustomerRelotionshipManager : ICustomerRelotionshipService
    {
        private readonly ICustomerRelotionshipDal _customerRelotionshipDal;

        public CustomerRelotionshipManager(ICustomerRelotionshipDal customerRelotionshipDal)
        {
            _customerRelotionshipDal = customerRelotionshipDal;
        }

      //  [SecuredAspect()]
        [ValidationAspect(typeof(CustomerRelotionshipValidator))]
        [RemoveCacheAspect("ICustomerRelotionshipService.Get")]

        public async Task<IResult> Add(CustomerRelationships customerRelotionship)
        {
            await _customerRelotionshipDal.Add(customerRelotionship);
            return new SuccessResult(CustomerRelotionshipMessages.Added);
        }

        [SecuredAspect()]
        [ValidationAspect(typeof(CustomerRelotionshipValidator))]
        [RemoveCacheAspect("ICustomerRelotionshipService.Get")]

        public async Task<IResult> Update(CustomerRelationships customerRelotionship)
        {
            await _customerRelotionshipDal.Update(customerRelotionship);
            return new SuccessResult(CustomerRelotionshipMessages.Updated);
        }

        [SecuredAspect()]
        [RemoveCacheAspect("ICustomerRelotionshipService.Get")]

        public async Task<IResult> Delete(CustomerRelationships customerRelotionship)
        {
            await _customerRelotionshipDal.Delete(customerRelotionship);
            return new SuccessResult(CustomerRelotionshipMessages.Deleted);
        }

        [SecuredAspect()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<CustomerRelationships>>> GetList()
        {
            return new SuccessDataResult<List<CustomerRelationships>>(await _customerRelotionshipDal.GetAll());
        }

        [SecuredAspect()]
        public async Task<IDataResult<CustomerRelationships>> GetById(int id)
        {
            return new SuccessDataResult<CustomerRelationships>(await _customerRelotionshipDal.Get(p => p.Id == id));
        }

    }
}
