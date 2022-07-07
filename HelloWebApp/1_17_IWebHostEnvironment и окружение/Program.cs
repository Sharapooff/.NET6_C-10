var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//�������� �����, ������������� �������� ���� launchSettings.json. ��� ����� ������� ����� ����������:
//app.Environment.EnvironmentName = "Production";

if (app.Environment.IsDevelopment())
{
    app.Run(async (context) => await context.Response.WriteAsync("In Development Stage"));
}
else
{
    app.Run(async (context) => await context.Response.WriteAsync("In Production Stage"));
}
Console.WriteLine($"{app.Environment.EnvironmentName}");

app.Run();




//��� �������������� � ����������, � ������� �������� ����������, ��������� ASP.NET Core ������������� ��������� IWebHostEnvironment.
// ���� ��������� ���������� ��� �������, � ������� ������� �� ����� �������� ���������� �� ���������:
// ApplicationName: ������ ��� ����������
// EnvironmentName: ������ �������� �����, � ������� ����������� ����������
// ContentRootPath: ������ ���� � �������� ����� ����������
// WebRootPath: ������ ���� � �����, � ������� �������� ����������� ������� ����������. �� ��������� ��� ����� wwwroot
// ContentRootFileProvider: ���������� ���������� ���������� Microsoft.AspNetCore.FileProviders.IFileProvider, ������� ����� �������������� ��� ������ ������ �� ����� ContentRootPath
// WebRootFileProvider: ���������� ���������� ���������� Microsoft.AspNetCore.FileProviders.IFileProvider, ������� ����� �������������� ��� ������ ������ �� ����� WebRootPath

// �� �������� ����� ��� ���������� �������� ������������ �� ��������� EnvironmentName. �� ��������� ������� ��� �������� �������� ��� ����� ��������:
// Development, Staging � Production. � ������� ��� �������� �������� ����� ��������� ���������� ����� ASPNETCORE_ENVIRONMENT.
// ������� �������� ������� ��������� �������� � ����� launchSettings.json, ������� ������������� � ������� � ����� Properties.
// ��� ����������� �������� ���� ���������� ��� ���������� IWebHostEnvironment ���������� ����������� ������ ����������:
// IsEnvironment(string envName): ���������� true, ���� ��� ����� ����� �������� ��������� envName
// IsDevelopment(): ���������� true, ���� ��� ����� - Development
// IsStaging(): ���������� true, ���� ��� ����� - Staging
// IsProduction(): ���������� true, ���� ��� ����� - Production

// ������ ���������������� ��������� ��� ��������� ������������ ��� � ����������� �� ����, �� ����� ������ ��������� ����������.
// ���� ���������� � �������� ����������, �� �� ����� ��������� ���� ���, � ��� ������������ ��� ������������ ������������� ������ ���.

//����������� ����� ��������� �����
// ���� �� ��������� ����� ����� ��������� ��� ���������: Development, Staging, Production, �� �� ����� ��� ������� ������� ����� ��������. ��������, ��� ���� �����������
// �����-�� �������������� ���������. ��� ����� ������� ����� ��������� ����� launchSettings.json ���� ����������.
// app.Environment.EnvironmentName = "Test";   // �������� �������� ����� �� Test
// ��� �������� �������� "ASPNETCORE_ENVIRONMENT" �� "Test" ��� ����� ������ � ����� launchSettings.json