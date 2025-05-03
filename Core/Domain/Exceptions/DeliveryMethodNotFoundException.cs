

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int id):NotFoundException($"No Delivery Method Found With Id = {id} .")
    {
    }
}
