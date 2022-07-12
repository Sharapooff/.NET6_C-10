var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//��� ��������� ������������ �� json-����� �������� ����� ���������� � ����� AddJsonFile()
builder.Configuration.AddJsonFile("config.json");
app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");

app.Run();




//������������ � JSON _______________________________________________________________________________________________________________________________________
// ��� �������, ��� �������� ������������ � ���������� ASP.NET Core ������������ ����� json. ��� ������ � ������� json ����������� ��������� JsonConfigurationProvider,
// � ��� �������� ������������ �� json ����������� ����� ���������� AddJsonFile().
// �� ��������� � ������� ��� ���� ���� ������������ json - appsettings.json, � ����� appsettings.Development.json,
// ������� ����������� �� ��������� � ���������� � ������� �� ����� ������������ ��� �������� ���������������� ��������.
// ����� ������������ ��������� ������������ (������� "Logging") � ����������� ����� (������� "AllowedHosts"). ���� �������� ����� ����� ��������� ��������.
// ����������� ������� ����� ������ ������ ����������� ��������� ��� ������� ����� ������������.
//
// ��� ������� ������� ����� ���� json. ����, ������� � ������ ����� ���� config.json
// ��� ����������� �������� � ����� json ��� ���� ���������, ��� ��� ������ ����� ���������� �����.
// �� ��� ���� �� ����� ������������ ��� ������������ ����� ��� ������ �����:
//builder.Configuration
//                .AddJsonFile("config.json")
//                .AddJsonFile("otherconfig.json");

//� ���� �� ������ ����� ���� ���������, ������� ����� ��� �� ����, ��� � ��������� ������� �����, �� ���������� ��������������� ��������: 
//��������� �� ������� ����� �������� ��������� �������.
//�� json ����� ������� ����� ����� ������� �� ������� �������, ��������:
//{
//  "person": { "profile": { "name": "Tomas", "email":  "tom@gmail.com"} },
//  "company": { "name": "Microsoft"}
//}
//� ����� ���������� � ���� ���������, ��� ���� ������������ ���� ��������� ��� ��������� � �������� ��������:
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

//������������ � XML _______________________________________________________________________________________________________________________________________
// �� ������������� ������������ � XML-����� �������� ��������� XmlConfigurationProvider. ��� �������� xml-����� ����������� ����� ���������� AddXmlFile().
// !!! � ����� xml � ��������� ������ ���� ���������� ����������� ��� ���������� � �������� ����� ����������
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddXmlFile("config.xml");
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");
//app.Run();
// ���� � ��� ���� ������������ ����� ������ ������ �����������,�� �� ����� ���������� � ���� ������� �����, ��� � � ����� json: var personName = appConfig["person:profile:name"];

//������������ � ini-������ _______________________________________________________________________________________________________________________________________
// ��� ������ � ������������� INI ����������� ��������� IniConfigurationProvider. � ��� �������� ������������ �� INI-����� ��� ���� ������������ ����� ���������� AddIniFile().
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//builder.Configuration.AddIniFile("config.ini");
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["person"]} - {appConfig["company"]}");
//app.Run();

