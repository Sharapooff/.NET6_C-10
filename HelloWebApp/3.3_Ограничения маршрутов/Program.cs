var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.Map("/users/{id}", (int id) => $"User Id: {id}"); //���� �������� ������ ����� � ������ - ������� ������ ��� ��������������
app.Map("/users/{id:int}", (int id) => $"User Id: {id}"); //����������� int ���������, ��� �������� ������ ������������ ��� int
app.Map("/", () => "Index Page");
app.Map("/users/{name:alpha:minlength(2)}/{age:int:range(1, 110)}", (string name, int age) => $"User Age: {age} \nUser Name:{name}");
app.Map("/phonebook/{phone:regex(^7-\\d{{3}}-\\d{{3}}-\\d{{4}}$)}/", (string phone) => $"Phone: {phone}");

app.Run();




// ����������� ��������� ���������� ��� �������� ���� �������, ������������� ��� � �������� �������� � ��������� �� ���� �������� ��� ���������� ��������. 
// ��� ��������� ����������� ����� �������� ��������� ����� ��������� ����������� �������� �����������.
// � ���� �� �� ��������� �����������, �� ������� ������ ��������������, � � ��� ������ ����������� ���������� ������ int ������, �� ������� ������ 404, �� ���� ������ �� ������.
// ASP.NET Core ������������� ��������� ��� ����������� ��������:
// int, bool, datetime, decimal, double, float, guid, long, (min)(max)length(3), length(min, max), min(value), max(value), range(min, max), regex(expression), required(������ �����)

// ������ �������� ���������� ������������ ������������ �����. ��� ������ ����������� ��������� � ������������ ���� Microsoft.AspNetCore.Routing.Constraints
