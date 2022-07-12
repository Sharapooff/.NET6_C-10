var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Для установки конфигурации из json-файла название файла передается в метод AddJsonFile()
builder.Configuration.AddJsonFile("config.json");
app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");

app.Run();




//Конфигурация в JSON _______________________________________________________________________________________________________________________________________
// Как правило, для хранения конфигурации в приложении ASP.NET Core используются файлы json. Для работы с файлами json применяется провайдер JsonConfigurationProvider,
// а для загрузки конфигурации из json применяется метод расширения AddJsonFile().
// По умолчанию в проекте уже есть файл конфигурации json - appsettings.json, а также appsettings.Development.json,
// которые загружаются по умолчанию в приложении и которые мы можем использовать для хранения конфигурационных настроек.
// Здесь определяются настройки логгирования (элемент "Logging") и разрешенные хосты (элемент "AllowedHosts"). Одни элементы могут иметь вложенные элементы.
// Аналогичным образом можно задать другие необходимые настройки или удалить ранее определенные.
//
// Для примера возьмем новый файл json. Итак, добавим в проект новый файл config.json
// При определении настроек в файле json нам надо учитывать, что они должны иметь уникальные ключи.
// Но при этом мы можем использовать для конфигурации более чем одного файла:
//builder.Configuration
//                .AddJsonFile("config.json")
//                .AddJsonFile("otherconfig.json");

//И если во втором файле есть настройки, которые имеют тот же ключ, что и настройки первого файла, то происходит переопределение настроек: 
//настройки из второго файла заменяют настройки первого.
//Но json может хранить также более сложные по составу объекты, например:
//{
//  "person": { "profile": { "name": "Tomas", "email":  "tom@gmail.com"} },
//  "company": { "name": "Microsoft"}
//}
//И чтобы обратиться к этой настройке, нам надо использовать знак двоеточия для обращения к иерархии настроек:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddJsonFile("config.json");
//app.Map("/", (IConfiguration appConfig) =>
//{
//    var personName = appConfig["person:profile:name"];
//    var companyName = appConfig["company:name"];
//    return $"{personName} - {companyName}";
//});
//app.Run();

//Конфигурация в XML _______________________________________________________________________________________________________________________________________
// За использование конфигурации в XML-файле отвечает провайдер XmlConfigurationProvider. Для загрузки xml-файла применяется метод расширения AddXmlFile().
// !!! У файла xml в свойствах должно быть выставлено копирование при компиляции в выходную папку приложения
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddXmlFile("config.xml");
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");
//app.Run();
// Если у нас файл конфигурации имеет разные уровни вложенности,то мы можем обращаться к этим уровням также, как и в файле json: var personName = appConfig["person:profile:name"];

//Конфигурация в ini-файлах _______________________________________________________________________________________________________________________________________
// Для работы с конфигурацией INI применяется провайдер IniConfigurationProvider. А для загрузки конфигурации из INI-файла нам надо использовать метод расширения AddIniFile().
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddIniFile("config.ini");
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");
//app.Run();

