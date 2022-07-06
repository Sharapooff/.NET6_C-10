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
        // путь к папке, где будут храниться файлы
        var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
        // создаем папку для хранения файлов
        Directory.CreateDirectory(uploadPath);

        foreach (var file in files)
        {
            // путь к папке uploads
            string fullPath = $"{uploadPath}/{file.FileName}";

            // сохраняем файл в папку uploads
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response.WriteAsync("Файлы успешно загружены");
    }
    else
    {
        await response.SendFileAsync("html/index.html");
    }
});

app.Run();


//Все загружаемые файлы в ASP.NET Core представлены типом IFormFile из пространства имен Microsoft.AspNetCore.Http.
// Соответственно для получения отправленного файла в контроллере необходимо использовать IFormFile.
// Затем с помощью методов IFormFile мы можем произвести различные манипуляции файлом - получит его свойства, сохранить, получить его поток и т.д.
// Некоторые его свойства и методы:
// ContentType: тип файла
// FileName: название файла
// Length: размер файла
// CopyTo/CopyToAsync: копирует файл в поток
// OpenReadStream: открывает поток файла для чтения
