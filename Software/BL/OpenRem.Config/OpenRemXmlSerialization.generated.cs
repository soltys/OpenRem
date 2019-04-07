﻿//-----------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------------------------------

// If you use this template inside a _portable_ class library,
// then you should define the PORTABLE conditional compilation symbol
// in order to enable the creation of some necessary stub classes.
#if PORTABLE

namespace System
{
    /// <summary>
    ///   Fake, this is used only to allow serialization on portable platforms.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = true, Inherited = false)]
    internal sealed class SerializableAttribute : Attribute
    {
        // This does nothing and should do nothing.
    }
}

#endif

namespace OpenRem.Config {
    
    
    /// <summary>
    ///   Automatically generated XML type mapping for ArduinoList.
    /// </summary>
    /// <remarks>
    ///   This is an automaticaly generated type, please do not edit it.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd", IsNullable=true)]
    public partial class ArduinoList {
        
        private Arduino[] arduinoField;
        
        /// <summary>
        ///   Gets or sets Arduino.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Arduino")]
        public Arduino[] Arduino {
            get {
                return this.arduinoField;
            }
            set {
                this.arduinoField = value;
            }
        }
    }
    
    /// <summary>
    ///   Automatically generated XML type mapping for Arduino.
    /// </summary>
    /// <remarks>
    ///   This is an automaticaly generated type, please do not edit it.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd", IsNullable=true)]
    public partial class Arduino {
        
        private string nameField;
        
        private string sampleRateField;
        
        private string bitRateField;
        
        private string channelsNumberField;
        
        private Probe[] probeField;
        
        /// <summary>
        ///   Gets or sets Name.
        /// </summary>
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets SampleRate.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
        public string SampleRate {
            get {
                return this.sampleRateField;
            }
            set {
                this.sampleRateField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets BitRate.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
        public string BitRate {
            get {
                return this.bitRateField;
            }
            set {
                this.bitRateField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets ChannelsNumber.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType="positiveInteger")]
        public string ChannelsNumber {
            get {
                return this.channelsNumberField;
            }
            set {
                this.channelsNumberField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets Probe.
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("Probe")]
        public Probe[] Probe {
            get {
                return this.probeField;
            }
            set {
                this.probeField = value;
            }
        }
    }
    
    /// <summary>
    ///   Automatically generated XML type mapping for Probe.
    /// </summary>
    /// <remarks>
    ///   This is an automaticaly generated type, please do not edit it.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd", IsNullable=true)]
    public partial class Probe {
        
        private Microphone inputField;
        
        private Microphone outputField;
        
        private ProbeSide sideField;
        
        private bool sideFieldSpecified;
        
        /// <summary>
        ///   Gets or sets Input.
        /// </summary>
        public Microphone Input {
            get {
                return this.inputField;
            }
            set {
                this.inputField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets Output.
        /// </summary>
        public Microphone Output {
            get {
                return this.outputField;
            }
            set {
                this.outputField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets Side.
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ProbeSide Side {
            get {
                return this.sideField;
            }
            set {
                this.sideField = value;
            }
        }
        
        /// <summary>
        ///   Gets or sets SideSpecified.
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SideSpecified {
            get {
                return this.sideFieldSpecified;
            }
            set {
                this.sideFieldSpecified = value;
            }
        }
    }
    
    /// <summary>
    ///   Automatically generated XML type mapping for Microphone.
    /// </summary>
    /// <remarks>
    ///   This is an automaticaly generated type, please do not edit it.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd", IsNullable=true)]
    public partial class Microphone {
        
        private string channelField;
        
        /// <summary>
        ///   Gets or sets Channel.
        /// </summary>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string Channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
    }
    
    /// <summary>
    ///   Automatically generated XML type mapping for ProbeSide.
    /// </summary>
    /// <remarks>
    ///   This is an automaticaly generated type, please do not edit it.
    /// </remarks>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="https://github.com/soltys/OpenRem/ArduinoConfig.xsd")]
    public enum ProbeSide {
        
        /// <summary>
        ///   Value 'Left' for enumeration ProbeSide.
        /// </summary>
        Left,
        
        /// <summary>
        ///   Value 'Right' for enumeration ProbeSide.
        /// </summary>
        Right,
    }
}
