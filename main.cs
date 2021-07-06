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

}
}