var builder = WebApplication.CreateBuilder(args);
//����������� ��� ����� ����������� ���������� �����
builder.Services.AddTransient<IHelloService, RuHelloService>();
builder.Services.AddTransient<IHelloService, EnHelloService>();
//����������� ������ ������� ��� ���������� ������������
builder.Services.AddSingleton<ValueStorage>();
builder.Services.AddSingleton<IGenerator>(serv => serv.GetRequiredService<ValueStorage>());
builder.Services.AddSingleton<IReader>(serv => serv.GetRequiredService<ValueStorage>());

var app = builder.Build();

app.UseMiddleware<HelloMiddleware>();
app.UseMiddleware<GeneratorMiddleware>();
app.UseMiddleware<ReaderMiddleware>();

app.Run();




//�� ��������� ��� ��������� ������������ � ASP.NET Core ���� ����������� �������������� � ����� �����. ������ ������ ��������, ����� ��������� ������ �� ���� ��������
//���� � ������. ������ ��������: ��� ����� ����������� ���������� ���������������� ����� ��������� ���������� ����������. ������ ��������: ��� ���������� ������������
//���������� ���������������� ���� � ��� �� ������.

//����������� ��� ����� ����������� ���������� �����
// ASP.NET Core ��������� ���������������� ��� ����� ����������� ����� ��������� �����.
interface IHelloService
{
    string Message { get; }
}

class RuHelloService : IHelloService
{
    public string Message => "������ ���";
}
class EnHelloService : IHelloService
{
    public string Message => "Hello world";
}

class HelloMiddleware
{
    readonly IEnumerable<IHelloService> helloServices;

    public HelloMiddleware(RequestDelegate _, IEnumerable<IHelloService> helloServices)
    {
        this.helloServices = helloServices;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.ContentType = "text/html; charset=utf-8";
        string responseText = "";
        foreach (var service in helloServices)
        {
            responseText += $"<h3>{service.Message}</h3>";
        }
        await context.Response.WriteAsync(responseText);
    }
}

//����������� ������ ������� ��� ���������� ������������
// ������ ���������� ������ ��������: ������������� ����������� ������������� ������ � �� �� �������. ������� ���������� ��������, � ������� �� ����� �����������.
interface IGenerator
{
    int GenerateValue();
}
interface IReader
{
    int ReadValue();
}
class ValueStorage : IGenerator, IReader
{
    int value;
    public int GenerateValue()
    {
        value = new Random().Next();
        return value;
    }

    public int ReadValue() => value;
}
class GeneratorMiddleware
{
    RequestDelegate next;
    IGenerator generator;

    public GeneratorMiddleware(RequestDelegate next, IGenerator generator)
    {
        this.next = next;
        this.generator = generator;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path == "/generate")
            await context.Response.WriteAsync($"New Value: {generator.GenerateValue()}");
        else
            await next.Invoke(context);
    }
}
class ReaderMiddleware
{
    IReader reader;

    public ReaderMiddleware(RequestDelegate _, IReader reader) => this.reader = reader;

    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync($"Current Value: {reader.ReadValue()}");
    }
}
//����� ��� ����� ������������ - IGenerator � IReader ���������� ���� ���������� - ValueStorage. ��� ��������� �� ������ "/generate" ����������� middleware GenerateMiddleware,
//������� �������� ������ IGenerator � � ��� ������� ���������� ����� ��������.
//��� ��������� �� ���� ���� ������� ����������� middleware ReaderMiddleware, ������� �������� ������ IReader � ���������� ������� ��������. ������ ��� ������� �������
//�� ������, ��� ������������ �������� � ������������ �������� ����� �� ����������������, ������ ���, �������� �� ��, ��� ��� ������� ������������ ���������,
//��� ���������� ��� ������ ���������� ������ ValueStorage