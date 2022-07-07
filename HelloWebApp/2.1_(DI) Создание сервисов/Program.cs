var builder = WebApplication.CreateBuilder(args);
//Благодаря вызову AddTransient<ITimeService, ShortTimeService>() система на место объектов интерфейса ITimeService будет передавать экземпляры класса ShortTimeService.
// добавление сервисов
builder.Services.AddTransient<ITimeService, ShortTimeService>();
builder.Services.AddTransient<TimeService_One>(); // как конкретный класс
builder.Services.AddTimeService(); // методы добавления в виде методов расширения для интерфейса IServiceCollection
// или builder.Services.AddTransient<ITimeService, LongTimeService>();
// сервисы добавляются до создания объекта WebApplication методом Build()
// создание объекта WebApplication
var app = builder.Build();

app.Run(async context =>
{
    // После добавления сервиса его можно получить и использовать в любой части приложения. Для получения сервиса могут применяться различные способы
    // в зависимости от ситуации.В данном случае используется свойство app.Services., которое предоставляет провайдер сервисов -объект IServiceProvider.
    // Для получения сервиса у провайдера сервиса вызывается метод GetService(), который типизируется типом сервиса:
    var timeService = app.Services.GetService<ITimeService>();
    var timeService_One = app.Services.GetService<TimeService_One>();
    // После получения сервиса мы можем использовать его.
    await context.Response.WriteAsync($"Time: {timeService?.GetTime()}  -  {timeService?.GetTime()}");
});

app.Run();


//Определим новый интерфейс ITimeService, который предназначен для получения времени:
interface ITimeService
{
    string GetTime();
}

// время в формате hh::mm
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}
// время в формате hh:mm:ss
class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}


//Сервис как конкретный класс
// При этом необязательно разделять определение сервиса в виде интерфейса и его реализацию. Сам термин "сервис" в данном случае может представлять любой объект, 
// функциональность которого может использоваться в приложении.
// Например, определим новый класс TimeService:
public class TimeService_One
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}
// Данный класс определяет один метод GetTime(), который возвращает текущее время.

//Расширения для добавления сервисов
// Нередко для сервисов создают собственные методы добавления в виде методов расширения для интерфейса IServiceCollection.
// Например, создадим подобный метод для сервиса TimeService:
public static class ServiceProviderExtensions
{
    public static void AddTimeService(this IServiceCollection services)
    {
        services.AddTransient<TimeService_One>();
    }
}