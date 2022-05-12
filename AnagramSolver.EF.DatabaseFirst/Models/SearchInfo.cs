using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class SearchInfo
    {
        public int Id { get; set; }
        public string? UserIp { get; set; }
        public TimeSpan? ExecTime { get; set; }
        public string? SearchedWord { get; set; }
        public int? AnagramId { get; set; }

        public virtual Word? Anagram { get; set; }
    }
}
