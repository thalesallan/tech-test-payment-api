namespace tech_test_payment.Helpers.ExceptionHelper
{
    public class CustonException : Exception
    {
        public CustonException()
        {
        }

        public CustonException(string message) : base(message)
        {
        }

        public CustonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
