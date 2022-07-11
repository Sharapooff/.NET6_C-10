var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/", () => "Index Page");
app.Map("/user", () => new Person("Tom", 37));
app.Map("/about", () => "About Page");
app.Map("/contact", () => "Contacts Page");
app.Map("/about", async (context) =>
{
    await context.Response.WriteAsync("About Page");
});
//ASP.NET Core ��������� ����� �������� ��� ��������� � ���������� �������� �����. 
app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));

app.Run();

record class Person(string Name, int Age);


//������� ������������� �������� �� ������������� �������� �������� � ���������� � �� ��������� ����������� ������������� �������� ��� ��������� �������
//������������ �������� ����� ����������. �������� ����� ��� endpoint ������������ ��������� ���, ������� ������������ ������. �� ���� �������� �����
//���������� ������ ��������, �������� ������ ��������������� ������, � ���������� ������� �� ����� ��������.

//��� ������������� ������� ������������� � �������� ��������� ������� ����������� ��� ���������� ���������� middleware
// UseEndpoints()
// UseRouting()

//����� Map
// ����� ������� �������� ����������� �������� ����� � ���������� �������� ����� Map, ������� ���������� ��� ����� ���������� ��� ���� IEndpointRouteBuilder.
// �� ��������� �������� ����� ��� ��������� �������� ���� GET.
// !!! �� ����� ������ ���� ����� � ����������� ������� Map(), ������� ������ � ������ ����� Map � ������� ���������� ��� ����� ���������� ��� ���� IApplicationBuilder



