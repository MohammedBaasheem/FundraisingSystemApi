namespace Fundraising_System.Application.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string message): base(message)
        { }  
    }
}
