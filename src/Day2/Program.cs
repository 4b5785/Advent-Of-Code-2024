using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

var delimiter = " ";
var path = Directory.GetCurrentDirectory() + "//inputData.csv";
var data = File.ReadAllLines(path);

var dataLength = data.Length;

var minLevelStep = 1;
var maxLevelStep = 3;

int part1Result = 0;
long part2Result = 0;

var reports = data.Select(x => x.Split(delimiter).Select(int.Parse).ToArray()).ToArray();

for (int i = 0; i < dataLength; i++)
{
    part1Result += ReportIsValid(reports[i]) ? 1 : 0;

    var reportIsValid = false;

    var reportVariations = GenerateAllReportVariations(reports[i]);

    for (int j = 0; j < reportVariations.Length; j++)
    {
        reportIsValid = ReportIsValid(reportVariations[j]);

        if (reportIsValid) break;
    }

    part2Result += reportIsValid ? 1 : 0;
}

Console.WriteLine($"Part 1: {part1Result}, Part 2: {part2Result} in {stopwatch.ElapsedMilliseconds}ms.");
Console.ReadLine();

bool ReportIsValid(int[] report)
{
    bool reportIsValid = true;

    var reportIsOrdered = (report[0] - report[1]) > 0 ? ReportIsAllIncreasing(report) : ReportIsAllDecreasing(report);

    if (!ReportIsValueValid(report))
    {
        reportIsValid = false;
    }

    return reportIsOrdered && reportIsValid;
}

bool ReportIsValueValid(int[] report)
{
    var isIncrising = (report[0] - report[1]) > 0;

    for (int i = 1; i < report.Length; i++)
    {
        var differenceBetweenSteps = Math.Abs(report[i - 1] - report[i]);

        if (differenceBetweenSteps < minLevelStep || differenceBetweenSteps > maxLevelStep)
        {
            return false;
        }
    }

    return true;
}

bool ReportIsAllIncreasing(int[] report)
{
    for (int i = 1; i < report.Length; i++)
    {
        if (report[i - 1] <= report[i])
        {
            return false;
        }
    }

    return true;
}

bool ReportIsAllDecreasing(int[] report)
{
    for (int i = 1; i < report.Length; i++)
    {
        if (report[i - 1] >= report[i])
        {
            return false;
        }
    }

    return true;
}

int[][] GenerateAllReportVariations(int[] report)
{
    var reportVariations = (from j in Enumerable.Range(0, report.Length + 1)
                            let before = report.Take(j - 1)
                            let after = report.Skip(j)
                            select Enumerable.Concat(before, after).ToArray()).ToArray();

    return reportVariations;
}