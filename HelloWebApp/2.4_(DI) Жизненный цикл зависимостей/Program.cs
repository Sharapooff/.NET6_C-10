var builder = WebApplication.CreateBuilder(args);
//*Transient
// CounterMiddleware получает объект ICounter, для которого создается один экземпляр класса RandomCounter. CounterMiddleware также получает объект CounterService,
// который также использует ICounter. И для этого ICounter будет создаваться второй экземпляр класса RandomCounter.
// Поэтому генерируемые случайные числа обоими экземплярами не совпадают. Таким образом, применение AddTransient создаст два разных объекта RandomCounter.
builder.Services.AddTransient<ICounter, RandomCounter>();
builder.Services.AddTransient<CounterService>();
//*Scoped
// Метод AddScoped создает один экземпляр объекта для всего запроса.
// Теперь в рамках одного и того же запроса и CounterMiddleware и сервис CounterService будут использовать один и тот же объект RandomCounter.
// При следующем запросе к приложению будет генерироваться новый объект RandomCounter.
//builder.Services.AddScoped<ICounter, RandomCounter>();
//builder.Services.AddScoped<CounterService>();
//*Singleton
// AddSingleton создает один объект для всех последующих запросов, при этом объект создается только тогда, когда он непосредственно необходим.
//builder.Services.AddSingleton<ICounter, RandomCounter>();
//builder.Services.AddSingleton<CounterService>();

var app = builder.Build();
app.UseMiddleware<CounterMiddleware>();
app.Run();




//ASP.NET Core позволяет управлять жизненным циклом внедряемых в приложении сервисов.
// С точки зрения жизненного цикла сервисы могут представлять один из следующих типов:
//*Transient: при каждом обращении к сервису создается новый объект сервиса. В течение одного запроса может быть несколько обращений к сервису,
// соответственно при каждом обращении будет создаваться новый объект. Подобная модель жизненного цикла наиболее подходит для легковесных сервисов,
// которые не хранят данных о состоянии
//*Scoped: для каждого запроса создается свой объект сервиса. То есть если в течение одного запроса есть несколько обращений к одному сервису,
// то при всех этих обращениях будет использоваться один и тот же объект сервиса.
//*Singleton: объект сервиса создается при первом обращении к нему, все последующие запросы используют один и тот же ранее созданный объект сервиса
// Для создания каждого типа сервиса предназначен соответствующий метод AddTransient(), AddScoped() и AddSingleton().

public interface ICounter
{
    int Value { get; }
}
public class RandomCounter : ICounter
{
    static Random rnd = new Random();
    private int _value;
    public RandomCounter()
    {
        _value = rnd.Next(0, 1000000);
    }
    public int Value
    {
        get => _value;
    }
}

public class CounterService
{
    public ICounter Counter { get; }
    public CounterService(ICounter counter)
    {
        Counter = counter;
    }
}

public class CounterMiddleware
{
    RequestDelegate next;
    int i = 0; // счетчик запросов
    public CounterMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext, ICounter counter, CounterService counterService)
    {
        i++;
        httpContext.Response.ContentType = "text/html;charset=utf-8";
        await httpContext.Response.WriteAsync($"Запрос {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
    }
}