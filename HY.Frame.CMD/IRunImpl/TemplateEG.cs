using HY.Frame.Core.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.CMD
{
    internal class TemplateEG : IRuner
    {
        public void Go()
        {
            var model = new
            {
                Id = 10,
                Name = "Name1",
                Items = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" }
            };

            var templateService = new TemplatesService(new FileSystemService(), new RazorTemplateEngine());

            var result = templateService.Parse("first", model);
            Console.Write(result);
        }
    }
}
