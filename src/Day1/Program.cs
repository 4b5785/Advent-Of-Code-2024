using System.Diagnostics;

Stopwatch stopwatch = Stopwatch.StartNew();

var delimiter = "   ";
var path = Directory.GetCurrentDirectory() + "//inputData.csv";
var data = File.ReadAllLines(path);

var dataLength = data.Length;

var left = new int[dataLength];
var right = new int[dataLength];

for (int i = 0; i < dataLength; i++)
{
    var parts = data[i].Split(delimiter);

    left[i] = int.Parse(parts[0]);
    right[i] = int.Parse(parts[1]);
}

var groupedRight = right.GroupBy(x => x);

Array.Sort(left);
Array.Sort(right);

int part1Result = 0;
long part2Result = 0;

for (int i = 0; i < dataLength; i++)
{
    part1Result += Math.Abs(left[i] - right[i]);
    part2Result += left[i] * groupedRight.FirstOrDefault(x => x.Key == left[i])?.Count() ?? 0;
}

Console.WriteLine($"Part 1: {part1Result}, Part 2: {part2Result} in {stopwatch.ElapsedMilliseconds}ms.");
Console.ReadLine();