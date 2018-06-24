// Francisco Sabater Villora

using System;
using System.Collections.Generic;
using System.IO;

public class ListOfQuestions
{
    List<Question> questions = new List<Question>();
    private const string LOAD_FILENAME = "txt/questions.dat";
    List<int> usedQuestions;

    public ListOfQuestions()
    {
        Load();
    }

    public int Amount
    {
        get
        {
            return questions.Count;
        }  
    }

    public Question Get(int index)
    {
        if(index < 0 || index > questions.Count)
            return questions[index];
        return null;
    }

    public void Add(Question question)
    {
        if(question != null)
            questions.Add(question);
    }

    public void Load()
    {
        try
        {
            StreamReader sr = new StreamReader(LOAD_FILENAME);
            string line = sr.ReadLine();
            while (line != null)
            {
                if (line != null)
                {
                    Question q = new Question(line);
                    questions.Add(q);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            usedQuestions = new List<int>();
        }
        catch (PathTooLongException)
        {
            Console.WriteLine("Path too long");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not accessible");
        }
        catch (IOException e)
        {
            Console.WriteLine("I/O error: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }

    public Question GetFromCategory(string category)
    {
        List<Question> auxList = new List<Question>();
        foreach(Question q in questions)
        {
            if (String.Compare(q.Category, category, true) == 0)
                auxList.Add(q);
        }

        Random r = new Random();
        Console.WriteLine(auxList.Count);
        return auxList[r.Next() % auxList.Count];
    }

    public Question GetFromCategoryNR(string category)
    {
        Console.WriteLine(category);
        Console.WriteLine(questions.Count);
        Question result = null;
        List<Question> auxList = new List<Question>();
       
        for (int i = 0; i < questions.Count && result == null; ++i)
        {
            if (String.Compare(questions[i].Category, category, true) == 0 &&
                !usedQuestions.Contains(i))
            {
                usedQuestions.Add(i);
                result = questions[i];
            }
        }

        return result;
    }
}

