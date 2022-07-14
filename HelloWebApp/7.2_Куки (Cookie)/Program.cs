var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    if (context.Request.Cookies.ContainsKey("name")) // также моможно применить метод TryGetValue() для получения кук: if (context.Request.Cookies.TryGetValue("name", out var login))
    {
        string? name = context.Request.Cookies["name"];
        await context.Response.WriteAsync($"Hello {name}!");
    }
    else
    {
        context.Response.Cookies.Append("name", "Tom");
        await context.Response.WriteAsync("Hello World!");
    }
});

app.Run();

//Куки представляют самый простой способ сохранить данные пользователя. Куки хранятся на компьютере пользователя и могут устанавливаться как на сервере, так и на клиенте.
// Так как куки посылаются с каждым запросом на сервер, то их максимальный размер ограничен 4096 байтами.

//Получение
//Для работы с куками также можно использовать контекст запроса HttpContext, который передается в качестве параметра в компоненты middleware, а также доступен в контроллерах и RazorPages.
// Чтобы получить куки, которые приходят вместе с запросом к приложению, нам надо использовать коллекцию Request.Cookies объекта HttpContext. 
// Коллекция context.Request.Cookies служит только для получения значений кук.

// Для этой коллекции определено несколько методов:
// bool ContainsKey(string key): возвращает true, если в коллекции кук есть куки с ключом key
// bool TryGetValue(string key, out string value): возвращает true, если удалось получить значение куки с ключом key в переменную value
// Стоит отметить, что куки - это строковые значения. Неважно, что вы пытаетесь сохранить в куки - все это необходимо приводить к строке и
// соответственно получаете из кук вы тоже строки.
// if (context.Request.Cookies.ContainsKey("name"))
// string name = context.Request.Cookies["name"];

//Установка
// Для установки кук, которые отправляются в ответ клиенту, применяется объект context.Response.Cookies, который представляет интерфейс IResponseCookies.
// Этот интерфейс определяет два метода:
// Append(string key, string value): добавляет для куки с ключом key значение value
// Delete(string key): удаляет куку по ключу

