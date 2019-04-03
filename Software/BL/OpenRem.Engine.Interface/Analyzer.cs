using System;

namespace OpenRem.Engine.Interface
{
    public class Analyzer
    {
        public Analyzer(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
        public string Name { get; set; }
    }
}