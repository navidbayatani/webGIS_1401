// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hellooo");
string str = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Document</title>\r\n</head>\r\n<body>\r\n    <h1>this is a test for counting</h1>\r\n    <img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQxeMHZFfVLjN9GS1Y_8DKJM9Tb7xyJwyfJTA&usqp=CAU\"  style=\"display: block;\" >\r\n    <img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQXFqBph9vw-kgRSSIGaXR04yaJTdZ9wDiyxA&usqp=CAU\" style=\"display: block;\">\r\n    <img src=\"https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHjrZk9tEzWyWG4Ze67Ir6AAE8J9m-y0r-lQ&usqp=CAU\" style=\"display: block;\">\r\n    \r\n</body>\r\n</html>";
var result = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy(r => r)
                .Select(grp => new
                {
                    Word = grp.Key,
                    Count = grp.Count()
                });
foreach (var item in result)
{
    Console.WriteLine("Word: {0}, Count:{1}", item.Word, item.Count);
}