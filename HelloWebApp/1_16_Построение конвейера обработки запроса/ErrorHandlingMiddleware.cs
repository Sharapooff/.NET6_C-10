namespace _1_16_Построение_конвейера_обработки_запроса
{
    public class ErrorHandlingMiddleware
    {
        readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        //В отличие от предыдущих двух компонентов ErrorHandlingMiddleware сначала передает запрос на выполнение последующим делегатам, а потом уже сам обрабатывает.
        //Это возможно, поскольку каждый компонент обрабатывает запрос два раза: вначале вызывается та часть кода, которая идет до await next.Invoke(context);,
        //а после завершения обработки последующих компонентов вызывается та часть кода, которая идет после await next.Invoke(context);.
        public async Task InvokeAsync(HttpContext context)
        {
            await next.Invoke(context);
            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied");
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
