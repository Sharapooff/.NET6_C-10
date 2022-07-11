var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TimeService>();

var app = builder.Build();

app.UseMiddleware<TimerMiddleware>();
app.Run(async (context) => await context.Response.WriteAsync("Main branch"));

app.Run();




//����� ���������� �������� � ��������� Services ������� WebApplicationBuilder ��� ���������� �������� ����������, � ��� ����� � � ��������� ����������� middleware.
// � middleware �� ����� �������� ����������� ����� ���������:
// ����� �����������
// ����� �������� ������ Invoke/InvokeAsync
// ����� �������� HttpContext.RequestServices
// ��� ���� ���� ���������, ��� ���������� middleware ��������� ��� ������� ���������� � ����� � ������� ����� ���������� ����� ����������.
// �� ���� ��� ����������� �������� �������������� ASP.NET Core ���������� ����� ��������� ���������. � ��� �������� ����������� �� ������������� ������������ � middleware.

//� ���������, ���� ����������� ���������� transient-������, ������� ��������� ��� ������ ��������� � ����, �� ��� ����������� �������� �� ����� ������������ ��� �� ����� ������,
//��� ��� ����������� middleware ���������� ���� ��� - ��� �������� ����������.

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
            await context.Response.WriteAsync($"������� �����: {timeService?.Time}");
        }
        else
        {
            await next.Invoke(context);
        }
    }
}

//C������ �� �� ��� �� ���������� �� ����� ����, �� ��� ����� ����� �������� ���� � �� �� �����, ��� ��� ������ TimerMiddleware ��� ������ ��� ��� ������ �������.
//������� �������� ����� ����������� middleware ������ �������� ��� �������� � ��������� ������ Singleton, ������� ��������� ���� ��� ��� ���� ����������� ��������.
//���� �� � middleware ���������� ������������ ������� � ��������� ������ Scoped ��� Transient, �� ����� �� ���������� ����� �������� ������ Invoke/InvokeAsync:
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
//            await context.Response.WriteAsync($"������� �����: {timeService?.Time}");
//        }
//        else
//        {
//            await next.Invoke(context);
//        }
//    }
//}