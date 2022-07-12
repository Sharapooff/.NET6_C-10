var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ��������� �������� ������������
app.Configuration["name"] = "Tom";
app.Configuration["age"] = "37";

app.Run(async (context) =>
{
    // ��������� �������� �����������
    string name = app.Configuration["name"];
    string age = app.Configuration["age"];
    await context.Response.WriteAsync($"{name} - {age}");
});

app.Run();




//���������� ASP.NET Core ����� �������� ���������������� ��������� �� ��������� ����������:
// ��������� ��������� ������
// ���������� ����� ���������
// ������� .NET � ������
// ����� (json, xml, ini)
// Azure
// ����� ������������ ���� ��������� ��������� � ��� ��� ��������� ���������� ������������

//��������� IConfiguration
//������������ ���������� � ASP.NET Core ������������ ������ ���������� IConfiguration:
//public interface IConfiguration
//{
//    string this[string key] { get; set; }
//    IEnumerable<IConfigurationSection> GetChildren();
//    IChangeToken GetReloadToken();
//    IConfigurationSection GetSection(string key);
//}
//������ ��������� �������� ��������� ����������:
//this[string key]: ����������, ����� ������� ����� �������� �� ����� �������� ��������� ������������. ����� ��������, ��� � ����,
//� �������� ��������� ������������ ������������ ����� ������ ���� string
//GetChildren(): ���������� ����� ��������� ������� ������ ������������ � ���� ������� IEnumerable<IConfigurationSection>
//GetReloadToken(): ���������� ������ IChangeToken, ������� ����������� ��� ������������ ��������� ������������
//GetSection(string key): ���������� ������ ������������, ������� ������������� ����� key
//����� ������������ ����� ���� ������������ ����������� IConfigurationRoot, ������� ����������� �� IConfiguration:
//public interface IConfigurationRoot : IConfiguration
//{
//    IEnumerable<IConfigurationProvider> Providers { get; }
//    void Reload();
//}
//�������� Providers ���������� ��������� ����������� ����������� ������������. ������ ��������� ������������ ������������ ������ IConfigurationProvider
//����� Reload() ������������� �������� �� ���� ����������� ���������� ������������
//����, ������ IConfiguration �� ���� ������ ��� ���������������� ��������� � ���� ������ ��� "����"-"��������".

//��������� ������ ������������
// � ���������� ��������� ������������ �������� � �������� Configuration ������� WebApplication. �������������� ����� ��� ��������
// �� ����� ���������� ��� �������� ��������� ������������:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//// ��������� �������� ������������
//app.Configuration["name"] = "Tom";
//app.Configuration["age"] = "37";
//app.Run(async (context) =>
//{
//    // ��������� �������� �����������
//    string name = app.Configuration["name"];
//    string age = app.Configuration["age"];
//    await context.Response.WriteAsync($"{name} - {age}");
//});
//app.Run();

//���������� ��������� ������������
//� ������� ���� ��������� ������������ ��������������� �� ����������� - ������� ��������� "name", ����� ��������� "age". ������ ���� �������� ����� ��� 
//    ���� ��� ����� ������� ���������, ������� ����� ���������� �� ����� ������, �������� � ������, ����� ��������� �������� � ����� json, xml ��� �������
//    �� ������-�� ������� ��������� ������������. ��� ���������� ��������� ������������ � ���������� ����� ��������� �������� Configuration ������� WebApplicationBuilder.
//    ��� �������� ������������ ����� ConfigurationManager, ��� �������� ��������� ��� ������� ��� ���������� ������������.
//var builder = WebApplication.CreateBuilder();
//builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
//{
//    {"name", "Tom"},
//    {"age", "37"}
//});
//var app = builder.Build();
//app.Run(async (context) =>
//{
//    // ��������� �������� �����������
//    string name = app.Configuration["name"];
//    string age = app.Configuration["age"];
//    await context.Response.WriteAsync($"{name} - {age}");
//});
//app.Run();
//����� ��� ���������� ������������ ����������� ����� AddInMemoryCollection(). ���� ����� ��������� ����� �������� � ���� ��������� ��� ����-��������

//��������� ������������ ����� Dependancy Injection
// ������������ ���������� � ���� ������� IConfiguration ������������ ���� �� ��������, ������� ����������� � ���������� �� ���������. 
//�������������� ��� ������������ ���������� �� ����� �������� ��� � ����� ������ ������ ����� �������� ��������� ������������. ��������:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//// ��������� �������� ������������
//app.Configuration["name"] = "Tom";
//app.Configuration["age"] = "37";
//// ����� �������� ��������� ������������ ������� ������ IConfiguration
//app.Map("/", (IConfiguration appConfig) => $"{appConfig["name"]} - {appConfig["age"]}");
//app.Run();
//� ������ ������ ���������� ������� � ������ app.Map() � �������� ��������� appConfig �������� ������ IConfiguration - �� ���� ��� ��� �� ����� ������ IConfiguration,
//��� � app.Configuration. �������� �������� �� ����� �������� ������������ � ������ ������ ����������, �������� ���, ��� ������ WebApplication ��� ����������.