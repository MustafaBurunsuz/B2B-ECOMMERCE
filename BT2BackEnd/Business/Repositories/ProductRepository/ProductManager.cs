using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Repositories.ProductRepository;
using Entities.Concrete;
using Business.Aspects.Secured;
using Core.Aspects.Validation;
using Core.Aspects.Caching;
using Core.Aspects.Performance;
using Business.Repositories.ProductRepository.Validation;
using Business.Repositories.ProductRepository.Constants;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.ProductRepository;
using Entities.Dtos;
using Business.Repositories.ProductImageRepository;
using Business.Repositories.PriceListDetailRepository;
using Core.Utilities.Business;
using Business.Repositories.BasketRepository;
using Business.Repositories.OrderDetailRepository;

namespace Business.Repositories.ProductRepository
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductImageService _productImageService;
        private readonly IPriceListDetailService _priceListDetailService;
        private readonly IBasketService _basketService;
        private readonly IOrderDetailService _orderDetailService;


        public ProductManager(IProductDal productDal, IProductImageService productImageService, IPriceListDetailService priceListDetailService, IBasketService basketService, IOrderDetailService orderDetailService)
        {
            _productDal = productDal;
            _productImageService = productImageService;
            _priceListDetailService = priceListDetailService;
            _basketService = basketService;
            _orderDetailService = orderDetailService;
        }

        //  [SecuredAspect("Admin,product.add")]
        [ValidationAspect(typeof(ProductValidator))]
        [RemoveCacheAspect("IProductService.Get")]

        public async Task<IResult> Add(Product product)
        {
            await _productDal.Add(product);
            return new SuccessResult(ProductMessages.Added);
        }

        [SecuredAspect("Admin,product.update")]
        [ValidationAspect(typeof(ProductValidator))]
        [RemoveCacheAspect("IProductService.Get")]

        public async Task<IResult> Update(Product product)
        {
            await _productDal.Update(product);
            return new SuccessResult(ProductMessages.Updated);
        }

       //[SecuredAspect("Admin,product.delete")]
        [RemoveCacheAspect("IProductService.Get")]

        public async Task<IResult> Delete(Product product)
        {
            IResult result = BusinessRules.Run
                (
                    await CheckIfProductExistToBaskets(product.Id),
                    await CheckIfProductExistToOrderDetails(product.Id)
                );
            if (result != null)
            {
                return result;
            }
            var images = await _productImageService.GetListByProductId(product.Id);
            foreach (var item in images)
            {
                await _productImageService.Delete(item);

            }
            var PriceListProducts = await _priceListDetailService.GetListByProductId(product.Id);
            foreach (var item in PriceListProducts)
            {
                await _priceListDetailService.Delete(item);

            }
            await _productDal.Delete(product);
            return new SuccessResult(ProductMessages.Deleted);
        }

        //[SecuredAspect("Admin,product.get")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ProductListDto>>> GetList()
        {
            return new SuccessDataResult<List<ProductListDto>>(await _productDal.GetList());
        }

        //[SecuredAspect("Admin,product.get")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<ProductListDto>>> GetProductList(int custommerId)
        {
            return new SuccessDataResult<List<ProductListDto>>(await _productDal.GetProductList(custommerId));
        }

        [SecuredAspect("Admin,product.get")]
        public async Task<IDataResult<Product>> GetById(int id)
        {
            return new SuccessDataResult<Product>(await _productDal.Get(p => p.Id == id));
        }
        public async Task<IResult> CheckIfProductExistToBaskets(int productId)
        {
            var result = await _basketService.GetListByProductId(productId);
            if (result.Count()>0 )
            {
                return new ErrorResult("Sepette bulunan ürünler silinemez!");

            }
            return new SuccessResult();
        }
        public async Task<IResult> CheckIfProductExistToOrderDetails(int productId)
        {
            var result = await _orderDetailService.GetListByProductId(productId);
            if (result.Count() > 0)
            {
                return new ErrorResult("Sipariþi bulunan ürünler silinemez!");

            }
            return new SuccessResult();
        }

    }
}
