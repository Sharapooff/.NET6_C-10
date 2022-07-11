var builder = WebApplication.CreateBuilder(args);
//!!! �� TimeService, �� ITimer �� ������ ����� ��������� ���� Scoped. �� ���� ��� ����� ���� Transient ��� Singleton.
builder.Services.AddTransient<ITimer, Timer>();
builder.Services.AddScoped<TimeService>();

var app = builder.Build();

app.UseMiddleware<TimerMiddleware>();

app.Run();




//��� �������, ������� ������������ � ASP.NET Core, ����� ��� �������� ���������� �����. Singleton-������� ��������� ���� ��� ��� ������� ����������,
//� ��� ���� �������� � ���������� ��� ���������� ���� � ��� �� singleton-������. � �������� singleton-�������� ���������, � �������, ���������� middleware ��� �������,
//������� �������������� � ������� ������ AddSingleton().
//Transient - ������� ��������� ������ ���, ����� ��� ��������� ��������� ������������� ������. � scoped-������� ��������� �� ������ �� ������ ������.
//���� ������� ��� ������� � ������� ����������� ��������� dependency injection ����� �������� � ������ �������. �������� ���������������� ������ ���������
//�������� ����������� �������� ����� �����������. ������ ������� � ������ ASP.NET Core 2.0 �� �� ����� ���������� scoped-������� � ����������� singleton-��������.
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
//TimeService �������� ����� ����������� ������ ITimer � ���������� ��� ��� ��������� �������� �������.
//����� ����� ����� ��������� ��������� middleware TimerMiddleware:
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

