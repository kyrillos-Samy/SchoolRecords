﻿namespace Entities
{
    public class Class : BaseEntity
    {
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string Name { get; set; }
    }
}
