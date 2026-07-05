namespace InternetShop.Exceptions
{
    public class NotEnoughStockException: Exception
    {
        public NotEnoughStockException(string message)
            : base(message)
        {

        }
    }
}
