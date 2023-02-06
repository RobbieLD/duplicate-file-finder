using DupImageLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupeFinder
{
    public class ImageProcessor
    {
        private readonly IEnumerable<ImageDetails> _details;
        public ImageProcessor(IEnumerable<ImageDetails> details)
        {
            _details = details;
        }

        public IEnumerable<string> Process()
        {
            var groups = _details.GroupBy(g => g.Hash, g => new { g.Size, g.Path }, (key, group) => new { Hash = key, Files = group.OrderByDescending(f => f.Size).ToList() }).Where(g => g.Files.Count() > 1);

            foreach (var group in groups)
            {
                foreach (var file in group.Files.Skip(1))
                {
                    yield return file.Path;
                }
            }
        }
    }
}
