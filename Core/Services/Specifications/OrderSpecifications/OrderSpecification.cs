

using Domain.Entities.Order;

namespace Services.Specifications.OrderSpecifications
{
    public class OrderSpecification: Specifications<Order>
    {
        public OrderSpecification(string email):base(O=>O.UserEmail==email )
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
            SetOrderByDescending(o => o.OrderDate);
            
        }

        //Get Order By Id
        public OrderSpecification(Guid id) : base(O => O.Id == id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.Items);
          

        }
    }
}
