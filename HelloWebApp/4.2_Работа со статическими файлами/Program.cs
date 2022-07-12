var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();  // ��������� ������� html �� ���������
app.UseStaticFiles();

app.Run(async (context) => await context.Response.WriteAsync("Hello World"));

app.Run();




//� ������� ������������ ������ ���������� UseDefaultFiles() ����� ��������� �������� ����������� ���-������� �� ��������� ��� ��������� � ��� �� ������� ����.
// � ���� ������ ��� �������� ������� � ����� ���-���������� ���� http://localhost:xxxx/ ���������� ����� ������ � ����� wwwroot ��������� �����:
// default.htm
// default.html
// index.htm
// index.html

//���� �� �� ����� ������������ ����, �������� �������� ���������� �� �����������������, �� ��� ���� � ���� ������ ��������� ������ DefaultFilesOptions:
//DefaultFilesOptions options = new DefaultFilesOptions();
//options.DefaultFileNames.Clear(); // ������� ����� ������ �� ���������
//options.DefaultFileNames.Add("hello.html"); // ��������� ����� ��� �����
//app.UseDefaultFiles(options); // ��������� ����������
//app.UseStaticFiles();

//����� UseDirectoryBrowser
// ����� UseDirectoryBrowser ��������� ������������� ������������� ���������� ��������� �� �����:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseDirectoryBrowser();
//app.UseStaticFiles();
//app.Run();

//������ ����� ����� ����������, ������� ��������� ����������� ������������ ������� �� ������� ����� ��� � ������� � ��������� ������� ������� � ��� ����� ����� 
//���������� ���������� ����� ��������:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseDirectoryBrowser(new DirectoryBrowserOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages")
//});
//app.UseStaticFiles();
//app.Run();

//������������� ��������� � ������
// ���������� ������ UseStaticFiles() ��������� ����������� ���� � ������������� ����������:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions() // ������������ ������� � �������� wwwroot/html
//{
//    FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages")
//});
//app.Run();

//����� UseFileServer
// ����� UseFileServer() ���������� ���������������� ����� ���� ���� ������������� ������� UseStaticFiles, UseDefaultFiles � UseDirectoryBrowser:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer();
//app.Run();

//�� ��������� ���� ����� ��������� ������������ ����������� ����� � ���������� ����� �� ��������� ���� index.html. ���� ��� ���� ��� �������� �������� ���������,
//�� �� ����� ������������ ���������� ������� ������:
//app.UseFileServer(enableDirectoryBrowsing: true);

//��� ���� ���������� ������ ��������� ����� ����� ������ ���������:
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer(new FileServerOptions
//{
//    EnableDirectoryBrowsing = true,
//    EnableDefaultFiles = false
//});
//app.Run();

//����� ����� ��������� ������������� ����� ������� � ����������:
//using Microsoft.Extensions.FileProviders; 
//var builder = WebApplication.CreateBuilder();
//var app = builder.Build();
//app.UseFileServer(new FileServerOptions
//{
//    EnableDirectoryBrowsing = true,
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
//    RequestPath = new PathString("/pages"),
//    EnableDefaultFiles = false
//});
//app.Run();
//� ���� ������ ����� �������� ����� �������� �� ���� http://localhost:xxxx/pages/, �� ��� ���� ���� http://localhost:xxxx/html/ �������� �� �����.