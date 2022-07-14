var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Items["text"] = "Hello from HttpContext.Items";
    context.Items.Add("message", "Hello world");
    await next.Invoke();
});
app.Run(async (context) =>
{
    if (context.Items.ContainsKey("message"))
        await context.Response.WriteAsync($"Message: {context.Items["message"]}");
    else
        await context.Response.WriteAsync("Random Text");
});
app.Run();




//Один из простейших способов хранения данных представляет коллекция HttpContext.Items - объект типа IDictionary<object, object>.
// Эта коллекция предназначена для таких данных, которые непосредственно связаны с текущим запросом. После завершения запроса все данные из HttpContext.Items удаляются.
// Каждый объект в этой коллекции имеет ключ и значение. И с помощью ключей можно управлять объектами коллекции. Например, если у нас обработка запроса вовлекает
// множество компонентов middleware, и мы хотим, чтобы для этих компонентов были доступны общие данные, то как раз можем применить эту коллекцию. 

//HttpContext.Items предоставляет ряд методов для управления элементами:
// void Add(object key, object value): добавляет объект value с ключом key
// void Clear(): удаляет все объекты
// bool ContainsKey(object key): возвращает true, если словарь содержит объект с ключом key
// bool Remove(object key): удаляет объект с ключом key, в случае удачного удаления возвращает true
// bool TryGetValue(object key, out object value): возвращает true, если значение объекта с ключом key успешно получено в объект value

