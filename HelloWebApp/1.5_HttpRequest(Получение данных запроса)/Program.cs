var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Получение заголовков запроса (применяется свойство Headers)
app.Run(async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    var stringBuilder = new System.Text.StringBuilder("<table>");

    foreach (var header in context.Request.Headers)
    {
        stringBuilder.Append($"<tr><td>{header.Key}</td><td>{header.Value}</td></tr>");
    }
    stringBuilder.Append("</table>");
    await context.Response.WriteAsync(stringBuilder.ToString());
    
});
//app.Run(async (context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));
//app.Run(async (context) => await context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" + $"<p>QueryString: {context.Request.QueryString}</p>");
app.Run();



//Свойство Request объекта HttpContext представляет объект HttpRequest и хранит информацию о запросе в виде следующих свойств:
// Body: предоставляет тело запроса в виде объекта Stream
// BodyReader: возвращает объект типа PipeReader для чтения тела запроса
// ContentLength: получает или устанавливает заголовок Content-Length
// ContentType: получает или устанавливает заголовок Content-Type
// Cookies: возвращает коллекцию куки (объект Cookies), ассоциированных с данным запросом
// Form: получает или устанавливает тело запроса в виде форм
// HasFormContentType: проверяет наличие заголовка Content-Type
// Headers: возвращает заголовки запроса
// Host: получает или устанавливает заголовок Host
// HttpContext: возвращает связанный с данным запросом объект HttpContext
// IsHttps: возвращает true, если применяется протокол https
// Method: получает или устанавливает метод HTTP
// Path: получает или устанавливает путь запроса в виде объекта RequestPath
// PathBase: получает или устанавливает базовый путь запроса. Такой путь не должен содержать завершающий слеш
// Protocol: получает или устанавливает протокол, например, HTTP
// Query: возвращает коллекцию параметров из строки запроса
// QueryString: получает или устанавливает строку запроса
// RouteValues: получает данные маршрута для текущего запроса
// Scheme: получает или устанавливает схему запроса HTTP

//Получение заголовков запроса (применяется свойство Headers) __________________________________________
// Для большинства стандартных заголовков HTTP в этом интерфейсе определены одноименные свойства, например,
// для заголовка "content-type" определено свойство ContentType, а для заголовка "accept" - свойство Accept
// var acceptHeaderValue = context.Request.Headers.Accept;
//Также подобые заголовки, а также какие-то кастомные заголовки, для которых не определены подобные свойства, можно получить как и любой дугой элемент словаря:
// var acceptHeaderValue = context.Request.Headers["accept"];

//Получение пути запроса _________________________________________________________________________________
// Свойство path позволяет получить запрошенный путь, то есть адрес, к которому обращается клиент.
// Свойство path позволяет получить запрошенный путь, то есть адрес, к которому обращается клиент:
// app.Run(async(context) => await context.Response.WriteAsync($"Path: {context.Request.Path}"));

//Строка запроса ________________________________________________________________________________________
//Свойство QueryString позволяет получить строку запроса. Строка запроса представляет ту часть запрошенного адреса,
//которая идет после символа ? и представляет набор параметров, разделенных символом амперсанда &.
//Строка запроса (query string) НЕ входит в путь запроса (path)
//context.Response.WriteAsync($"<p>Path: {context.Request.Path}</p>" + $"<p>QueryString: {context.Request.QueryString}</p>");
//С помощью свойства Query можно получить все параметры строки запроса в виде словаря.
//foreach (var param in context.Request.Query) {
//    stringBuilder.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");}
//Соответственно можно вытащить из словаря Query значения отдельных параметров.
//string name = context.Request.Query["name"];
//string age = context.Request.Query["age"];