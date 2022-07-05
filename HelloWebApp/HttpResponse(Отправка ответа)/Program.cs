var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Run(async (context) => await context.Response.WriteAsync("Hello World!", System.Text.Encoding.Default));
//Здесь параметр context, который передается в middleware в методе app.Run() как раз представляет объект HttpContext. И через этот объект,
//точнее через его свойство Response мы можем отправить клиенту некоторый ответ: context.Response.WriteAsync().

app.Run(async (context) =>//Установка заголовков
{
    var response = context.Response;
    response.Headers.ContentLanguage = "ru-RU";
    response.Headers.ContentType = "text/plain; charset=utf-8";
    response.Headers.Append("key-id", "010");    // добавление кастомного заголовка
    
    //response.StatusCode = 404; // установка кодов статуса
    
    //response.ContentType = "text/html; charset=utf-8";
    //await response.WriteAsync("<h2>Hello World!</h2><h3>Welcome to ASP.NET Core</h3>");
    
    await response.WriteAsync("Hello World!!!");
});

app.Run();


//Все данные запроса передаются в middleware через объект Microsoft.AspNetCore.Http.HttpContext. Этот объект инкапсулирует информацию о запросе,
//позволяет управлять ответом и, кроме того, имеет еще много другой функциональности.

//Свойство Response объекта HttpContext представляет объект HttpResponse и устанавливает, что будет отравляться в виде ответа. 
//Для установки различных аспектов ответа класс HttpResponse определяет следующие свойства:
//    Body: получает или устанавливает тело ответа в виде объекта Stream
//    BodyWriter: возвращает объект типа PipeWriter для записи ответа
//    ContentLength: получает или устанавливает заголовок Content-Length
//    ContentType: получает или устанавливает заголовок Content-Type
//    Cookies: возвращает куки, отправляемые в ответе
//    HasStarted: возвращает true, если отправка ответа уже началась
//    Headers: возвращает заголовки ответа
//    Host: получает или устанавливает заголовок Host
//    HttpContext: возвращает объект HttpContext, связанный с данным объектом Response
//    StatusCode: возвращает или устанавливает статусный код ответа
//    Чтобы отправить ответ, мы можем использовать ряд методов класса HttpResponse:
//    Redirect(): выполняет переадресацию(временную или постоянную) на другой ресурс
//    WriteAsJson()/ WriteAsJsonAsync(): отправляет ответ в виде объектов в формате JSON
//    WriteAsync(): отправляет некоторое содержимое. Одна из версий метода позволяет указать кодировку. Если кодировка не указана, то по умолчанию применяется кодировка UTF-8
//    SendFileAsync(): отправляет файл


//Установка заголовков
//    Для установки заголовков применяется свойство Headers, которое представляет тип IHeaderDictionary. Для большинства стандартных заголовков HTTP
//    в этом интерфейсе определены одноименные свойства, например, для заголовка "content-type" определено свойство ContentType.
//    Другие, в том числе свои кастомные заголовки можно добавить через метод Append().

//Установка кодов статуса
//Для установки статусных кодов применяется свойство StatusCode, которому передается числовой код статуса/

//Отправка html-кода
//  Если необходимо отправить html-код, то для этого необходимо установить для заголовка Content-Type значение text/html.