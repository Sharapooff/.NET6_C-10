using System.Text;

var builder = WebApplication.CreateBuilder(args);
IServiceCollection allServices = builder.Services;  // коллекция сервисов
//их можем использовать в различных частях приложения
var app = builder.Build();

app.Run(async context =>
{
    var sb = new StringBuilder();
    sb.Append("<h1>Все сервисы</h1>");
    sb.Append("<table>");
    sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");
    foreach (var svc in allServices)
    {
        sb.Append("<tr>");
        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
        sb.Append($"<td>{svc.Lifetime}</td>");
        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
        sb.Append("</tr>");
    }
    sb.Append("</table>");
    context.Response.ContentType = "text/html;charset=utf-8";
    await context.Response.WriteAsync(sb.ToString());
});

app.Run();




//Dependency injection (DI) или внедрение зависимостей представляет механизм, который позволяет сделать взаимодействующие в приложении объекты слабосвязанными.
//Такие объекты связаны между собой через абстракции, например, через интерфейсы, что делает всю систему более гибкой, более адаптируемой и расширяемой.

//В центре подобного механизма находится понятие зависимость - некоторая сущность, от которой зависит другая сущность. 
//class Logger
//{
//    public void Log(string message) => Console.WriteLine(message);
//}
//class Message
//{
//    Logger logger = new Logger();
//    public string Text { get; set; } = "";
//    public void Print() => logger.Log(Text);
//}
// Сущность Message зависит от сущности - Logger
// Если мы захотим вместо класса Logger использовать другой тип тип логгера, например, логгировать в файл, а не на консоль, то нам придется менять класс Message.
// В итоге такими системами сложнее управлять и сложнее тестировать.

//Чтобы отвязать объект Logger от класса Message, мы можем создать абстракцию, которая будет представлять логгер, и передавать ее извне в объект Message:
interface ILogger
{
    void Log(string message);
}
class Logger : ILogger
{
    public void Log(string message) => Console.WriteLine(message);
}
class Message
{
    ILogger logger;
    public string Text { get; set; } = "";
    public Message(ILogger logger)
    {
        this.logger = logger;
    }
    public void Print() => logger.Log(Text);
}
//Преимуществом ASP.NET Core в этом оношении является то, что фреймворк уже по умолчанию имеет встроенный контейнер внедрения зависимостей, который представлен интерфейсом
//IServiceProvider. А сами зависимости еще называются сервисами, собственно поэтому контейнер можно назвать провайдером сервисов. Этот контейнер отвечает за сопоставление
//зависимостей с конкретными типами и за внедрение зависимостей в различные объекты.

//Установка встроенных сервисов фреймворка
// За управление сервисами в приложении в классе WebApplicationBuilder определено свойство Services, которое представляет объект IServiceCollection - коллекцию сервисов:
// WebApplicationBuilder builder = WebApplication.CreateBuilder();
// IServiceCollection allServices = builder.Services;  // коллекция сервисов
// И даже если мы не добавляем в эту коллекцию никаких сервисов, IServiceCollection уже содержит ряд сервисов по умолчанию

//Информация о сервисах
// Каждый сервис в коллекции IServiceCollection представляет объект ServiceDescriptor, который несет некоторую информацию.
// В частности, наиболее важные свойства этого объекта:
// ServiceType: тип сервиса
// ImplementationType: тип реализации сервиса
// Lifetime: жизненный цикл сервиса

//Регистрация встроенных сервисов ASP.NET Core
// Кроме ряда подключаемых по умолчанию сервисов ASP.NET Core имеет еще ряд встроенных сервисов, которые мы можем подключать в приложение при необходимости.
// Все сервисы и компоненты middleware, которые предоставляются ASP.NET по умолчанию, регистрируются в приложение с помощью методов расширений
// IServiceCollection, имеющих общую форму Add[название_сервиса].
// Например:
// var builder = WebApplication.CreateBuilder();
// builder.Services.AddMvc();
// Для объекта IServiceCollection определено ряд методов расширений, которые начинаются на Add, как, например, AddMvc().
// Эти методы добавляют в объект IServiceCollection соответствующие сервисы. Например, AddMvc() добавляет в приложение сервисы MVC,
// благодаря чему мы сможем их использовать в приложении.