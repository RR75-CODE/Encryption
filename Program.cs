using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: Program trainingFile scrambledFile outputFile");
            return;
        }

        string trainingFile = args[0];
        string scrambledFile = args[1];
        string outputFile = args[2];

        Console.WriteLine("1. Reading input file \"" + trainingFile + "\".");
        string trainingText = File.ReadAllText(trainingFile).ToUpper();

        int lineCount = File.ReadAllLines(trainingFile).Length;
        int charCount = trainingText.Length;
        Console.WriteLine("2. Length of input file is " + lineCount + " lines, and " + charCount + " characters.");

        int[] counts = new int[26];
        foreach (char c in trainingText)
        {
            if (c >= 'A' && c <= 'Z')
            {
                counts[c - 'A']++;
            }
        }

        int firstCount = 0, secondCount = 0;
        char firstChar = 'A', secondChar = 'A';

        for (int i = 0; i < 26; i++)
        {
            if (counts[i] > firstCount)
            {
                secondCount = firstCount;
                secondChar = firstChar;

                firstCount = counts[i];
                firstChar = (char)(i + 'A');
            }
            else if (counts[i] > secondCount)
            {
                secondCount = counts[i];
                secondChar = (char)(i + 'A');
            }
        }

        Console.WriteLine("3. The two most occurring characters are " +
            firstChar + " and " + secondChar +
            ", occurring " + firstCount + " times and " + secondCount + " times.");

        Console.WriteLine("4. Reading the encrypted file \"" + scrambledFile + "\".");
        string scrambledText = File.ReadAllText(scrambledFile).ToUpper();

        int[] scrambledCounts = new int[26];
        foreach (char c in scrambledText)
        {
            if (c >= 'A' && c <= 'Z')
            {
                scrambledCounts[c - 'A']++;
            }
        }

        int scrambledMax = 0;
        char scrambledChar = 'A';
        for (int i = 0; i < 26; i++)
        {
            if (scrambledCounts[i] > scrambledMax)
            {
                scrambledMax = scrambledCounts[i];
                scrambledChar = (char)(i + 'A');
            }
        }

        Console.WriteLine("5. The most occurring character is " +
            scrambledChar + ", occurring " + scrambledMax + " times.");

        int shift = scrambledChar - firstChar;
        Console.WriteLine("6. A shift factor of " + shift + " has been determined.");

        string result = "";
        foreach (char c in scrambledText)
        {
            if (c >= 'A' && c <= 'Z')
            {
                char newChar = (char)(c - shift);
                if (newChar < 'A') newChar = (char)(newChar + 26);
                if (newChar > 'Z') newChar = (char)(newChar - 26);
                result += newChar;
            }
            else
            {
                result += c;
            }
        }

        File.WriteAllText(outputFile, result);
        Console.WriteLine("7. Writing output file now to \"" + outputFile + "\".");

        Console.WriteLine("8. Display the file? (y/n).");
        string answer = Console.ReadLine().Trim().ToLower();

        if (answer == "y")
        {
            Console.WriteLine("Decrypted File Content");
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("Program ended without displaying file.");
        }
    }
}
