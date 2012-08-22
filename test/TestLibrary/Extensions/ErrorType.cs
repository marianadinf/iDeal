namespace UIT.iDeal.TestLibrary.Extensions
{
    public class ErrorType
    {
        readonly string _errorMessage;

        private ErrorType(string errorMessage)
        {
            _errorMessage = errorMessage;
        }

        public override string ToString()
        {
            return _errorMessage;
        }

        public static readonly ErrorType NotEmpty = new ErrorType("'{0}' should not be empty.");
    }
}