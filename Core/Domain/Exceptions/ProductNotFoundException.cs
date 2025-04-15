

namespace Domain.Exceptions
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(int id):base($"Product With id : {id} not Found")
        {
            
        }
    }
}
