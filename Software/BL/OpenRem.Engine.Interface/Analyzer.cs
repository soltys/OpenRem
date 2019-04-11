using System;
using System.Runtime.Serialization;

namespace OpenRem.Engine
{
    [DataContract]
    public class Analyzer
    {
        public Analyzer(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; }
        [DataMember]
        public string Name { get; set; }
    }
}