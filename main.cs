using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class theWord {
  public string capital;
  public string capitalAsInDoc;

  public string country;
  public string lineOfDoc;

  public theWord() {
    Random rnd = new Random();
    StreamReader file = new StreamReader("countries.txt");
    string line;
    int lineNum = rnd.Next(183);
    for (int i = 0;
      (line = file.ReadLine()) != null; i++) {
      if (lineNum == i) {
        lineOfDoc = line;
        break;
      }
    }

    //Defining the capital and country based on line of document
    char[] charArray = lineOfDoc.ToCharArray();
    int beginningOfTheCapital = 0;
    for (int i = 0; i < lineOfDoc.Length; i++) {
      if (charArray[i + 1] != '|') {
        country += charArray[i];
      } else {
        beginningOfTheCapital = i + 3;
        break;
      }

    }
    for (int i = beginningOfTheCapital; i < lineOfDoc.Length; i++) {
      capital += charArray[i];
    }
    capitalAsInDoc = capital;
    capital = capital.ToUpper();
    file.Close();
  }
}

class MainClass {

  public static void Main() {
    bool playAgain = true;

    while (playAgain) {

      Console.WriteLine("Welcome to the Hangman game! \n \n");
      theWord word = new theWord();
      bool winningCondition = false;
      short hearts = 5;
      for (int i = 0; i < word.capital.Length; i++) {
        if ((word.capital.ToCharArray())[i] == ' ') {
          Console.Write("   ");

        } else {
          Console.Write("_ ");
        }
      }
      Stopwatch time = new Stopwatch();
      List < char > wrongUsedSigns = new List < char > ();
      List < char > wellUsedSigns = new List < char > ();
      DateTime date = DateTime.Now;
      while (!winningCondition && hearts > 0) {

        time.Start();
        Console.WriteLine("Do you want to guess the letter or the whole word? Press 'l' if a letter, and 'w' if the word");
        string choiceString = Console.ReadLine();
        char choice = choiceString.ToCharArray()[0];
        Console.WriteLine("\n");
        if (choice == 'w') {
          string wordGuess = Console.ReadLine().ToUpper();
          if (wordGuess == word.capital) {
            winningCondition = true;
          } else {
            hearts -= 2;
            Console.WriteLine($"Sorry, it is not {wordGuess}. You are losing 2 hearts for that... now you have {hearts}");
          }
        } else if (choice == 'l') {
          Console.WriteLine("Choose a letter. Letters that were already used are: \n");
          
          for (int i = 0; i < wrongUsedSigns.Count; i++) {
            Console.Write(wrongUsedSigns[i] + ", ");
          }
          Console.WriteLine();
          string letter1 = Console.ReadLine();
          char letter = letter1.ToUpper().ToCharArray()[0];
          bool notUsed = true;
          for (int i = 0; i < word.capital.Length; i++) {

            if ((word.capital.ToCharArray())[i] == letter) {
              wellUsedSigns.Add(letter);
              notUsed = false;
              break;
            }
          }
          bool wrongUsed = false;
          if (notUsed) {
            for (int i = 0; i < wrongUsedSigns.Count; i++) {
              if (wrongUsedSigns[i] == letter) {
                wrongUsed = true;
                break;
              }
            }
            if (!wrongUsed) {
              hearts--;
              wrongUsedSigns.Add(letter);
              Console.WriteLine($"Sorry, but {letter} isnt present in the word. You have {hearts} tries now");
            }
          }
          int lettersUsed = 0;
          for (int i = 0; i < word.capital.Length; i++) {
            if ((word.capital.ToCharArray())[i] == ' ') {
              Console.Write("   ");

            } else {
              bool notGuessed = true;
              for (int p = 0; p < wellUsedSigns.Count; p++) {

                if (wellUsedSigns[p] == (word.capital.ToCharArray())[i]) {
                  lettersUsed++;
                  Console.Write(wellUsedSigns[p]);
                  notGuessed = false;
                  break;
                }

              }
              if (notGuessed)
                Console.Write("_ ");

            }

          }
          if (lettersUsed == word.capital.Length) {
            winningCondition = true;
          }
          if (hearts == 2) {
            Console.WriteLine($"\n The city is a capital of {word.country}");
          }
        }

      }
      time.Stop();
      TimeSpan timeScore = time.Elapsed;
      if (winningCondition) {
        Console.WriteLine("You have won! Guessing took you {0:00} seconds", timeScore.Seconds);
        Console.WriteLine("What's your name?");
        string name = Console.ReadLine();
        StreamWriter scoreFile = new StreamWriter("scores.txt");
        scoreFile.WriteLine($"{name} | {date} | time : {timeScore.Seconds} | hearts: {hearts} ");
        scoreFile.Flush();
        scoreFile.Close();

      } else {
        Console.WriteLine("You have lost!");
      }
      Console.WriteLine("Do you want to play again? y / n");
      char ans = Console.ReadLine().ToCharArray()[0];
      if (!(ans == 'y')) {
        playAgain = false;
      }

    }
  }
}