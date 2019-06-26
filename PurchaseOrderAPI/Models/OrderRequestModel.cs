using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PurchaseOrderAPI.Models
{
    public class OrderRequestModel
    {
        [StringLength(4)]
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public int? Quantity { get; set; }


        [StringLength(4)]
        public string PurchaseOrderNO { get; set; }

        public DateTime? PurchaseOrderDate { get; set; }

        [StringLength(4)]
        public string SupplierNumber { get; set; }
    }
}