namespace ALBab.Errors
{
    public class ApiException // Using inside ExceptionMiddleware for 500
    {
         public ApiException(int statusCode =500, string message = null, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

     public int StatusCode { get; set; }
     public string Message { get; set; }   

     public string Details { get; set; } 
        
    }
}