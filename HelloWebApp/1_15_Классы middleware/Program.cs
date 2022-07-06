using _1_15_Классы_middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Для добавления компонента middleware, который представляет класс, в конвейер обработки запроса применяется метод UseMiddleware(). 
app.UseMiddleware<TokenMiddleware>();

app.Run(async (context) => await context.Response.WriteAsync("Main branch"));
//С помощью метода UseMiddleware<T> в конструктор объекта TokenMiddleware будет внедряться объект для параметра RequestDelegate next.
//Поэтому явным образом передавать значение для этого параметра нам не нужно.

//Метод расширения для встраивания middleware/ т.е. метод обертка для примера выше
//app.UseToken();

app.Run();




//В примерах выше, используемые компоненты middleware фактически представляли методы, то есть так называемые inline middleware.
//Однако ASP.NET Core позволяет определять middleware в виде отдельных классов.

//Метод расширения для встраивания middleware 
// Также нередко для встраивания подобных компонентов middleware определяются специальные методы расширения.
// Так, добавим в проект новый класс, который назовем TokenExtensions.

//Передача параметров
// Изменим класс TokenMiddleware, чтобы он извне получал образец токена для сравнения:
//public class TokenMiddleware
//{
//    private readonly RequestDelegate next;
//    string pattern;
//    public TokenMiddleware(RequestDelegate next, string pattern)
//    {
//        this.next = next;
//        this.pattern = pattern;
//    }

//    public async Task InvokeAsync(HttpContext context)
//    {
//        var token = context.Request.Query["token"];
//        if (token != pattern)
//        {
//            context.Response.StatusCode = 403;
//            await context.Response.WriteAsync("Token is invalid");
//        }
//        else
//        {
//            await next.Invoke(context);
//        }
//    }
//}

// Образец токена, с которым идет сравнения, устанавливается через конструктор. Чтобы передать его в конструктор, изменим класс TokenExtensions:
//public static class TokenExtensions
//{
//    public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
//    {
//        return builder.UseMiddleware<TokenMiddleware>(pattern);
//    }
//}

//В метод builder.UseMiddleware можно передать набор значений, которые передаются в конструктор компонента middleware.
//И при вызове метода расширения UseToken в него можно передать конкретное значение:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();

//app.UseToken("555555");

//app.Run(async (context) => await context.Response.WriteAsync("Hello METANIT.COM"));

//app.Run();