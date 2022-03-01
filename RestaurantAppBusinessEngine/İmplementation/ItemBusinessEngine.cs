using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using RestaurantAppData.DataContracts;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAppBusinessEngine.İmplementation
{
    public class ItemBusinessEngine : IItemBusinessEngine
    {
        private readonly IUnitOfWork _uow;
        public ItemBusinessEngine(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public Result<List<ItemsDto>> GetItems()
        {
            List<ItemsDto> items = new List<ItemsDto>();
            var data = _uow.items.GetAll().ToList(); //Dataları getir
            if (data != null)
            {
                foreach (var item in data)
                {
                    items.Add(new ItemsDto()
                    {
                        ItemId = item.ItemId,
                        Name=item.Name,
                        Price=item.Price
                    });
                }
                return new Result<List<ItemsDto>>(true, ResultConstant.RecordFound,items); 
            }
            return new Result<List<ItemsDto>>(false, ResultConstant.RecordNotFound);
        }
    }
}
