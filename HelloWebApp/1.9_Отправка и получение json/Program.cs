using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//отправка
//app.Run(async (context) =>
//{
//    Person tom = new("Tom", 22);
//    await context.Response.WriteAsJsonAsync(tom);//отправка
//});

//получение
app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    if (request.Path == "/api/user")
    {
        var message = "Некорректные данные";   // содержание сообщения по умолчанию
        try
        {
            // пытаемся получить данные json
            var person = await request.ReadFromJsonAsync<Person>();
            if (person != null) // если данные сконвертированы в Person
                message = $"Name: {person.Name}  Age: {person.Age}";
        }
        catch { }
        // отправляем пользователю данные
        await response.WriteAsJsonAsync(new { text = message });
    }
    else
    {
        response.ContentType = "text/html; charset=utf-8";
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


public record Person(string Name, int Age);


//Для отправки json можно воспользоваться методом WriteAsJson()/WriteAsJsonAsync() объекта HttpResponse.
// Этот метод позволяет сериализовать переданные в него объекты в формат JSON и автоматически для заголовка "content-type"
// устанавливает значение "application/json; charset=utf-8"

//Получение JSON. Метод ReadFromJsonAsync
// Для получения из запроса объект в формате JSON в классе HttpRequest определен метод ReadFromJsonAsync().
// Он позволяет сериализовать данные в объект определенного типа.
// Стоит отметить, что проверять на наличие json в запросе можно с помощью метода HasJsonContentType() - он возвращает true, если клиент прислал json.

//Настройка сериализации
// Пример, когда данные json не совсем соответствуют определению типа, в который надо выполнить десериализацию (отличаются имена полей Name - UserName)
// Однако объект Person все равно будет создан, просто его свойства получат значения по умолчанию (null для свойства Name и 0 для свойства Age).
// В итоге при отправке подобных данных на сервере возникнет исключение типа System.Text.Json.JsonException, а клиент получит информацию об исключении.
//  от задачи можно использовать различные решения - обрабатывть исключения, встраивать дополнительные middleware для отлова подобных ситуаций и так далее.
//  Одним из решений подобных проблем также может быть настройка сериализации/десериализации с помощью параметра типа JsonSerializerOptions,
//  которое может передаваться в метод ReadFromJsonAsync().
//public class PersonConverter : JsonConverter<Person>
//{
//    public override Person Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        var personName = "Undefined";
//        var personAge = 0;
//        while (reader.Read())
//        {
//            if (reader.TokenType == JsonTokenType.PropertyName)
//            {
//                var propertyName = reader.GetString();
//                reader.Read();
//                switch (propertyName)
//                {
//                    // если свойство Age/age и оно содержит число
//                    case "age" or "Age" when reader.TokenType == JsonTokenType.Number:
//                        personAge = reader.GetInt32();  // считываем число из json
//                        break;
//                    // если свойство Age/age и оно содержит строку
//                    case "age" or "Age" when reader.TokenType == JsonTokenType.String:
//                        string? stringValue = reader.GetString();
//                        // пытаемся конвертировать строку в число
//                        if (int.TryParse(stringValue, out int value))
//                        {
//                            personAge = value;
//                        }
//                        break;
//                    case "Name" or "name":  // если свойство Name/name
//                        string? name = reader.GetString();
//                        if (name != null)
//                            personName = name;
//                        break;
//                }
//            }
//        }
//        return new Person(personName, personAge);
//    }
//    // сериализуем объект Person в json
//    public override void Write(Utf8JsonWriter writer, Person person, JsonSerializerOptions options)
//    {
//        writer.WriteStartObject();
//        writer.WriteString("name", person.Name);
//        writer.WriteNumber("age", person.Age);

//        writer.WriteEndObject();
//    }
//}
////и заменить метод Run
//app.Run(async (context) =>
//{
//    var response = context.Response;
//    var request = context.Request;
//    if (request.Path == "/api/user")
//    {
//        var responseText = "Некорректные данные";   // содержание сообщения по умолчанию

//        if (request.HasJsonContentType())
//        {
//            // определяем параметры сериализации/десериализации
//            var jsonoptions = new JsonSerializerOptions();
//            // добавляем конвертер кода json в объект типа Person
//            jsonoptions.Converters.Add(new PersonConverter());
//            // десериализуем данные с помощью конвертера PersonConverter
//            var person = await request.ReadFromJsonAsync<Person>(jsonoptions);
//            if (person != null)
//                responseText = $"Name: {person.Name}  Age: {person.Age}";
//        }
//        await response.WriteAsJsonAsync(new { text = responseText });
//    }
//    else
//    {
//        response.ContentType = "text/html; charset=utf-8";
//        await response.SendFileAsync("html/index.html");
//    }
//});