using DupeFinder;
using DupImageLib;

if (args.Length == 0)
{
    Console.WriteLine("No Path Given");
    return;
}

var analyzer = new ImageAnalyzer(args[0]);
var results = analyzer.Analyze();

foreach(var result in results)
{
    Console.WriteLine(result);
}

var processor = new ImageProcessor(results);

var filesToDelete = processor.Process();

foreach (var file in filesToDelete)
{
    File.Delete(file);
}
