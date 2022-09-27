namespace SistemaGenericoRH.Helpers
{
    public class GenericException : Exception
    {
        public GenericException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage
        {
            get; set;
        }


    }
}