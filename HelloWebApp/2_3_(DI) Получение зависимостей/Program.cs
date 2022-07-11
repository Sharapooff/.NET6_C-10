var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ITimeService, ShortTimeService>(); // ��������� ������
//������������ ������ ___
builder.Services.AddTransient<TimeMessage>();
//����� Invoke/InvokeAsync ����������� middleware
builder.Services.AddTransient<ITimeService, ShortTimeService>();

var app = builder.Build();

app.Run(async context =>
{
    //WebApplication (service locator)___
    // �������� �� ��������� �������� ������ ������� ITimeService - � ������ ������ �� ����� ������������ ������ ShortTimeService
    var timeService = app.Services.GetService<ITimeService>(); // ���� ������ �� ��������, ������ null, ��� ��������� ����������
    //var timeService = app.Services.GetRequiredService<ITimeService>(); // ���� ������ �� ��������, ������� ����������
    //HttpContext.RequestServices ___
    //var timeService = context.RequestServices.GetService<ITimeService>();
    //������������ ������ ___
    //var timeMessage = context.RequestServices.GetService<TimeMessage>();
    //context.Response.ContentType = "text/html;charset=utf-8";
    //await context.Response.WriteAsync($"<h2>{timeMessage?.GetTime()}</h2>");
    //����� Invoke/InvokeAsync ����������� middleware ___
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



//� ASP.NET Core �� ����� �������� ����������� � ���������� ������� ���������� ���������;
// - ����� �������� Services ������� WebApplication (service locator)
// - ����� �������� RequestServices ��������� ������� HttpContext � ����������� middleware (service locator)
// - ����� ����������� ������
// - ����� �������� ������ Invoke ���������� middleware
// - ����� �������� Services ������� WebApplicationBuilder

//�������� Services ������� WebApplication (service locator)_________________________________________________________________________________________________
// ���, ��� ��� �������� ������ WebApplication, ������� ������������ ������� ����������, (��������, � ����� Program.cs),
// ��� ��������� �������� �� ����� ������������ ��� �������� Services. 
// GetService<service>(): ���������� ��������� �������� ��� �������� �������, ������� ������������ ��� service.
//                        � ������ ���� � ���������� �������� ��� ������� ������� �� ����������� �����������, �� ���������� �������� null
// GetRequiredService<service>(): ���������� ��������� �������� ��� �������� �������, ������� ������������ ��� service.
//                        � ������ ���� � ���������� �������� ��� ������� ������� �� ����������� �����������, �� ���������� ����������
// ������ ������� ��������� ������� ��� ���������� service locator, �, ��� �������, �� ������������� � �������������, �� ��� �� ����� � ������ ASP.NET Core
// � �������� �� ����� ������������ �������� ���������������� �������� ���, ��� ������ ������� ��������� ������������ �� �������.

//HttpContext.RequestServices _________________________________________________________________________________________________________________
// ���, ��� ��� �������� ������ HttpContext, �� ����� ������������ ��� ��������� �������� ��� �������� RequestServices. ��� �������� ������������� ������ IServiceProvider.
// �� ���� �� ���� �� ����� ���� � ���� ��������� �������� ��������� �������� � ������� ������� GetService() � GetRequiredService()

//������������ ������ _________________________________________________________________________________________________________________
// ���������� � ASP.NET Core ������� ��������� ������������ ���������� ������������ ������� ��� �������� ���� ������������.
// �������� �������� ����� ������������ �������� ���������������� �������� ��������� ������������.
class TimeMessage
{
    ITimeService timeService;
    public TimeMessage(ITimeService timeService)
    {
        this.timeService = timeService;
    }
    public string GetTime() => $"Time: {timeService.GetTime()}";
}
// ���� ����� ����������� ������ ���������� ����������� �� ITimeService. ������ ����� ����������, ��� ��� ����� �� ���������� ���������� ITimeService.
// � ������ GetTime() ��������� ���������, � ������� �� ������� �������� ������� �����.


//����� Invoke/InvokeAsync ����������� middleware ___________________________________________________
// ������� ����, ��� ����������� ���������� � ����������� �������, ����� ����� �� ����� ���������� � ����� Invoke/InvokeAsync() ���������� middleware.
// ��������, ��������� ��������� ���������:
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