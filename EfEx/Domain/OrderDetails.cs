﻿namespace EfEx.Domain
{
    public class OrderDetails
    {
        //[Table("orderdetails")]
        public int OrderId {get; set; }
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
    }
}