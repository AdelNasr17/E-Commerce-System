
namespace Domain.Exceptions
{
    public sealed class AddressNotFoundException(string userName):NotFoundException($"User {userName} Has No Address .")
    {
    }
}
