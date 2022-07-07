var builder = WebApplication.CreateBuilder(args);
//��������� ������ AddTransient<ITimeService, ShortTimeService>() ������� �� ����� �������� ���������� ITimeService ����� ���������� ���������� ������ ShortTimeService.
// ���������� ��������
builder.Services.AddTransient<ITimeService, ShortTimeService>();
builder.Services.AddTransient<TimeService_One>(); // ��� ���������� �����
builder.Services.AddTimeService(); // ������ ���������� � ���� ������� ���������� ��� ���������� IServiceCollection
// ��� builder.Services.AddTransient<ITimeService, LongTimeService>();
// ������� ����������� �� �������� ������� WebApplication ������� Build()
// �������� ������� WebApplication
var app = builder.Build();

app.Run(async context =>
{
    // ����� ���������� ������� ��� ����� �������� � ������������ � ����� ����� ����������. ��� ��������� ������� ����� ����������� ��������� �������
    // � ����������� �� ��������.� ������ ������ ������������ �������� app.Services., ������� ������������� ��������� �������� -������ IServiceProvider.
    // ��� ��������� ������� � ���������� ������� ���������� ����� GetService(), ������� ������������ ����� �������:
    var timeService = app.Services.GetService<ITimeService>();
    var timeService_One = app.Services.GetService<TimeService_One>();
    // ����� ��������� ������� �� ����� ������������ ���.
    await context.Response.WriteAsync($"Time: {timeService?.GetTime()}  -  {timeService?.GetTime()}");
});

app.Run();


//��������� ����� ��������� ITimeService, ������� ������������ ��� ��������� �������:
interface ITimeService
{
    string GetTime();
}

// ����� � ������� hh::mm
class ShortTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}
// ����� � ������� hh:mm:ss
class LongTimeService : ITimeService
{
    public string GetTime() => DateTime.Now.ToLongTimeString();
}


//������ ��� ���������� �����
// ��� ���� ������������� ��������� ����������� ������� � ���� ���������� � ��� ����������. ��� ������ "������" � ������ ������ ����� ������������ ����� ������, 
// ���������������� �������� ����� �������������� � ����������.
// ��������, ��������� ����� ����� TimeService:
public class TimeService_One
{
    public string GetTime() => DateTime.Now.ToShortTimeString();
}
// ������ ����� ���������� ���� ����� GetTime(), ������� ���������� ������� �����.

//���������� ��� ���������� ��������
// ������� ��� �������� ������� ����������� ������ ���������� � ���� ������� ���������� ��� ���������� IServiceCollection.
// ��������, �������� �������� ����� ��� ������� TimeService:
public static class ServiceProviderExtensions
{
    public static void AddTimeService(this IServiceCollection services)
    {
        services.AddTransient<TimeService_One>();
    }
}