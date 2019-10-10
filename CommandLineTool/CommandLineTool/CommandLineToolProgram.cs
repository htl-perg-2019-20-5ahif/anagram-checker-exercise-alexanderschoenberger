using AnagramLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace CommandLineTool
{
    class CommandLineToolProgram
    {
        static int Main(string[] args)
        {
            if (args.Length > 0)
            {
                IAnagramLibrary reader = new AnagramLibrary.AnagramLibrary();
                reader.ReadFile(ConfigurationManager.AppSettings.Get("dir"));
                if (args[0].Equals("check"))
                {
                    return check(args);
                }
                if (args[0].Equals("getKnown"))
                {
                    return getKnown(args[1]);
                }
                if (args[0].Equals("getPermutations"))
                {
                    return getPermutations(args[1]);
                }
            }
            return 1;
        }

        private static int getPermutations(string word)
        {
            var found = AnagramLibrary.AnagramLibrary.getPermutations(word);
            printList(found);
            return 0;
        }

        private static int getKnown(string word)
        {
            var found = (List<string>)AnagramLibrary.AnagramLibrary.findAnagrams(word);
            if (found == null)
            {
                Console.WriteLine("No known anagrams found ");
                return 1;
            }
            printList(found);
            return 0;
        }

        private static int check(string[] args)
        {
            Console.Write("\"" + args[1] + "\" and \"" + args[2] + "\" are ");
            if (!AnagramLibrary.AnagramLibrary.CheckToWords(args[1], args[2]))
            {
                Console.Write("no ");
            }
            Console.WriteLine("anagrams");
            return 0;
        }

        private static void printList(IEnumerable<string> list)
        {
            foreach (string word in list)
            {
                Console.WriteLine(word);
            }
        }
    }
}
