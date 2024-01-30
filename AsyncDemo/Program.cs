// See https://aka.ms/new-console-template for more information
//Console.WriteLine("");

using (HttpClient client = new HttpClient())
{
    var htmlStr = await client.GetStringAsync("http://www.baidu.com");
    string fileName = "d:/111.txt";
    File.Delete(fileName);
    await File.WriteAllTextAsync(fileName, htmlStr.ToString());
    var data = await File.ReadAllTextAsync(fileName);
    Console.WriteLine(data);
}

 