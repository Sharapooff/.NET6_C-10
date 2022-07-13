var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Configuration.AddJsonFile("person.json");
var tom = new Person();
app.Configuration.Bind(tom);    // ��������� ������������ � �������� tom, �������� ������ ����������� � ���������� ������ Bind
// � �������� ������������ ������ Bind �� ����� �� ������������ ����� Get<T>(), ������� ���������� ������ ���������� ������:
// Person tom = app.Configuration.Get<Person>();
// �������� ������� ����� ��������� �������� ��� ��������� ������������ ����� �������� Dependency Injection:
// app.Map("/", (IConfiguration appConfig) =>
// {
//     var tom = appConfig.Get<Person>();  // ��������� ������������ � �������� tom
//     return $"{tom.Name} - {tom.Age}";
// });

app.Run(async (context) => await context.Response.WriteAsync($"{tom.Name} - {tom.Age}"));

app.Run();


//��������� ASP.NET Core ��������� ������������ ���������������� ��������� �� ������ C#.
// ��������, ��������� � ������� ����� ���� person.json, ������� ����� ������� ������ ������������ � ������ ������������ �� ����� person.json � �������� ������ Person:
public class Person
{
    public string Name { get; set; } = "";
    public int Age { get; set; } = 0;
}

//�������� ������� ��������
// ���������� �������� ����� ������� �� ��������� ������. ��������� ��������� ���� person_new.json
// ��� ������������� ���� ������ � ���� C# ��������� ��������� ������:
public class Person_new
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public List<string> Languages { get; set; } = new();
    public Company? Company { get; set; }
}
public class Company
{
    public string Title { get; set; } = "";
    public string Country { get; set; } = "";
}

// ������ �������� � ���������� �������� �� ������������ json � ������� ������� C#:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();

//builder.Configuration.AddJsonFile("person_new.json");

//var tom = new Person_new();
//app.Configuration.Bind(tom);

//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    string name = $"<p>Name: {tom.Name}</p>";
//    string age = $"<p>Age: {tom.Age}</p>";
//    string company = $"<p>Company: {tom.Company?.Title}</p>";
//    string langs = "<p>Languages:</p><ul>";
//    foreach (var lang in tom.Languages)
//    {
//        langs += $"<li><p>{lang}</p></li>";
//    }
//    langs += "</ul>";

//    await context.Response.WriteAsync($"{name}{age}{company}{langs}");
//});
//app.Run();

//�������� ������������ �� xml
// ������� ���� ������������ ������ Person_new � Company. � ������� � ������ ���� person.xml, ������� ����� ��������� ����������� ������/
// �������� �������� �� ��������� � ����� xml �������� - ��� ����� ������� name, ������� ���������� �������� ������.
// �������� ������������ �� ���� ������������� ����� xml � ����������:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();

//builder.Configuration.AddXmlFile("person.xml");

//var tom = new Person_new();
//app.Configuration.Bind(tom);

//app.Run(async (context) =>
//{
//    context.Response.ContentType = "text/html; charset=utf-8";
//    string name = $"<p>Name: {tom.Name}</p>";
//    string age = $"<p>Age: {tom.Age}</p>";
//    string company = $"<p>Company: {tom.Company?.Title}</p>";
//    string langs = "<p>Languages:</p><ul>";
//    foreach (var lang in tom.Languages)
//    {
//        langs += $"<li><p>{lang}</p></li>";
//    }
//    langs += "</ul>";

//    await context.Response.WriteAsync($"{name}{age}{company}{langs}");
//});
//app.Run();
// �������� ������ ����������� ����� � json �� xml, � ���� ��������� ��� �������� �������.

//�������� ������ ������������
// � �������� ���� ����������� �������� ��������� ������� ������������, ������ ����� ����� ������������ �������� ��������� ������. ��������, ���� � ����� json � xml
// ���� ���������� ������ company, ������� ������ �������� ������������. �������� �������� �������� ���� ������ � ������� ������ Company:

//�������� ������ ������������
// � �������� ���� ����������� �������� ��������� ������� ������������, ������ ����� ����� ������������ �������� ��������� ������. ��������, ���� � ����� json � xml
// ���� ���������� ������ company, ������� ������ �������� ������������. �������� �������� �������� ���� ������ � ������� ������ Company:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();

//builder.Configuration.AddJsonFile("person.json");

//Company company = app.Configuration.GetSection("company").Get<Company>();

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync($"{company.Title} - {company.Country}");
//});
//app.Run();
// � ������� ������ GetSection() �������� ������ ��� ������ ������������ � ����� ����� ����� ������� ������ Bind ��� Get � ��������� ��������.