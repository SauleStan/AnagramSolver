using System;
using System.Collections.Generic;

namespace AnagramSolver.EF.DatabaseFirst.Models
{
    public partial class Word
    {
        public Word()
        {
            CachedWords = new HashSet<CachedWord>();
            SearchInfos = new HashSet<SearchInfo>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CachedWord> CachedWords { get; set; }
        public virtual ICollection<SearchInfo> SearchInfos { get; set; }
    }
}
