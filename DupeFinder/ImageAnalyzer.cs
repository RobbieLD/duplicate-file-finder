using DupImageLib;
using System.Drawing;
using System.Drawing.Imaging;

namespace DupeFinder
{
    public class ImageAnalyzer
    {
        private readonly string _path;
        private readonly Stack<ImageDetails> _details = new();
        private readonly ImageHashes _hasher = new (new ImageSharpTransformer());

        public ImageAnalyzer(string path)
        {
            _path = path;
        }

        public Stack<ImageDetails> Analyze()
        {
            var files = Directory.EnumerateFiles(_path);

            int count = 0;
            foreach (var file in files)
            {
                count++;

                if (count % 10 == 0)
                {
                    Console.WriteLine($"{count}/{files.Count()} images analyzed");
                }

                var hash = GetHash(file);

                if (hash == 0) continue;

                _details.Push(new ImageDetails
                {
                    Hash = hash,
                    Path = file,
                    Size = new FileInfo(file).Length
                });
            }

            Console.WriteLine("All images analyzed");
            return _details;
        }

        private ulong GetHash(string file)
        {
            try
            {
                return _hasher.CalculateDctHash(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                var broken = $"{Path.GetDirectoryName(file)}\\broken";
                if (!Directory.Exists(broken))
                {
                    Directory.CreateDirectory(broken);
                }

                File.Move(file, $"{broken}\\{Path.GetFileName(file)}");
                return 0;
            }
        }
    }
}
