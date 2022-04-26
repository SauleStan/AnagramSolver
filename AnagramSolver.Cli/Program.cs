
using AnagramSolver.BusinessLogic;

AnagramController anagramController = new AnagramController();

Console.WriteLine("Your input: ");
var inputWord = Console.ReadLine();
//string inputWord = "alus";

if (inputWord != null)
{
    var words = anagramController.FindAnagrams(inputWord);
    Console.WriteLine("Anagrams: ");
    foreach (var word in words)
    {
        Console.WriteLine(word);
    }
}