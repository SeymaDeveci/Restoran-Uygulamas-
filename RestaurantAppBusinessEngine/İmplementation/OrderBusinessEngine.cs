using RestaurantAppBusinessEngine.Contracts;
using RestaurantAppCommon.Dtos;
using RestaurantAppCommon.ResultConstant;
using RestaurantAppData.DataContracts;
using RestaurantAppData.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantAppBusinessEngine.İmplementation
{
    public class OrderBusinessEngine : IOrderBusinessEngine
    {
        private readonly IUnitOfWork _uow;
        public OrderBusinessEngine(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Result<bool> DeleteOrder(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new Result<bool>(false, $" id => {id} değeri 0'dan büyük olmalıdır.");
                }
                var data = _uow.order.GetFirstOrDefault(o => o.OrderId == id, "OrderItems"); //OrdetItems tblo ismi
                foreach (var item in data.OrderItems.ToList())
                {
                    _uow.orderitem.Remove(item);
                }
                _uow.order.Remove(data);
                _uow.Save();
                return new Result<bool>(true, ResultConstant.RecordRemovedSuccess);
            }
            catch (Exception ex)
            {

                return new Result<bool>(false,ex.Message.ToString());
            }
        }

        public Result<List<GetOrderDto>> GetOrders()
        {
            var data = _uow.order.GetAll(null, null, "Customer"); //Customer tablosuyla birlikte dataları getir
            if (data != null)
            {
                List<GetOrderDto> returnModel = new List<GetOrderDto>();
                foreach (var item in data)
                {
                    returnModel.Add(new GetOrderDto()
                    {

                        GrandTotal = item.GrandTotal,
                        OrderId = item.OrderId,
                        OrderNo = item.OrderNo,
                        PaymentMethod = item.PaymentMethod,
                        CustomerName = item.Customer.Name

                    });
                }
                return new Result<List<GetOrderDto>>(true, ResultConstant.RecordFound, returnModel);
            }
            return new Result<List<GetOrderDto>>(false, ResultConstant.RecordNotFound);
        }

        public Result<bool> SaveOrder(OrderDto order)
        {
            try
            {
                Order orderModel = new Order();
                orderModel.CustomerId = Convert.ToInt32(order.OrderSubDto.CustomerId);
                orderModel.GrandTotal = Convert.ToInt32(order.OrderSubDto.GrandTotal);
                orderModel.OrderNo = order.OrderSubDto.OrderNo;
                orderModel.PaymentMethod = order.OrderSubDto.PaymentMethod;

                _uow.order.Add(orderModel); //order tablosuna kayıt ekleme
                _uow.Save();

                foreach (var item in order.OrderItemModelDtos)
                {
                    OrderItem oItem = new OrderItem();
                    oItem.OrderId = orderModel.OrderId;
                    oItem.Quantity = item.Quantity;
                    oItem.ItemId = Convert.ToInt32(item.ItemId);
                    _uow.orderitem.Add(oItem);
                }

                _uow.Save();
                return new Result<bool>(true, ResultConstant.RecordCreated);
            }
            catch (System.Exception ex)
            {

                return new Result<bool>(false, ex.Message.ToString());
            }
        }
    }
}
