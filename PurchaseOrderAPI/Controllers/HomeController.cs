using PurchaseOrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PurchaseOrderAPI.Controllers
{
    [RoutePrefix("purchase")]
    public class HomeController : ApiController
    {
        EntityModel model = new EntityModel();

        [Route("getitems")]
        public List<ItemModel>GetItems()
        {

            var entityItems= model.ITEMs;
            var items = new List<ItemModel>();
            foreach ( var rec in entityItems)
            {
                items.Add(new ItemModel { ItemCode = rec.ITCODE, ItemDescription = rec.ITDESC, ItemRate = rec.ITRATE });

            }

            return items;
        }

        [Route("getitem")]
        public ItemModel GetItemDetails(string itemCode)
        {
            var entityItem = model.ITEMs.Any(x=>x.ITCODE==itemCode);
            var item = new ItemModel();
            return item;
        }

        [Route("getorders")]
        public List<OrderRequestModel> GetOrders()
        {

            var entityOrders = model.PODETAILs;

            var orders = new List<OrderRequestModel>();
            foreach (var rec in entityOrders)
            {
                var entityPOMaster = model.POMASTERs.First(x => x.PONO == rec.PONO);
                var entityItem = model.ITEMs.First(x => x.ITCODE == rec.ITCODE);
                orders.Add(new OrderRequestModel { ItemCode = rec.ITCODE, Quantity = rec.QTY, ItemDescription=entityItem.ITDESC,PurchaseOrderDate= entityPOMaster.PODATE });
            }

            return orders;
        }

        [Route("order")]
        public Object OrderItem(OrderRequestModel order)
        {
            var pomaster = model.Set<POMASTER>();
            pomaster.Add(new POMASTER {
                PODATE =order.PurchaseOrderDate,
                PONO=order.PurchaseOrderNO,
                SUPLNO=order.SupplierNumber
            });

            var podetail = model.Set<PODETAIL>();
            podetail.Add(new PODETAIL
            {
                ITCODE=order.ItemCode,
                PONO=order.PurchaseOrderNO,
                QTY=order.Quantity
            });

            model.SaveChanges();
            return true;
        }

        public Object CancelOrder(string itemCode)
        {
            //Delete from Order
            return null;
        }


    }
}
