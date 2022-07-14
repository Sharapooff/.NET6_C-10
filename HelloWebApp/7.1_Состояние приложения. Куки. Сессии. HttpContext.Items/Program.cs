var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Items["text"] = "Hello from HttpContext.Items";
    context.Items.Add("message", "Hello world");
    await next.Invoke();
});
app.Run(async (context) =>
{
    if (context.Items.ContainsKey("message"))
        await context.Response.WriteAsync($"Message: {context.Items["message"]}");
    else
        await context.Response.WriteAsync("Random Text");
});
app.Run();




//���� �� ���������� �������� �������� ������ ������������ ��������� HttpContext.Items - ������ ���� IDictionary<object, object>.
// ��� ��������� ������������� ��� ����� ������, ������� ��������������� ������� � ������� ��������. ����� ���������� ������� ��� ������ �� HttpContext.Items ���������.
// ������ ������ � ���� ��������� ����� ���� � ��������. � � ������� ������ ����� ��������� ��������� ���������. ��������, ���� � ��� ��������� ������� ���������
// ��������� ����������� middleware, � �� �����, ����� ��� ���� ����������� ���� �������� ����� ������, �� ��� ��� ����� ��������� ��� ���������. 

//HttpContext.Items ������������� ��� ������� ��� ���������� ����������:
// void Add(object key, object value): ��������� ������ value � ������ key
// void Clear(): ������� ��� �������
// bool ContainsKey(object key): ���������� true, ���� ������� �������� ������ � ������ key
// bool Remove(object key): ������� ������ � ������ key, � ������ �������� �������� ���������� true
// bool TryGetValue(object key, out object value): ���������� true, ���� �������� ������� � ������ key ������� �������� � ������ value

