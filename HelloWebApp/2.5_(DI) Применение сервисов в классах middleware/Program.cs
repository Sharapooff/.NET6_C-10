var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TimeService>();

var app = builder.Build();

app.UseMiddleware<TimerMiddleware>();
app.Run(async (context) => await context.Response.WriteAsync("Main branch"));

app.Run();




//ѕосле добавлени€ сервисов в коллекцию Services объекта WebApplicationBuilder они станов€тс€ доступны приложению, в том числе и в кастомных компонентах middleware.
// ¬ middleware мы можем получить зависимости трем€ способами:
// „ерез конструктор
// „ерез параметр метода Invoke/InvokeAsync
// „ерез свойство HttpContext.RequestServices
// ѕри этом надо учитывать, что компоненты middleware создаютс€ при запуске приложени€ и живут в течение всего жизненного цикла приложени€.
// “о есть при последующих запросах инфраструктура ASP.NET Core использует ранее созданный компонент. » это налагает ограничени€ на использование зависимостей в middleware.

//¬ частности, если конструктор передаетс€ transient-сервис, который создаетс€ при каждом обращении к нему, то при последующих запросах мы будем использовать тот же самый сервис,
//так как конструктор middleware вызываетс€ один раз - при создании приложени€.

public class TimeService
{
    public TimeService()
    {
        Time = DateTime.Now.ToLongTimeString();
    }
    public string Time { get; }
}
public class TimerMiddleware
{
    RequestDelegate next;
    TimeService timeService;

    public TimerMiddleware(RequestDelegate next, TimeService timeService)
    {
        this.next = next;
        this.timeService = timeService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/time")
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync($"“екущее врем€: {timeService?.Time}");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}

//Cколько бы мы раз не обращались по этому пути, мы все врем€ будем получать одно и то же врем€, так как объект TimerMiddleware был создан еще при первом запросе.
//ѕоэтому передача через конструктор middleware больше подходит дл€ сервисов с жизненным циклом Singleton, которые создаютс€ один раз дл€ всех последующих запросов.
//≈сли же в middleware необходимо использовать сервисы с жизненным циклом Scoped или Transient, то лучше их передавать через параметр метода Invoke/InvokeAsync:
//public class TimerMiddleware
//{
//    RequestDelegate next;

//    public TimerMiddleware(RequestDelegate next)
//    {
//        this.next = next;
//    }

//    public async Task InvokeAsync(HttpContext context, TimeService timeService)
//    {
//        if (context.Request.Path == "/time")
//        {
//            context.Response.ContentType = "text/html; charset=utf-8";
//            await context.Response.WriteAsync($"“екущее врем€: {timeService?.Time}");
//        }
//        else
//        {
//            await next.Invoke(context);
//        }
//    }
//}