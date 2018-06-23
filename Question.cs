// Francisco Sabater Villora

using System;

public class Question
{
    public string Text      { get; set; }
    public string Category  { get; set; }
    public string Answer1   { get; set; }
    public string Answer2   { get; set; }
    public string Answer3   { get; set; }
    public string Answer4   { get; set; }
    public ushort Correct   { get; set; }
    

    public Question (string data)
    {
        string[] dataSplitted = data.Split('Ç');
        Category = dataSplitted[0];
        Text = dataSplitted[1];
        Answer1 = dataSplitted[2];
        Answer2 = dataSplitted[3];
        Answer3 = dataSplitted[4];
        Answer4 = dataSplitted[5];
        Correct = Convert.ToUInt16(dataSplitted[6]);

    }
}
