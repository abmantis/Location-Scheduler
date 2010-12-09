using System;
using System.Xml.Serialization;	 // For serialization of an object to an XML Document file.
//using System.Runtime.Serialization.Formatters.Binary; // For serialization of an object to an XML Binary file.
using System.IO;				 // For reading/writing data to an XML file.
using System.Collections.Generic;

namespace HelperLib
{
	/// <summary>
	/// Serialization format types.
	/// </summary>
	public enum SerializedFormat
	{
		/// <summary>
		/// Binary serialization format.
		/// </summary>
		Binary,

		/// <summary>
		/// Document serialization format.
		/// </summary>
		Document
	}

	/// <summary>
	/// Facade to XML serialization and deserialization of strongly typed objects to/from an XML file.
	/// 
	/// References: XML Serialization at http://samples.gotdotnet.com/:
	/// http://samples.gotdotnet.com/QuickStart/howto/default.aspx?url=/quickstart/howto/doc/xmlserialization/rwobjfromxml.aspx
	/// </summary>
	public static class ObjectXMLSerializer<T> where T : class // Specify that T must be a class.
	{
		#region Load methods

		/// <summary>
		/// Loads an object from an XML file in Document format.
		/// </summary>
		/// <example>
		/// <code>
		/// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml");
		/// </code>
		/// </example>
		/// <param name="path">Path of the file to load the object from.</param>
		/// <returns>Object loaded from an XML file in Document format.</returns>
		public static T Load(string path)
		{
			T serializableObject = LoadFromDocumentFormat(null, path);
			return serializableObject;
		}

		/// <summary>
		/// Loads an object from an XML file using a specified serialized format.
		/// </summary>
		/// <example>
		/// <code>
		/// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", SerializedFormat.Binary);
		/// </code>
		/// </example>		
		/// <param name="path">Path of the file to load the object from.</param>
		/// <param name="serializedFormat">XML serialized format used to load the object.</param>
		/// <returns>Object loaded from an XML file using the specified serialized format.</returns>
		public static T Load(string path, SerializedFormat serializedFormat)
		{
			T serializableObject = null;

			switch (serializedFormat)
			{
				case SerializedFormat.Binary:
					serializableObject = LoadFromBinaryFormat(path);
					break;

				case SerializedFormat.Document:
				default:
					serializableObject = LoadFromDocumentFormat(null, path);
					break;
			}

			return serializableObject;
		}

		/// <summary>
		/// Loads an object from an XML file in Document format, supplying extra data types to enable deserialization of custom types within the object.
		/// </summary>
		/// <example>
		/// <code>
		/// serializableObject = ObjectXMLSerializer&lt;SerializableObject&gt;.Load(@"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
		/// </code>
		/// </example>
		/// <param name="path">Path of the file to load the object from.</param>
		/// <param name="extraTypes">Extra data types to enable deserialization of custom types within the object.</param>
		/// <returns>Object loaded from an XML file in Document format.</returns>
		public static T Load(string path, System.Type[] extraTypes)
		{
			T serializableObject = LoadFromDocumentFormat(extraTypes, path);
			return serializableObject;
		}

		#endregion

		#region Save methods

		/// <summary>
		/// Saves an object to an XML file in Document format.
		/// </summary>
		/// <example>
		/// <code>        
		/// SerializableObject serializableObject = new SerializableObject();
		/// 
		/// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml");
		/// </code>
		/// </example>
		/// <param name="serializableObject">Serializable object to be saved to file.</param>
		/// <param name="path">Path of the file to save the object to.</param>
		public static void Save(T serializableObject, string path)
		{
			SaveToDocumentFormat(serializableObject, null, path);
		}

		/// <summary>
		/// Saves an object to an XML file using a specified serialized format.
		/// </summary>
		/// <example>
		/// <code>
		/// SerializableObject serializableObject = new SerializableObject();
		/// 
		/// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml", SerializedFormat.Binary);
		/// </code>
		/// </example>
		/// <param name="serializableObject">Serializable object to be saved to file.</param>
		/// <param name="path">Path of the file to save the object to.</param>
		/// <param name="serializedFormat">XML serialized format used to save the object.</param>
		public static void Save(T serializableObject, string path, SerializedFormat serializedFormat)
		{
			switch (serializedFormat)
			{
				case SerializedFormat.Binary:
					SaveToBinaryFormat(serializableObject, path);
					break;

				case SerializedFormat.Document:
				default:
					SaveToDocumentFormat(serializableObject, null, path);
					break;
			}
		}

		/// <summary>
		/// Saves an object to an XML file in Document format, supplying extra data types to enable serialization of custom types within the object.
		/// </summary>
		/// <example>
		/// <code>        
		/// SerializableObject serializableObject = new SerializableObject();
		/// 
		/// ObjectXMLSerializer&lt;SerializableObject&gt;.Save(serializableObject, @"C:\XMLObjects.xml", new Type[] { typeof(MyCustomType) });
		/// </code>
		/// </example>
		/// <param name="serializableObject">Serializable object to be saved to file.</param>
		/// <param name="path">Path of the file to save the object to.</param>
		/// <param name="extraTypes">Extra data types to enable serialization of custom types within the object.</param>
		public static void Save(T serializableObject, string path, System.Type[] extraTypes)
		{
			SaveToDocumentFormat(serializableObject, extraTypes, path);
		}

		#endregion

		#region Private

		private static FileStream CreateFileStream(string path)
		{
			FileStream fileStream = null;
			fileStream = new FileStream(path, FileMode.OpenOrCreate);
			
			return fileStream;
		}

		private static T LoadFromBinaryFormat(string path)
		{
			T serializableObject = null;

// 			using (FileStream fileStream = CreateFileStream(path))
// 			{
// 				BinaryFormatter binaryFormatter = new BinaryFormatter();
// 				serializableObject = binaryFormatter.Deserialize(fileStream) as T;
// 			}

			return serializableObject;
		}

		private static T LoadFromDocumentFormat(System.Type[] extraTypes, string path)
		{
			T serializableObject = null;

			using (TextReader textReader = CreateTextReader(path))
			{
				XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
				serializableObject = xmlSerializer.Deserialize(textReader) as T;

			}

			return serializableObject;
		}

		private static TextReader CreateTextReader(string path)
		{
			TextReader textReader = null;
			textReader = new StreamReader(path);

			return textReader;
		}

		private static TextWriter CreateTextWriter(string path)
		{
			TextWriter textWriter = null;
			textWriter = new StreamWriter(path);

			return textWriter;
		}

		private static XmlSerializer CreateXmlSerializer(System.Type[] extraTypes)
		{
			Type ObjectType = typeof(T);

			XmlSerializer xmlSerializer = null;

			if (extraTypes != null)
				xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
			else
				xmlSerializer = new XmlSerializer(ObjectType);

			return xmlSerializer;
		}

		private static void SaveToDocumentFormat(T serializableObject, System.Type[] extraTypes, string path)
		{
			using (TextWriter textWriter = CreateTextWriter(path))
			{
				XmlSerializer xmlSerializer = CreateXmlSerializer(extraTypes);
				xmlSerializer.Serialize(textWriter, serializableObject);
			}
		}

		private static void SaveToBinaryFormat(T serializableObject, string path)
		{
// 			using (FileStream fileStream = CreateFileStream(path))
// 			{
// 				BinaryFormatter binaryFormatter = new BinaryFormatter();
// 				binaryFormatter.Serialize(fileStream, serializableObject);
// 			}
		}

		#endregion
	}
}