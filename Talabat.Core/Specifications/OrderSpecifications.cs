using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Core.Specifications
{
    public class OrderSpecifications:BaseSpecifications<Order>
    {
        public OrderSpecifications(string Buyeremail):base(O=>O.BuyerEmail ==Buyeremail)
        {
            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.OrderItems);
            AddOrderByDesc(O => O.OrderDate);   //from newest to oldest
        }

        public OrderSpecifications(string Buyeremail , int OrderId):base(O => O.BuyerEmail == Buyeremail && O.Id == OrderId)
        {

            Includes.Add(o => o.deliveryMethod);
            Includes.Add(o => o.OrderItems);
        }



    }
 }
