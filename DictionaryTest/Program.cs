using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DictionaryTest
{
    
    internal class Program
    {
        
        public static Dictionary<string, string> WriteAllWordsAndDef()
        {
            Console.WriteLine("Input the numbers of words:");
            int numbers = int.Parse(Console.ReadLine());

            Dictionary<string, string> WordDictionary = new Dictionary<string, string>();

            for (int i = 0; i < numbers; i++)
            {
                int counter =i+1;
                DWord dword = new DWord();
                Console.WriteLine($"Input your word:{counter}");
                Console.Write($"{counter}:");
                dword.Word = Console.ReadLine();

                Console.WriteLine($"Input it's definition:{dword.Word}");
                Console.Write($"{counter}:");
                dword.Definition = Console.ReadLine();

                WordDictionary.Add(dword.Word, dword.Definition);

            }
            return WordDictionary;
        }
        public static List<string> MixWords(Dictionary<string, string> WordDictionary) 
        {
            if(WordDictionary == null || WordDictionary.Count == 0)
            {
                Console.WriteLine("Dict is empty!");
                return null;
            }
            List<string> keys = new List<string>(WordDictionary.Keys);

            Random rand = new Random();
            var mixedWord = keys.OrderBy(x => rand.Next()).ToList();

            return mixedWord;
        }
        public static void ShowAllWordsFromDictionary(Dictionary<string, string> WordDictionary)
        {
            if (WordDictionary.Count == 0)
            {
                Console.WriteLine("Your dictionary is empty!");
                return;
            }

            Console.WriteLine("Your Dictionary:");

            // Определим ширину колонок для выравнивания
            int keyColumnWidth = Math.Max(WordDictionary.Keys.Max(k => k.Length), "Word".Length) + 2;
            int valueColumnWidth = Math.Max(WordDictionary.Values.Max(v => v.Length), "Definition".Length) + 2;

            // Заголовок таблицы
            Console.WriteLine($"{"№",-3} {"Word".PadRight(keyColumnWidth)} {"Definition".PadRight(valueColumnWidth)}");

            // Разделительная линия
            Console.WriteLine(new string('-', 3 + keyColumnWidth + valueColumnWidth + 2));

            int counter = 1;
            foreach (var word in WordDictionary)
            {
                Console.WriteLine($"{counter,-3} {word.Key.PadRight(keyColumnWidth)} {word.Value.PadRight(valueColumnWidth)}");
                counter++;
            }
        }


        public static List<string> GuessTheWordByDef(Dictionary<string, string> WordDictionary)
        {
            Console.WriteLine("Guess the word by definition:");
            bool choice = true; 
            List<string> values = new List<string>(WordDictionary.Values);

            Random rand = new Random();
            var mixedWord = values.OrderBy(x => rand.Next()).ToList();


            
                
                for(int i = 0; i<= mixedWord.Count; i++)
                {
                    Console.WriteLine(mixedWord[i]);
                    string input = Console.ReadLine();
                    

                if (input == "quit")
                    {
                        choice = false;
                    }
                }

                
            
            return mixedWord;


        }

        public static void SaveDictInTxt(Dictionary<string, string> WordDictionary)
        {
            string folderPath = @"C:\Users\User\source\repos\DictionaryTest\DictionaryTest\Dictionary";

            Console.WriteLine("Name of file:");
            string fileName = Console.ReadLine();

            // Полный путь к файлу, включая имя файла
            string filePath = Path.Combine(folderPath, fileName + ".txt");

            // Проверяем, существует ли директория, если нет, создаем её
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Directory created: {folderPath}");
            }

            // Создаем файл, если его не существует
            using (FileStream fs = File.Create(filePath))
            {
                // Закрываем файл, чтобы можно было его использовать в StreamWriter
                fs.Close();
            }

            // Записываем данные в файл
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var kvp in WordDictionary)
                {
                    writer.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }

            Console.WriteLine($"Dictionary saved to {filePath}");
        }



    static void Main(string[] args)
        {
            Dictionary<string, string> MainDict = new Dictionary<string, string>();
            while (true)
            {
                Console.WriteLine("****************************************");
                Console.WriteLine("Menu:");
                Console.WriteLine("1.Write all of yours words in dictionary");
                Console.WriteLine("2.Show the dictionary");
                Console.WriteLine("3.Mix words");
                Console.WriteLine("4. Save dictionary to file");
                //Console.WriteLine("5.Guess");
                Console.WriteLine("****************************************");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            MainDict = WriteAllWordsAndDef();
                            break;
                        case 2:
                            Console.Clear();
                            ShowAllWordsFromDictionary(MainDict);
                            break;
                        case 3:
                            Console.Clear();
                            MixWords(MainDict);
                            break;
                        case 4:
                            Console.Clear();
                            SaveDictInTxt(MainDict);
                            Console.WriteLine("Dictionary saved to file.");
                            break;
                        //case 5:
                        //    Console.Clear();
                        //    GuessTheWordByDef(MainDict);
                        //    break;
                        default:
                            Console.WriteLine("It's not correct number!");
                            break;
                    }
                }
                
            }
        }
    }
}

