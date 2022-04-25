using Contracts;
using Contracts.DataAccess;
using Contracts.Interfaces;

namespace BusinessLogic;

public class AnagramController
{
    private IWordRepository _wordRepository = new InMemData();
    private HashSet<string> _fetchedWords;
    private HashSet<Anagram> _fetchedAnagramsSet;
    private HashSet<string> _anagramsSet;

    public AnagramController()
    {
        _fetchedWords = _wordRepository.GetWords();
        _fetchedAnagramsSet = new HashSet<Anagram>();
        _anagramsSet = new HashSet<string>();
    }

    private void ConvertToAnagrams(HashSet<string> stringWords)
    {
        foreach (var word in stringWords)
        {
            _fetchedAnagramsSet.Add(new Anagram(word));
        }
    }

    public HashSet<string> FindAnagrams(string inputWord)
    {
        // Convert inputWord to anagram
        Anagram inputAnagram = new Anagram(inputWord);
        
        // Filter the set to same length words
        _fetchedWords.RemoveWhere(x => x.Length != inputWord.Length);
        
        // Convert the set to Anagram models
        ConvertToAnagrams(_fetchedWords);

        // Check for anagrams
        foreach (var word in _fetchedAnagramsSet)
        {
            // Compares anagram cribs and ignores the word that is the same as input word
            if (word.Equals(inputAnagram) && !word.Name.Equals(inputAnagram.Name))
            {
                _anagramsSet.Add(word.Name);
            }
        }

        return _anagramsSet;
    }
}