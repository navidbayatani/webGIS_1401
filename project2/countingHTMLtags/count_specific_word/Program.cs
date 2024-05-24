// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text.RegularExpressions;

Console.WriteLine("by navid");
string word = "img";
string sentence = File.ReadAllText("D:/c_sharp/htmlcount.html");
//U can add your own path and try :)
int count = 0;
foreach (Match match in Regex.Matches(sentence, word, RegexOptions.IgnoreCase))
{
    count++;
}
Console.WriteLine("{0}" + " found " + "{1}" + " Times", word, count);
