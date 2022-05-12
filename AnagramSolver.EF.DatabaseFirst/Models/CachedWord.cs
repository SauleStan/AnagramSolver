using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class CachedWord
    {
        public int Id { get; set; }
        public string? InputWord { get; set; }
        public int? AnagramWordId { get; set; }

        public virtual Word? AnagramWord { get; set; }
    }
}
