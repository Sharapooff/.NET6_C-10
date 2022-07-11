var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ITimeService, ShortTimeService>(); // добавляем сервис
//Конструкторы класса ___
builder.Services.AddTransient<TimeMessage>();
//Метод Invoke/InvokeAsync компонентов middleware
builder.Services.AddTransient<ITimeService, ShortTimeService>();

var app = builder.Build();

app.Run(async context =>
{
    //WebApplication (service locator)___
    // Получаем из коллекции сервисов объект сервиса ITimeService - в данном случае он будет представлять объект ShortTimeService
    var timeService = app.Services.GetService<ITimeService>(); // если сервис не добавлен, вернет null, без генерации исключения
    //var timeService = app.Services.GetRequiredService<ITimeService>(); // если сервис не добавлен, вызовет исключение
    //HttpContext.RequestServices ___
    //var timeService = context.RequestServices.GetService<ITimeService>();
    //Конструкторы класса ___
    //var timeMessage = context.RequestServices.GetService<TimeMessage>();
    //context.Response.ContentType = "text/html;charset=utf-8";
    //await context.Response.WriteAsync($"<h2>{timeMessage?.GetTime()}</h2>");
    //Метод Invoke/InvokeAsync компонентов middleware ___
    //app.UseMiddleware<TimeMessageMiddleware>();
    await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
});

app.Run();


interface ITimeService
{
    string GetTime();
}
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}



//В ASP.NET Core мы можем получить добавленные в приложения сервисы различными способами;
// - Через свойство Services объекта WebApplication (service locator)
// - Через свойство RequestServices контекста запроса HttpContext в компонентах middleware (service locator)
// - Через конструктор класса
// - Через параметр метода Invoke компонента middleware
// - Через свойство Services объекта WebApplicationBuilder

//Свойство Services объекта WebApplication (service locator)_________________________________________________________________________________________________
// Там, где нам доступен объект WebApplication, который представляет текущее приложение, (например, в файле Program.cs),
// для получения сервисов мы можем использовать его свойство Services. 
// GetService<service>(): использует провайдер сервисов для создания объекта, который представляет тип service.
//                        В случае если в провайдере сервисов для данного сервиса не установлена зависимость, то возвращает значение null
// GetRequiredService<service>(): использует провайдер сервисов для создания объекта, который представляет тип service.
//                        В случае если в провайдере сервисов для данного сервиса не установлена зависимость, то генерирует исключение
// Данный паттерн получения сервиса еще называется service locator, и, как правило, не рекомендуется к использованию, но тем не менее в рамках ASP.NET Core
// в принципе мы можем использовать подобную функциональность особенно там, где другие способы получения зависимостей не достуны.

//HttpContext.RequestServices _________________________________________________________________________________________________________________
// Там, где нам доступен объект HttpContext, мы можем использовать для получения сервисов его свойство RequestServices. Это свойство предоставляет объект IServiceProvider.
// То есть по сути мы имеем дело с выше описанным способом получения сервисов с помощью методов GetService() и GetRequiredService()

//Конструкторы класса _________________________________________________________________________________________________________________
// Встроенная в ASP.NET Core система внедрения зависимостей использует конструкторы классов для передачи всех зависимостей.
// Передача сервисов через конструкторы является предпочтительным способом внедрения зависимостей.
class TimeMessage
{
    ITimeService timeService;
    public TimeMessage(ITimeService timeService)
    {
        this.timeService = timeService;
    }
    public string GetTime() => $"Time: {timeService.GetTime()}";
}
// десь через конструктор класса передается зависимость от ITimeService. Причем здесь неизвестно, что это будет за реализация интерфейса ITimeService.
// В методе GetTime() формируем сообщение, в котором из сервиса получаем текущее время.


//Метод Invoke/InvokeAsync компонентов middleware ___________________________________________________
// Подобно тому, как зависимости передаются в конструктор классов, точно также их можно передавать в метод Invoke/InvokeAsync() компонента middleware.
// Например, определим следующий компонент:
class TimeMessageMiddleware
{
    private readonly RequestDelegate next;

    public TimeMessageMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITimeService timeService)
    {
        context.Response.ContentType = "text/html;charset=utf-8";
        await context.Response.WriteAsync($"<h1>Time: {timeService.GetTime()}</h1>");
    }
}