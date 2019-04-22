using System;
using System.Runtime.Serialization;

namespace OpenRem.Engine
{
    [DataContract]
    public class Analyzer
    {
        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Name { get; set; }
    }
}