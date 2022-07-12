var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");

app.Run();




//Загрузка аргументов командной строки
// Для тестирования передачи параметров командной строки изменим в проекте в папке Properties файл launchSettings.json и добавим "commandLineArgs": "name=Bob age=37"
// Автоматически может подхватить параметры командной строки.

//Запуск через консоль
// Итак, удалим выше определенные параметры и выполним перестроение проекта. Откроем командную строку и перейдем в консоли с помощью команды cd в папку проекта. 
// Далее введем следующую команду:
// dotnet run name=Tom age=35

//Программная симуляция аргументов командной строки
// Также мы можем на уровне кода симулировать передачу параметров командной строки:
// string[] commandLineArgs = { "name=Alice", "age=29" };  // псевдопараметры командной строки
// var builder = WebApplication.CreateBuilder(commandLineArgs);
// var app = builder.Build();
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Run();

//Применение метода AddCommandLine
// Также можно было бы передать параметры командной строки через метод AddCommandLine():
// var builder = WebApplication.CreateBuilder();
// string[] commandLineArgs = { "name=Sam", "age=25" };  // псевдопараметры командной строки
// builder.Configuration.AddCommandLine(commandLineArgs);  // передаем параметры в качестве конфигурации
// var app = builder.Build();
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Run();

//Переменные среды окружения как источник конфигурации
// Для загрузки переменных среды окружения в качестве параметров конфигурации применяется провайдер EnvironmentVariablesConfigurationProvider.
// Для его использования у объекта ConfigurationManager вызывается метод AddEnvironmentVariables(). Однако в реальности вряд ли его придется часто использовать,
// так как среда ASP.NET Core уже загружает переменные среды окружения в объект конфигурации по умолчанию.
// Например, получим переменную окружения "JAVA_HOME", которая указывает на папку установки java sdk, если эта переменная определена
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Map("/", (IConfiguration appConfig) => $"JAVA_HOME: {appConfig["JAVA_HOME"] ?? "not set"}");
//app.Run();

//Хранение конфигурации в памяти
//Провайдер MemoryConfigurationProvider позволяет использовать в качестве конфигурации коллекцию IEnumerable<KeyValuePair<string, string>>, которая хранит данные в виде
//пары ключ-значение (пример - объект Dictionary). Для добавления источника конфигурации применяется метод AddInMemoryCollection(),
//в который передается словарь конфигурационных настроек:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
//{
//    {"name", "Tom"},
//    {"age", "37"}
//});
//app.Map("/", (IConfiguration appConfig) =>
//{
//    var name = appConfig["name"];
//    var age = appConfig["age"];
//    return $"{name} - {age}";
//});
//app.Run();




