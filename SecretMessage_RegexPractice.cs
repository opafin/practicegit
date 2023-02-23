using System.Text.RegularExpressions;

public class SecretMessage
{
    string decryptorPattern = @"(\d{2,3})";

    void Decryptor()
    {
        Regex regex = new Regex(decryptorPattern);
        var input = File.ReadAllLines("input2.txt");

        foreach (var line in input)
        {
            MatchCollection matchCollection = regex.Matches(line);

            foreach (var match in matchCollection)
            {
                string? matchedToString = match.ToString();
                int matchedToInt = Convert.ToInt32(matchedToString);
                Console.Write((char)matchedToInt);
            }
        }
    }
}
