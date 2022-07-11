var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/users/{id}/{name}", (string id, string name) => $"User Id: {id}   User Name: {name}");//������ �������-����������� "/users/{id}-{name}"
app.Map("/users/{id}-{name}", HandleRequest);
app.Map("/users/{id}", (string id) => $"User Id: {id}");
app.Map("/users", () => "Users Page");
app.Map("/", () => "Index Page");

app.Run();




//������ ��������, ������� �������������� � �������� ������, ����� ����� ���������.
// ��������� ����� ��� � ������������ � ������� �������� ������ �������� ������: {��������_���������}

//����������� ���������� ����������
// �������� ������� ����� ���������� � ������� ���������� ���������� ��������

//��������� ����������� �������� � ��������� �����
string HandleRequest(string id, string name)
{
    return $"User Id: {id}   User Name: {name}";
}

//�������������� ��������� ������� �������� � ����� ������� ��������
//app.Map("/users/{id?}", (string? id) => $"User Id: {id??"Undefined"}");

//�������� ���������� �� ���������
// ���������� �������� ����� ����� ��������� �������� �� ��������� �� ������, ���� �� �� �������� ��������:
//app.Map(
//    "{controller=Home}/{action=Index}/{id?}",
//    (string controller, string action, string? id) =>
//        $"Controller: {controller} \nAction: {action} \nId: {id}"
//);

//�������� ������������� ���������� ���������� � �������
// �� ����� ���������� ����� ���������� ��������� � �������, ����� �� ���� ������ ����������� � ����� ��������� � �������
// ��������� �� ������ * ("���������") ��� * *(��� ���������) (��� ��� ���������� catchall-��������):
// app.Map("users/{**info}", (string info) => $"User Info: {info}");