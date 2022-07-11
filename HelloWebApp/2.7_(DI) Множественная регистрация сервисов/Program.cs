var builder = WebApplication.CreateBuilder(args);
//Регистрация для одной зависимости нескольких типов
builder.Services.AddTransient<IHelloService, RuHelloService>();
builder.Services.AddTransient<IHelloService, EnHelloService>();
//Регистрация одного объекта для нескольких зависимостей
builder.Services.AddSingleton<ValueStorage>();
builder.Services.AddSingleton<IGenerator>(serv => serv.GetRequiredService<ValueStorage>());
builder.Services.AddSingleton<IReader>(serv => serv.GetRequiredService<ValueStorage>());

var app = builder.Build();

app.UseMiddleware<HelloMiddleware>();
app.UseMiddleware<GeneratorMiddleware>();
app.UseMiddleware<ReaderMiddleware>();

app.Run();




//По умолчанию при внедрении зависимостей в ASP.NET Core одна зависимость сопоставляется с одним типом. Однако бывают ситуации, когда требуется отойти от этой привязки
//один к одному. Первая ситуация: для одной зависимости необходимо зарегистрировать сразу несколько конкретных реализаций. Вторая ситуация: для нескольких зависимостей
//необходимо зарегистрировать один и тот же объект.

//Регистрация для одной зависимости нескольких типов
// ASP.NET Core позволяет зарегистрировать для одной зависимости сразу несколько типов.
interface IHelloService
{
    string Message { get; }
}

class RuHelloService : IHelloService
{
    public string Message => "Привет мир";
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

//Регистрация одного объекта для нескольких зависимостей
// Теперь рассмотрим другую ситуацию: использование несколькими зависимостями одного и то же объекта. Сначала рассмотрим ситуацию, с которой мы можем столкнуться.
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
//Здесь для обоих зависимостей - IGenerator и IReader определена одна реализация - ValueStorage. При обращении по адресу "/generate" срабатывает middleware GenerateMiddleware,
//который получает сервис IGenerator и с его помощью генерирует новое значение.
//При обращении по всем иным адресам срабатывает middleware ReaderMiddleware, который получает сервис IReader и возвращает текущее значение. Однако при запуске проекта
//мы увидим, что генерируемое значение и возвращаемое значения никак не синхронизированы, потому что, несмотря на то, что оба сервиса представляют синглтоны,
//они используют два разных экземпляра класса ValueStorage