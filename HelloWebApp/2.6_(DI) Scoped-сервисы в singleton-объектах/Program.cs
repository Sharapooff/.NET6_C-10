var builder = WebApplication.CreateBuilder(args);
//!!! ни TimeService, ни ITimer не должны иметь жизненный цикл Scoped. То есть это может быть Transient или Singleton.
builder.Services.AddTransient<ITimer, Timer>();
builder.Services.AddScoped<TimeService>();

var app = builder.Build();

app.UseMiddleware<TimerMiddleware>();

app.Run();




//Все объекты, которые используются в ASP.NET Core, имеет три варианта жизненного цикла. Singleton-объекты создаются один раз при запуске приложения,
//и при всех запросах к приложению оно использует один и тот же singleton-объект. К подобным singleton-объектам относятся, к примеру, компоненты middleware или сервисы,
//которые регистрируются с помощью метода AddSingleton().
//Transient - объекты создаются каждый раз, когда нам требуется экземпляр определенного класса. А scoped-объекты создаются по одному на каждый запрос.
//Одни объекты или сервисы с помощью встроенного механизма dependency injection можно передать в другие объекты. Наиболее распространенный способ внедрения
//объектов предсталяет инъекция через конструктор. Однако начиная с версии ASP.NET Core 2.0 мы не можем передавать scoped-сервисы в конструктор singleton-объектов.
public interface ITimer
{
    string Time { get; }
}
public class Timer : ITimer
{
    public Timer()
    {
        Time = DateTime.Now.ToLongTimeString();
    }
    public string Time { get; }
}
public class TimeService
{
    private ITimer timer;
    public TimeService(ITimer timer)
    {
        this.timer = timer;
    }
    public string GetTime() => timer.Time;
}
//TimeService получает через конструктор сервис ITimer и использует его для получения текущего времени.
//Также пусть будет определен компонент middleware TimerMiddleware:
public class TimerMiddleware
{
    TimeService timeService;
    public TimerMiddleware(RequestDelegate next, TimeService timeService)
    {
        this.timeService = timeService;
    }

    public async Task Invoke(HttpContext context)
    {
        await context.Response.WriteAsync($"Time: {timeService?.GetTime()}");
    }
}

