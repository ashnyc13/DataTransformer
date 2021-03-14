using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransformer.Models
{
    public class PipelineProgressEventArgs
    {
        public int PercentProgress { get; set; }
        public string StatusMessage { get; set; }
    }
}
