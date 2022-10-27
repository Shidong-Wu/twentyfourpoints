// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

Console.WriteLine("Hello, this is an app for 24 points game.");
do
{
    Console.WriteLine("Please input 4 numbers.");
    string a = Console.ReadLine();
    double numa;
    while (string.IsNullOrEmpty(a) || !double.TryParse(a, out numa))
    {
        Console.WriteLine("Please input num a");
        a = Console.ReadLine();
    }

    string b = Console.ReadLine();
    double numb;
    while (string.IsNullOrEmpty(b) || !double.TryParse(b, out numb))
    {
        Console.WriteLine("Please input num b");
        b = Console.ReadLine();
    }

    string c = Console.ReadLine();
    double numc;
    while (string.IsNullOrEmpty(c) || !double.TryParse(c, out numc))
    {
        Console.WriteLine("Please input num c");
        c = Console.ReadLine();
    }

    string d = Console.ReadLine();
    double numd;
    while (string.IsNullOrEmpty(d) || !double.TryParse(d, out numd))
    {
        Console.WriteLine("Please input num d");
        d = Console.ReadLine();
    }
    List<CalculateResult> numberList = new List<CalculateResult>()
{
    new CalculateResult(){ CalFlow = a, CalResult = numa},
    new CalculateResult(){ CalFlow = b, CalResult = numb},
    new CalculateResult(){ CalFlow = c, CalResult = numc},
    new CalculateResult(){ CalFlow = d, CalResult = numd},
};

    Calculate calobj = new Calculate();

    var results = calobj.CalculateNumbs(numberList);

    if (results != null && results.Any(r => r.CalResult == 24))
    {
        Console.WriteLine("\n");
        Console.WriteLine("Total: " + results.Where(r => r.CalResult == 24).Count().ToString());
        foreach (var r24 in results.Where(r => r.CalResult == 24))
        {
            Console.WriteLine(r24.CalFlow);
            Console.WriteLine("\n");
        }
    }
    else
    {
        Console.WriteLine("These 4 numbers can not get 24.");
    }
    Console.WriteLine("Press n to continue, press other key to exit.");
} while (Console.ReadLine().ToString() == "n");

Console.WriteLine("App exit. Bye.");

public class Calculate
{
    public List<CalculateResult> CalculateNumbs(List<CalculateResult> numberList)
    {
        if (numberList == null || !numberList.Any())
        {
            return null;
        }
        int maxNumberUse = numberList.Count;
        List<CalculateResult> resultList = new List<CalculateResult>();
        Get2NumAndCalculate(numberList, resultList);
        return resultList;
    }


    public void Get2NumAndCalculate(List<CalculateResult> numberList, List<CalculateResult> resultList)
    {
        if (numberList.Count <= 1)
        {
            resultList.AddRange(numberList);
            return;
        }
        for (int currentIdx = 0; currentIdx < numberList.Count; currentIdx++)
        {
            var current = numberList[currentIdx];
            for (int targetIndex = 0; targetIndex < numberList.Count; targetIndex++)
            {
                if (targetIndex == currentIdx) //取到同一个数 跳过
                    continue;
                var target = numberList[targetIndex];
                double currentNum = current.CalResult;
                double targetNum = target.CalResult;
                var calculate4functions = Calculate2Num(currentNum, targetNum);

                foreach (var r in calculate4functions)
                {
                    CalculateResult nextNum = null;

                    List<CalculateResult> nextList;
                    switch (r.Key)
                    {
                        case "+":
                            nextNum = new CalculateResult
                            {
                                CalResult = r.Value,
                                CalFlow = String.Format("({0} + {1})", current.CalFlow, target.CalFlow)
                            };

                            nextList = new List<CalculateResult>();
                            nextList.Add(nextNum);
                            for (int nindex = 0; nindex < numberList.Count; nindex++)
                            {
                                if (nindex == currentIdx || nindex == targetIndex)
                                    continue;
                                nextList.Add(numberList[nindex]);
                            }
                            if (nextList.Count == 1)
                            {
                                resultList.AddRange(nextList);
                            }
                            else
                            {
                                Get2NumAndCalculate(nextList, resultList);
                            }
                            break;
                        case "-":
                            nextNum = new CalculateResult
                            {
                                CalResult = r.Value,
                                CalFlow = String.Format("({0} - {1})", current.CalFlow, target.CalFlow)
                            };
                            nextList = new List<CalculateResult>();
                            nextList.Add(nextNum);
                            for (int nindex = 0; nindex < numberList.Count; nindex++)
                            {
                                if (nindex == currentIdx || nindex == targetIndex)
                                    continue;
                                nextList.Add(numberList[nindex]);
                            }
                            if (nextList.Count == 1)
                            {
                                resultList.AddRange(nextList);
                            }
                            else
                            {
                                Get2NumAndCalculate(nextList, resultList);
                            }
                            break;
                        case "*":
                            nextNum = new CalculateResult
                            {
                                CalResult = r.Value,
                                CalFlow = String.Format("({0} * {1})", current.CalFlow, target.CalFlow)
                            };
                            nextList = new List<CalculateResult>();
                            nextList.Add(nextNum);
                            for (int nindex = 0; nindex < numberList.Count; nindex++)
                            {
                                if (nindex == currentIdx || nindex == targetIndex)
                                    continue;
                                nextList.Add(numberList[nindex]);
                            }
                            if (nextList.Count == 1)
                            {
                                resultList.AddRange(nextList);
                            }
                            else
                            {
                                Get2NumAndCalculate(nextList, resultList);
                            }
                            break;
                        case "/":
                            nextNum = new CalculateResult
                            {
                                CalResult = r.Value,
                                CalFlow = String.Format("({0} / {1})", current.CalFlow, target.CalFlow)
                            };
                            nextList = new List<CalculateResult>();
                            nextList.Add(nextNum);
                            for (int nindex = 0; nindex < numberList.Count; nindex++)
                            {
                                if (nindex == currentIdx || nindex == targetIndex)
                                    continue;
                                nextList.Add(numberList[nindex]);
                            }
                            if (nextList.Count == 1)
                            {
                                resultList.AddRange(nextList);
                            }
                            else
                            {
                                Get2NumAndCalculate(nextList, resultList);
                            }
                            break;
                    }
                }
            }
        }
        return;
    }

    public Dictionary<string, double> Calculate2Num(double num1, double num2)
    {
        Dictionary<string, double> result = new Dictionary<string, double>();
        result.Add("+", num1 + num2);
        result.Add("-", num1 - num2);
        result.Add("*", num1 * num2);
        if (num2 != 0)
            result.Add("/", num1 / num2);
        return result;
    }
}

public class CalculateResult
{
    public double CalResult { get; set; }

    public string CalFlow { get; set; }
}