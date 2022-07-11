var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", () => "Index Page");
app.Map("/user", () => new Person("Tom", 37));
app.Map("/about", () => "About Page");
app.Map("/contact", () => "Contacts Page");
app.Map("/about", async (context) =>
{
    await context.Response.WriteAsync("About Page");
});
//ASP.NET Core позволяет легко получить все имеющиеся в приложении конечные точки. 
app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

app.Run();

record class Person(string Name, int Age);


//Система маршрутизации отвечает за сопоставление входящих запросов с маршрутами и на основании результатов сопоставления выбирает для обработки запроса
//определенную конечную точку приложения. Конечная точка или endpoint представляет некоторый код, который обрабатывает запрос. По сути конечная точка
//объединяет шаблон маршрута, которому должен соответствовать запрос, и обработчик запроса по этому маршруту.

//Для использования системы маршрутизации в конвейер обработки запроса добавляются два встроенных компонента middleware
// UseEndpoints()
// UseRouting()

//Метод Map
// Самым простым способом определения конечной точки в приложении является метод Map, который реализован как метод расширения для типа IEndpointRouteBuilder.
// Он добавляет конечные точки для обработки запросов типа GET.
// !!! Не стоит путать этот метод с одноименным методом Map(), который описан в статье Метод Map и который реализован как метод расширения для типа IApplicationBuilder



