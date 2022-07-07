namespace _1_16_Построение_конвейера_обработки_запроса
{
    //Этот компонент в зависимости от строки запроса возвращает либо определенную строку, либо устанавливает код ошибки.
    public class RoutingMiddleware
    {
        readonly RequestDelegate next;
        public RoutingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            if (path == "/index")
            {
                await context.Response.WriteAsync("Home Page");
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("About Page");
            }
            else
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
