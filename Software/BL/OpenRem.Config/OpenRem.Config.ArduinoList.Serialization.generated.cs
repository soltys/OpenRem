//-----------------------------------------------------------------------------------------------------
// <auto-generated>
//     This source code was auto-generated by XsdClassGen.tt.
//     Runtime Version: 4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OpenRem.Config
{
    /// <summary>
    ///   Automatically generated mapping for the ArduinoList XML element
    ///   declared in the OpenRem.Config namespace.
    /// </summary>
	public partial class ArduinoList
	{
        /// <summary>
        ///   Deserializes given string into an instance of <see cref="ArduinoList" />.
        /// </summary>
        /// <param name="str">The string from which an instance of <see cref="ArduinoList" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="ArduinoList" /> deserialized from given string.</returns>
        /// <exception cref="ArgumentException">Given string is null or empty.</exception>
		public static ArduinoList DeserializeFrom(string str)
		{
            if (String.IsNullOrEmpty(str)) {
                throw new ArgumentException("Cannot deserialize from a null or empty string.", "str");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
		    using (var stream = new StringReader(str)) {
			    return serializer.Deserialize(stream) as ArduinoList;
		    }
		}        
        
        /// <summary>
        ///   Deserializes given stream into an instance of <see cref="ArduinoList" />.
        /// </summary>
        /// <param name="stream">The stream from which an instance of <see cref="ArduinoList" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="ArduinoList" /> deserialized from given stream.</returns>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public static ArduinoList DeserializeFrom(Stream stream)
		{
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			return serializer.Deserialize(stream) as ArduinoList;
		}

        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="ArduinoList" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="ArduinoList" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="ArduinoList" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static ArduinoList DeserializeFrom(TextReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			return serializer.Deserialize(reader) as ArduinoList;
		}
        
        /// <summary>
        ///   Deserializes given reader into an instance of <see cref="ArduinoList" />.
        /// </summary>
        /// <param name="reader">The reader from which an instance of <see cref="ArduinoList" /> should be deserialized.</param>
        /// <returns>An instance of <see cref="ArduinoList" /> deserialized from given reader.</returns>
        /// <exception cref="ArgumentNullException">Given reader is null.</exception>
		public static ArduinoList DeserializeFrom(XmlReader reader)
		{            
            if (reader == null) {
                throw new ArgumentNullException("reader");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			return serializer.Deserialize(reader) as ArduinoList;
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="ArduinoList" /> into given string.
        /// </summary>
        /// <param name="str">The string into which the instance of <see cref="ArduinoList" /> should be serialized.</param>
		public void SerializeTo(ref string str)
		{
			var serializer = new XmlSerializer(typeof(ArduinoList));
            using (var stream = new StringWriter()) {
			    serializer.Serialize(stream, this);
                str = stream.ToString();
            }
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="ArduinoList" /> into given stream.
        /// </summary>
        /// <param name="stream">The stream into which the instance of <see cref="ArduinoList" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given stream is null.</exception>
		public void SerializeTo(Stream stream)
		{            
            if (stream == null) {
                throw new ArgumentNullException("stream");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			serializer.Serialize(stream, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="ArduinoList" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="ArduinoList" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(TextWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			serializer.Serialize(writer, this);
		}
        
        /// <summary>
        ///   Serializes this instance of <see cref="ArduinoList" /> into given writer.
        /// </summary>
        /// <param name="writer">The writer into which the instance of <see cref="ArduinoList" /> should be serialized.</param>
        /// <exception cref="ArgumentNullException">Given writer is null.</exception>
		public void SerializeTo(XmlWriter writer)
		{            
            if (writer == null) {
                throw new ArgumentNullException("writer");
            }
			var serializer = new XmlSerializer(typeof(ArduinoList));
			serializer.Serialize(writer, this);
		}
	}
}