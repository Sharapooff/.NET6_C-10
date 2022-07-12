var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");

app.Run();




//�������� ���������� ��������� ������
// ��� ������������ �������� ���������� ��������� ������ ������� � ������� � ����� Properties ���� launchSettings.json � ������� "commandLineArgs": "name=Bob age=37"
// ������������� ����� ���������� ��������� ��������� ������.

//������ ����� �������
// ����, ������ ���� ������������ ��������� � �������� ������������ �������. ������� ��������� ������ � �������� � ������� � ������� ������� cd � ����� �������. 
// ����� ������ ��������� �������:
// dotnet run name=Tom age=35

//����������� ��������� ���������� ��������� ������
// ����� �� ����� �� ������ ���� ������������ �������� ���������� ��������� ������:
// string[] commandLineArgs = { "name=Alice", "age=29" };  // ��������������� ��������� ������
// var builder = WebApplication.CreateBuilder(commandLineArgs);
// var app = builder.Build();
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Run();

//���������� ������ AddCommandLine
// ����� ����� ���� �� �������� ��������� ��������� ������ ����� ����� AddCommandLine():
// var builder = WebApplication.CreateBuilder();
// string[] commandLineArgs = { "name=Sam", "age=25" };  // ��������������� ��������� ������
// builder.Configuration.AddCommandLine(commandLineArgs);  // �������� ��������� � �������� ������������
// var app = builder.Build();
// app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
// app.Run();

//���������� ����� ��������� ��� �������� ������������
// ��� �������� ���������� ����� ��������� � �������� ���������� ������������ ����������� ��������� EnvironmentVariablesConfigurationProvider.
// ��� ��� ������������� � ������� ConfigurationManager ���������� ����� AddEnvironmentVariables(). ������ � ���������� ���� �� ��� �������� ����� ������������,
// ��� ��� ����� ASP.NET Core ��� ��������� ���������� ����� ��������� � ������ ������������ �� ���������.
// ��������, ������� ���������� ��������� "JAVA_HOME", ������� ��������� �� ����� ��������� java sdk, ���� ��� ���������� ����������
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Map("/", (IConfiguration appConfig) => $"JAVA_HOME: {appConfig["JAVA_HOME"] ?? "not set"}");
//app.Run();

//�������� ������������ � ������
//��������� MemoryConfigurationProvider ��������� ������������ � �������� ������������ ��������� IEnumerable<KeyValuePair<string, string>>, ������� ������ ������ � ����
//���� ����-�������� (������ - ������ Dictionary). ��� ���������� ��������� ������������ ����������� ����� AddInMemoryCollection(),
//� ������� ���������� ������� ���������������� ��������:
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




