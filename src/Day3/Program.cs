using System.Diagnostics;
using System.Text.RegularExpressions;

Stopwatch stopwatch = Stopwatch.StartNew();

var delimiter = ",";
var path = Directory.GetCurrentDirectory() + "//inputData.txt";
var data = File.ReadAllText(path);

string patternPart1 = @"mul\(\d+,\d+\)";
string patternPart2 = @"(?<mul>mul\((?<mulLeft>\d+),(?<mulRight>\d+)\))|(?<do>do\(\))|(?<dont>don't\(\))";

MatchCollection matchesPart1 = Regex.Matches(data, patternPart1);
var part1Result = AddMatchesTogether(matchesPart1).Sum();

MatchCollection matches = Regex.Matches(data, patternPart2);

bool isEnabled = true;
long part2Result = 0;

foreach (Match match in matches)
{
    switch (isEnabled, match.Groups["do"].Success, match.Groups["mul"].Success, match.Groups["dont"].Success)
    {
        case (false, true, _, _):
            isEnabled = true;
            break;

        case (true, _, _, true):
            isEnabled = false;
            break;

        case (true, _, true, _):
            part2Result += int.Parse(match.Groups["mulLeft"].ValueSpan) * int.Parse(match.Groups["mulRight"].ValueSpan);
            break;

        default:
            break;
    }
}

Console.WriteLine($"Part 1: {part1Result}, Part 2: {part2Result} in {stopwatch.ElapsedMilliseconds}ms.");
Console.ReadLine();

IEnumerable<int> AddMatchesTogether(MatchCollection matches)
    => from match in matches
       let values = match.Value.Trim('m', 'u', 'l', '(', ')').Split(delimiter).Select(int.Parse).ToArray()
       select values[0] * values[1];