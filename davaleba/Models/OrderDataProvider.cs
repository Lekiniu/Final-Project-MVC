using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace davaleba.Models
{
    public class OrderDataProvider
    {
        ProjectDbEntities _db = new ProjectDbEntities();

     
        public Order GetOrderById(int id)
        {
            return _db.Orders.FirstOrDefault(e => e.Id == id);
        }

        //Create
        public void CreateOrder(OrderCustomClass order)
        {
                _db.Orders.Add(new Order()
                {
                    ProductId = order.ProductId,
                    UserId = order.UserId,
                    soldItem=order.soldItem
                });

            _db.SaveChanges();
        }

        //Edit 
        public void EditOrder(OrderCustomClass order)
        {
            var result = _db.Orders.FirstOrDefault(e => e.Id == order.Id);

            result.ProductId = order.ProductId;
            result.UserId = order.UserId;
            result.soldItem = order.soldItem;
            _db.SaveChanges();
        }
        //Delete
        //public void deleteUser(Users user)
        //{
        //    var result = _db.Users.FirstOrDefault(e => e.Id == user.Id);
        //    result.IsActive = false;
        //    _db.SaveChanges();
        //}


        public void FullDeleteOrder(Order order)
        {
            
            var deleteOrders = _db.Orders.Where(e => e.Id == order.Id);

            _db.Orders.RemoveRange(deleteOrders);
            _db.SaveChanges();
        }

        //search 
        //public IEnumerable<Orders> GetOrder(int id)
        //{

        //    return _db.Orders.Where(e => e.UserId==id);
        //}


        public IEnumerable<Order> AllOrders()
        {
            return _db.Orders;
        }
    }
}