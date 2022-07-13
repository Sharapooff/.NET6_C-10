using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("person.json");
// ���������� ������� ������ Person, ������� ����� ������������ ����� �������� �������� ������������, � ������������� ����� json
// ������������� ������ Person �� ���������� �� ������������
builder.Services.Configure<Person>(builder.Configuration);
var app = builder.Build();

// ����� ����� ������� ��������� ������������ �� ����� �������� ��������� ������ ����� ������ IOptions<Person>
app.Map("/", (IOptions<Person> options) =>
{
    Person person = options.Value;  // �������� ���������� ����� Options ������ Person
    return person;
});

app.Run();




//��������� ASP.NET Core ��������� ������� Options, ������� ��������� ���������� ������������ �� ������ ��� ����� �������� � ���� ��� ����-��������, � ��� �������
//������������ �������.
//��� ���������� ����� �������� � ���������� � ������� IServiceCollection, ������� ������������ ��������� �������� ����������, ��������� ����� Configure():
//public static IServiceCollection Configure<TOptions>(this IServiceCollection services, IConfiguration config) where TOptions : class
//public static IServiceCollection Configure<TOptions>(this IServiceCollection services, IConfiguration config, Action<BinderOptions> configureBinder) where TOptions : class
//public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, IConfiguration config) where TOptions : class
//public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, IConfiguration config, Action<BinderOptions> configureBinder)
//
//���� ����� ���������� ��� ����� ���������� ��� ���� IServiceCollection. � ��� ������ ������ ������������ �����, ������ �������� ���� ���������� �����
//�������� ��������� ������������. � ����� ��� ������ ������ ��������� � �������� ������ �� ���������� ������ ������������, �� ������ ������� ����� ����������� ������ TOptions.

// ��������, � ��� � ������� ��������� ���� ������������ person.json
// ������ ���� �� ���� ��������� ������ ������������. ������� name �������������� � ������ �����������, age - � ���������, languages ������������ �����, �������� �������
// ������������, � ������� company - ��������, � ������� ������������ ��������. � �� ����� ��� ������ ������������ ��� ��������� � ���������� ��� ��������� ������.
// ��� ����� ������� ������� � ������ ����� Person:
public class Person
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

////��������� �������� �����������
//// ��� ������������� �� ����� �������������� ��������� � ������� ���������� ������ services.Configure():
//using Microsoft.Extensions.Options; 
//var builder = WebApplication.CreateBuilder();
//builder.Configuration.AddJsonFile("person.json");
//builder.Services.Configure<Person>(builder.Configuration);
//builder.Services.Configure<Person>(opt =>
//{
//    opt.Age = 22;
//});

//var app = builder.Build();

//app.Map("/", (IOptions<Person> options) =>
//{
//    Person person = options.Value;  // �������� ���������� ����� Options ������ Person
//    return person;
//});
//app.Run();
//// ����� ����� ���������� ��������� ������ ������������. ��������, ��������� ������ Company:
//using Microsoft.Extensions.Options; 
//var builder = WebApplication.CreateBuilder();
//builder.Configuration.AddJsonFile("person.json");
//builder.Services.Configure<Person>(builder.Configuration);
//builder.Services.Configure<Company>(builder.Configuration.GetSection("company"));

//var app = builder.Build();

//app.Map("/", (IOptions<Company> options) => options.Value);

//app.Run();