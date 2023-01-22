namespace BookReader.Api.BookReaderHttpClient.Exceptions
{
    public class BookReaderHttpClientException : Exception
    {
        public BookReaderHttpClientException(string message) : base(message) { }

        public BookReaderHttpClientException(string message, Exception innerException) : base(message, innerException) { }
    }
}
