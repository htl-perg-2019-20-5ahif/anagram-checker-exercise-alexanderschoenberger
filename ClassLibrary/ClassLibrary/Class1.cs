using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Class1 : IClass1
    {
        static Dictionary<string, List<string>> anagrams;
        static List<string> list;

        public void ReadFile(string path)
        {
            var lines = File.ReadAllLines(path);
            anagrams = new Dictionary<string, List<string>>();
            foreach (string word in lines)
            {
                var line = word.Split('=');

                try
                {
                    var list = new List<string>();
                    list.Add(line[1]);
                    anagrams.Add(line[0], list);
                }
                catch (Exception e)
                {
                    List<string> list;
                    anagrams.TryGetValue(line[0], out list);
                    list.Add(line[1]);
                }
            }
        }

        public static bool CheckToWords(string firstWord, string secondWord)
        {
            List<string> foundWord;
            if (anagrams.ContainsKey(firstWord))
            {
                anagrams.TryGetValue(firstWord, out foundWord);
                return foundWord.Contains(secondWord);
            }
            if (anagrams.ContainsKey(secondWord))
            {
                anagrams.TryGetValue(secondWord, out foundWord);
                return foundWord.Contains(firstWord);
            }
            var keys = anagrams.Where(p => p.Value.Contains(firstWord) && p.Value.Contains(secondWord)).Select(p => p.Key);
            if (keys != null)
            {
                return true;
            }
            return false;
        }

        public static IEnumerable<string> findAnagrams(string word)
        {
            List<string> foundWord = new List<string>();

            if (anagrams.ContainsKey(word))
            {
                anagrams.TryGetValue(word, out foundWord);
            }
            else
            {
                var key = anagrams.Where(p => p.Value.Contains(word)).Select(p => p.Key).First();
                anagrams.TryGetValue(key, out foundWord);
                return foundWord;
            }

            return foundWord;
        }

        public static IEnumerable<string> getPermutations(string word)
        {
            list = new List<string>();
            RecPermutation("", word);
            return list;
        }

        private static void RecPermutation(string soFar, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                list.Add(soFar);
                return;
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string remaining = input.Substring(0, i) + input.Substring(i + 1);
                    RecPermutation(soFar + input[i], remaining);
                }
            }
        }
    }
}
