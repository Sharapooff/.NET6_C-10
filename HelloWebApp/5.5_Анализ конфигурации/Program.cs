var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddJsonFile("config.json");

app.Map("/", (IConfiguration appConfig) =>
{
    IConfigurationSection connStrings = appConfig.GetSection("ConnectionStrings");
    string defaultConnection = connStrings.GetSection("DefaultConnection").Value;
    // ����� �� ����� �� ������������ ���� ����� GetSection(), ������� ��� ������ ���� � ������ ������:
    // string defaultConnection = appConfig.GetSection("ConnectionStrings:DefaultConnection").Value;
    // ��������� ������ �� ������������ ���������� ����������.
    // ����� �� ����� �� �������� ������ ��������, ��������� �����������:
    // string defaultConnection = appConfig["ConnectionStrings:DefaultConnection"];
    // �� � ����� ����, ��� ������ ��������������� � ������� "ConnectionStrings" ��� �������� ����� GetConnectionString():
    // string con = appConfig.GetConnectionString("DefaultConnection");

    return defaultConnection;
});

app.Run();




//��� ������ � ������������� ��������� IConfiguration ���������� ��������� ������:
// GetSection(name): ���������� ������ IConfiguration, ������� ������������ ������ ������������ ������ name
// GetChildren(): ���������� ��� ��������� �������� ������� ������������ � ���� ������ �������� IConfiguration
// GetReloadToken(): ���������� ����� - ������ IChangeToken, ������� ������������ ��� ����������� ��� ��������� ������������
// GetConnectionString(name): ������������ ������ GetSection("ConnectionStrings")[name] � ��������������� ��������������� ��� ������ �� �������� ������������ ��������� ����� �����
// [key]: ����������, ������� ��������� �������� �� ������������� ����� key ���������� ��������


//��������� ���� ������������� ������, �� ����� �������� ������ ����� ����� ������������. ��������, ����� � ������� ��������� ��������� ���������������� ���� project.json
// �������������� � ������� ��� ��� ���������� � ��������:
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