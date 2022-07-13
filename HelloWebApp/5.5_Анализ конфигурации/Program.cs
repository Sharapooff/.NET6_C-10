var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddJsonFile("config.json");

app.Map("/", (IConfiguration appConfig) =>
{
    IConfigurationSection connStrings = appConfig.GetSection("ConnectionStrings");
    string defaultConnection = connStrings.GetSection("DefaultConnection").Value;
    // Также мы могли бы использовать один вызов GetSection(), передав ему полный путь к нужной секции:
    // string defaultConnection = appConfig.GetSection("ConnectionStrings:DefaultConnection").Value;
    // Вложенные секции от родительских отделяются двоеточием.
    // Также мы могли бы получить нужное значение, используя индексаторы:
    // string defaultConnection = appConfig["ConnectionStrings:DefaultConnection"];
    // Ну и кроме того, для работы непосредственно с секцией "ConnectionStrings" нам доступен метод GetConnectionString():
    // string con = appConfig.GetConnectionString("DefaultConnection");

    return defaultConnection;
});

app.Run();




//Для работы с конфигурацией интерфейс IConfiguration определяет следующие методы:
// GetSection(name): возвращает объект IConfiguration, который представляет только определенную секцию name
// GetChildren(): возвращает все подсекции текущего объекта конфигурации в виде набора объектов IConfiguration
// GetReloadToken(): возвращает токен - объект IChangeToken, который используется для уведомления при изменении конфигурации
// GetConnectionString(name): эквивалентен вызову GetSection("ConnectionStrings")[name] и предназначается непосредственно для работы со строками подключенияк различным базам даных
// [key]: индексатор, который позволяет получить по определенному ключу key хранящееся значение


//Используя выше рассмотренные методы, мы можем провести анализ всего файла конфигурации. Например, пусть в проекте определен следующий конфигурационный файл project.json
// Проанализируем и выведем все его содержимое в браузере:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddJsonFile("project.json");
//app.Map("/", (IConfiguration appConfig) => GetSectionContent(appConfig.GetSection("projectConfig")));
//app.Run();

//string GetSectionContent(IConfiguration configSection)
//{
//    System.Text.StringBuilder contentBuilder = new();
//    foreach (var section in configSection.GetChildren())
//    {
//        contentBuilder.Append($"\"{section.Key}\":");
//        if (section.Value == null)
//        {
//            string subSectionContent = GetSectionContent(section);
//            contentBuilder.Append($"{{\n{subSectionContent}}},\n");
//        }
//        else
//        {
//            contentBuilder.Append($"\"{section.Value}\",\n");
//        }
//    }
//    return contentBuilder.ToString();
//}