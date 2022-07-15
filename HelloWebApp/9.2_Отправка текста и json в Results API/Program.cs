var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", () => Results.Content("你好", "text/plain", System.Text.Encoding.Unicode));

app.Map("/chinese", () => Results.Text("你好", "text/plain", System.Text.Encoding.Unicode));
app.Map("/", () => Results.Text("Hello World"));

app.Map("/person", () => Results.Json(new Person("Bob", 41)));   // отправка объекта Person
app.Map("/", () => Results.Json(new { name = "Tom", age = 37 })); // отправка анонимного объекта


app.Run();



//Отправка текста и метод Content
// Метод Content() отправляет текстовое содержимое и позволяет при этом задать тип содержимого и кодировку. 
// Если указан только первый параметр, то метод по умолчанию будет использовать в качестве типа содержимого "text/plain", а в качестве кодировки "utf-8"

//Text
// Метод Text() работает аналогичным образом, он также отравляет текст и принимает те же параметры.

//Отравка json
// Для отправки данных в формате json применяется метод Results.Json()
// Параметры метода:
// data: отправляемый объект
// options: объект System.Text.Json.JsonSerializerOptions ?, который задает параметры сериализации
// contentType: тип содержимого в виде строки. Если этот параметр не указан, то по умолчанию применяется тип "application/json; charset=utf-8"
// statusCode: отправляемый вместе с json код статуса. Если этот параметр не указан, то по умолчанию код статус - 200
record class Person(string Name, int Age);
// Если надо конкретизировать параметры сериализации в json, то можно использовать второй параметр метода, который представляет тип System.Text.Json.JsonSerializerOptions?:
//app.Map("/sam", () => Results.Json(new Person("Sam", 25),
//        new()
//        {
//            PropertyNameCaseInsensitive = false,
//            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.WriteAsString
//        }));

//app.Map("/bob", () => Results.Json(new Person("Bob", 41),
//        new(System.Text.Json.JsonSerializerDefaults.Web)));

//app.Map("/tom", () => Results.Json(new Person("Tom", 37),
//         new(System.Text.Json.JsonSerializerDefaults.General)));

// Нередко формат json также применяется и для отправки ошибок. В этом случае мы можем задать статусный код ошибки с помощью последнего параметра:
//app.Map("/error", () => Results.Json(new {message="Unexpected error"}, statusCode: 500));