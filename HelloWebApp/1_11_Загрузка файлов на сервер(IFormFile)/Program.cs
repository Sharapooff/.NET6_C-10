var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (context) =>
{
    var response = context.Response;
    var request = context.Request;

    response.ContentType = "text/html; charset=utf-8";

    if (request.Path == "/upload" && request.Method == "POST")
    {
        IFormFileCollection files = request.Form.Files;
        // ���� � �����, ��� ����� ��������� �����
        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        // ������� ����� ��� �������� ������
        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            // ���� � ����� uploads
            string fullPath = $"{uploadPath}/{file.FileName}";

            // ��������� ���� � ����� uploads
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("����� ������� ���������");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


//��� ����������� ����� � ASP.NET Core ������������ ����� IFormFile �� ������������ ���� Microsoft.AspNetCore.Http.
// �������������� ��� ��������� ������������� ����� � ����������� ���������� ������������ IFormFile.
// ����� � ������� ������� IFormFile �� ����� ���������� ��������� ����������� ������ - ������� ��� ��������, ���������, �������� ��� ����� � �.�.
// ��������� ��� �������� � ������:
// ContentType: ��� �����
// FileName: �������� �����
// Length: ������ �����
// CopyTo/CopyToAsync: �������� ���� � �����
// OpenReadStream: ��������� ����� ����� ��� ������
