var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.UseDeveloperExceptionPage(); // �� �����������

// ������ ��� ���������
app.Environment.EnvironmentName = "Production"; 

app.Run(async (context) =>
{
    int a = 5;
    int b = 0;
    int c = a / b;
    await context.Response.WriteAsync($"c = {c}");
});


app.Run();




//������ � ���������� ����� ������� ��������� �� ��� ����:
// ����������, ������� ��������� � �������� ���������� ���� (��������, ������� �� 0), � ����������� ������ ��������� HTTP (��������, ������ 404).

// ������� ���������� ����� ���� ������� ��� ������������ � �������� �������� ����������, �� ������� ������������ �� ������ ����� �� ������.
// ��� ��������� ���������� � ����������, ������� ��������� � �������� ����������, ������������ ����������� middleware - DeveloperExceptionPageMiddleware.
// ������ ������� ��� ��� �� ���� ���������, ��������� ��� ����������� �������������:
// if (context.HostingEnvironment.IsDevelopment())
// {
//    app.UseDeveloperExceptionPage();
// }
// ���� ���������� ��������� � ��������� ����������, �� � ������� middleware app.UseDeveloperExceptionPage()
// ���������� ������������� ���������� � ������� ���������� � ��� ������������.

// ��������� app.Environment.EnvironmentName = "Production" ������ ��� ��������� � "Development" (������� ����������� �� ���������) �� "Production".
// � ���� ������ ����� ASP.NET Core ������ �� ����� ����������� ���������� ��� ����������� � ������ ����������.
// �������������� middleware �� ������ app.UseDeveloperExceptionPage() �� ����� ������������� ����������. � ���������� ����� ���������� ������������ ������ 500,
// ������� ���������, ��� �� ������� �������� ������ ��� ��������� �������. ������ ������� ������� �������� ������ ����� ��������� � ��� ������.

//UseExceptionHandler
// ��� �� ����� ������ ��������, � ������� ���-���� ��������� ������������� ���� ������������� ��������� ���������� � ���, ��� �� ���-���� ���������.
// ���� ����������� ���-�� ���������� ������ ��������. ��� ���� ����� ����� ������������ ��� ���� ���������� middleware - ExceptionHandlerMiddleware,
// ������� ������������ � ������� ������ UseExceptionHandler(). �� �������������� ��� ������������� ���������� �� ��������� ����� � ��������� ���������� ����������.
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Environment.EnvironmentName = "Production"; // ������ ��� ���������
//// ���� ���������� �� ��������� � �������� ����������
//// �������������� �� ������ "/error"
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error"); // �������������� ��� ������������� ���������� �� ��������� ����� � ��������� ���������� ����������
//}
//// middleware, ������� ������������ ����������
//app.Map("/error", app => app.Run(async context =>
//{
//    context.Response.StatusCode = 500;
//    await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
//}));

//  ������ ������ ����� ��������� ���������� - �� ����� �������� ���������� �� ������ "/error" � �������� ��� �� ����� �� �������.
//  �� � ���� ������ �� ����� ������������ ������ ������ ������ UseExceptionHandler(), ������� ��������� �������, �������� �������� ������������ ������ IApplicationBuilder:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.Environment.EnvironmentName = "Production";
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler(app => app.Run(async context =>
//    {
//        context.Response.StatusCode = 500;
//        await context.Response.WriteAsync("Error 500. DivideByZeroException occurred!");
//    }));
//}
//app.Run(async (context) =>
//{
//    int a = 5;
//    int b = 0;
//    int c = a / b;
//    await context.Response.WriteAsync($"c = {c}");
//});
//app.Run();
// ������� ���������, ��� app.UseExceptionHandler() ������� �������� ����� � ������ ��������� middleware.








