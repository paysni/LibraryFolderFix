﻿namespace Atlas_Web.Models
{
    public partial class CollectionTerm
    {
        public int TermAnnotationId { get; set; }
        public int? TermId { get; set; }
        public int? DataProjectId { get; set; }
        public int? Rank { get; set; }

        public virtual Collection DataProject { get; set; }
        public virtual Term Term { get; set; }
    }
}
