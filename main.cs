using System;
using System.IO;
using System.Collections.Generic;

class theWord{
  public string capital;
  public string capitalAsInDoc;

  public string country;
  public string lineOfDoc;
  
  public theWord()
  {
    Random rnd = new Random();
    StreamReader file = new StreamReader("countries.txt");
     string line;
    int lineNum = rnd.Next(183);
    for(int i = 0;( line = file.ReadLine()) != null; i++)
     {
     if(lineNum == i)
     {
       lineOfDoc = line;
       break;
     }
   }

   //Defining the capital and country based on line of document
  char [] charArray = lineOfDoc.ToCharArray();
  int beginningOfTheCapital = 0;
   for(int i = 0; i < lineOfDoc.Length; i++)
   {
     if(charArray[i+1] != '|')
     {
       country += charArray[i];
     }else{
       beginningOfTheCapital = i + 3;
       break;
     }
     
   }
    for(int i = beginningOfTheCapital; i < lineOfDoc.Length; i++)
    {
      capital += charArray[i];
    }
  capitalAsInDoc = capital;
  capital = capital.ToUpper();
    file.Close();
  }
}


class MainClass {  
 
 
  public static void Main () {
    Console.WriteLine("Welcome to the Hangman game! \n");
   theWord word = new theWord();
  bool winningCondition = false;
  short hearts = 5;
  Console.WriteLine(word.capital);
  for(int i = 0; i < word.capital.Length; i++)
  {
    if((word.capital.ToCharArray())[i] == ' ')
    {
      Console.Write("   ");

    }else{
      Console.Write("_ ");
    }
  }
  List<char> wrongUsedSigns = new List<char>();
  List<char> wellUsedSigns = new List<char>();

  while(!winningCondition && hearts > 0)
  {
    Console.WriteLine("Do you want to guess the letter or the whole word? Press 'l' if a letter, and 'w' if the word");
    char choice = Convert.ToChar(Console.ReadLine());
    Console.WriteLine("\n");
  if(choice == 'w')
  {

  }else{
    char letter = Convert.ToChar(Console.ReadLine().ToUpper());
    bool notUsed = true;
    for(int i = 0; i < word.capital.Length; i++)
    {
      
      if((word.capital.ToCharArray())[i] == letter)
      {
        wellUsedSigns.Add(letter);
        notUsed = false;
        break;
      }
    }
    bool wrongUsed = false;
    if(notUsed)
    {
      for(int i = 0; i < wrongUsedSigns.Count; i++)
      {
        if(wrongUsedSigns[i] == letter){
          wrongUsed = true;
          break;
        }
      }
    if(!wrongUsed)
    {
      wrongUsedSigns.Add(letter);
    }
    }
    for(int i = 0; i < word.capital.Length; i++)
  {
    if((word.capital.ToCharArray())[i] == ' ')
    {
      Console.Write("   ");

    }else{
      bool notGuessed = true;
        for(int p = 0; p < wellUsedSigns.Count; p++)
        {
          if(wellUsedSigns[p] == (word.capital.ToCharArray())[i]){
          Console.Write(wellUsedSigns[p]);
          notGuessed = false;
          break;
          }
       
        }
       if(notGuessed)
              Console.Write("_ ");

    }
    }
  }


  }
  if(winningCondition){
    Console.WriteLine("You have won!");
  }else{
    Console.WriteLine("You have lost!");
  }
}}