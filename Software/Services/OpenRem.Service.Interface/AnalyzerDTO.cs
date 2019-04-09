using System;
using System.Runtime.Serialization;

namespace OpenRem.Service.Interface
{
    [DataContract]
    public class AnalyzerDTO
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}