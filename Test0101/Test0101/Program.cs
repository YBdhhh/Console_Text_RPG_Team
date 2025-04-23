using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (TryParseCompleted(input, out string result))
            {
                Console.WriteLine("성공");
                break;
            }
            else
            {
                Console.WriteLine("실패");            
            }
        }
        static bool TryParseCompleted(string? s, out string result)
        {
            const string prefix = "열려라참깨";
            if (!string.IsNullOrWhiteSpace(s) && s.StartsWith(prefix))
            {
                result = s;     // 조건 만족 시 원본을 result에 할당

                return true;
            }

            result = string.Empty;
            return false;
        }
    }

   
}



