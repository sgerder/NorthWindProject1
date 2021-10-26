using System;
using System.Collections.Generic;

namespace EfEx.Domain
{
    public class Order
    {
        //[Table("Orders")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ShipName { get; set; }
        public string ShipCity { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}