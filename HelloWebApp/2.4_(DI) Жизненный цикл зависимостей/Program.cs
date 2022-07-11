var builder = WebApplication.CreateBuilder(args);
//*Transient
// CounterMiddleware �������� ������ ICounter, ��� �������� ��������� ���� ��������� ������ RandomCounter. CounterMiddleware ����� �������� ������ CounterService,
// ������� ����� ���������� ICounter. � ��� ����� ICounter ����� ����������� ������ ��������� ������ RandomCounter.
// ������� ������������ ��������� ����� ������ ������������ �� ���������. ����� �������, ���������� AddTransient ������� ��� ������ ������� RandomCounter.
builder.Services.AddTransient<ICounter, RandomCounter>();
builder.Services.AddTransient<CounterService>();
//*Scoped
// ����� AddScoped ������� ���� ��������� ������� ��� ����� �������.
// ������ � ������ ������ � ���� �� ������� � CounterMiddleware � ������ CounterService ����� ������������ ���� � ��� �� ������ RandomCounter.
// ��� ��������� ������� � ���������� ����� �������������� ����� ������ RandomCounter.
//builder.Services.AddScoped<ICounter, RandomCounter>();
//builder.Services.AddScoped<CounterService>();
//*Singleton
// AddSingleton ������� ���� ������ ��� ���� ����������� ��������, ��� ���� ������ ��������� ������ �����, ����� �� ��������������� ���������.
//builder.Services.AddSingleton<ICounter, RandomCounter>();
//builder.Services.AddSingleton<CounterService>();

var app = builder.Build();
app.UseMiddleware<CounterMiddleware>();
app.Run();




//ASP.NET Core ��������� ��������� ��������� ������ ���������� � ���������� ��������.
// � ����� ������ ���������� ����� ������� ����� ������������ ���� �� ��������� �����:
//*Transient: ��� ������ ��������� � ������� ��������� ����� ������ �������. � ������� ������ ������� ����� ���� ��������� ��������� � �������,
// �������������� ��� ������ ��������� ����� ����������� ����� ������. �������� ������ ���������� ����� �������� �������� ��� ����������� ��������,
// ������� �� ������ ������ � ���������
//*Scoped: ��� ������� ������� ��������� ���� ������ �������. �� ���� ���� � ������� ������ ������� ���� ��������� ��������� � ������ �������,
// �� ��� ���� ���� ���������� ����� �������������� ���� � ��� �� ������ �������.
//*Singleton: ������ ������� ��������� ��� ������ ��������� � ����, ��� ����������� ������� ���������� ���� � ��� �� ����� ��������� ������ �������
// ��� �������� ������� ���� ������� ������������ ��������������� ����� AddTransient(), AddScoped() � AddSingleton().

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
    int i = 0; // ������� ��������
    public CounterMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext, ICounter counter, CounterService counterService)
    {
        i++;
        httpContext.Response.ContentType = "text/html;charset=utf-8";
        await httpContext.Response.WriteAsync($"������ {i}; Counter: {counter.Value}; Service: {counterService.Counter.Value}");
    }
}