
using BusinessLogic;

AnagramController anagramController = new AnagramController();

string inputWord = "alus";

HashSet<string> words = anagramController.FindAnagrams(inputWord);

foreach (var word in words)
{
    Console.WriteLine(word);
}
