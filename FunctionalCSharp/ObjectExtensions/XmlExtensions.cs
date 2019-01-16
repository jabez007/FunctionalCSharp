using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace FunctionalCSharp.ObjectExtensions
{
  /// <summary>
  /// Extension methods to functionalize serializing an object to and from XML
  /// </summary>
  public static class XmlExtensions
  {
    #region GetXmlSerializer

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlSerializer GetXmlSerializer<T>(this T @this) where T : class =>
      new XmlSerializer(@this.GetType());

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlSerializer GetXmlSerializer(this Type @this) =>
      new XmlSerializer(@this);

    #endregion GetXmlSerializer

    #region ToXml

    /// <summary>
    /// Basic serialization to an XML string using StringWriter in XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string ToXmlString<T>(this T @this) where T : class =>
      new StringWriter()
        .Using(writer => @this.SerializeTo(writer).ToString());

    /// <summary>
    /// Serialization to an XML string with defined settings using XmlWriter(StringBuilder) in XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static string ToXmlString<T>(this T @this, XmlWriterSettings settings) where T : class =>
      new StringBuilder()
        .CreateXmlWriter(settings)
        .Using(writer => @this.SerializeTo(writer).ToString());

    #region SerializeTo

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static Stream SerializeTo<T>(this T @this, Stream stream) where T : class
    {
      @this.GetXmlSerializer().Serialize(stream, @this);
      return stream;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="textWriter"></param>
    /// <returns></returns>
    public static TextWriter SerializeTo<T>(this T @this, TextWriter textWriter) where T : class
    {
      @this.GetXmlSerializer().Serialize(textWriter, @this);
      return textWriter;
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="xmlWriter"></param>
    /// <returns></returns>
    public static XmlWriter SerializeTo<T>(this T @this, XmlWriter xmlWriter) where T : class
    {
      @this.GetXmlSerializer().Serialize(xmlWriter, @this);
      return xmlWriter;
    }

    #endregion SerializeTo

    #region CreateXmlWriter

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this Stream @this) =>
      XmlWriter.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this StringBuilder @this) =>
      XmlWriter.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this TextWriter @this) =>
      XmlWriter.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this XmlWriter @this) =>
      XmlWriter.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this Stream @this, XmlWriterSettings settings) =>
      XmlWriter.Create(@this, settings);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this StringBuilder @this, XmlWriterSettings settings) =>
      XmlWriter.Create(@this, settings);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this TextWriter @this, XmlWriterSettings settings) =>
      XmlWriter.Create(@this, settings);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlWriter CreateXmlWriter(this XmlWriter @this, XmlWriterSettings settings) =>
      XmlWriter.Create(@this, settings);

    #endregion CreateXmlWriter

    #endregion ToXml

    #region FromXml

    /// <summary>
    /// Basic deserialization of an XML string to an object of the given type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static T FromXmlString<T>(this string @this) where T : class =>
      new StringReader(@this)
        .Using(reader => reader.DeserializeTo<T>());

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static T FromXmlString<T>(this string @this, XmlReaderSettings settings) where T : class =>
      new StringReader(@this)
        .Using(reader => reader.CreateXmlReader(settings).Using(xmlReader => xmlReader.DeserializeTo<T>()));

    #region DeserializeTo

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static T DeserializeTo<T>(this Stream @this) where T : class =>
      typeof(T).GetXmlSerializer().Deserialize(@this) as T;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static T DeserializeTo<T>(this TextReader @this) where T : class =>
      typeof(T).GetXmlSerializer().Deserialize(@this) as T;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static T DeserializeTo<T>(this XmlReader @this) where T : class =>
      typeof(T).GetXmlSerializer().Deserialize(@this) as T;

    #endregion DeserializeTo

    #region CreateXmlReader

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlReader CreateXmlReader(this Stream @this) =>
      XmlReader.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlReader CreateXmlReader(this TextReader @this) =>
      XmlReader.Create(@this);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlReader CreateXmlReader(this Stream @this, XmlReaderSettings settings) =>
      XmlReader.Create(@this, settings);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlReader CreateXmlReader(this TextReader @this, XmlReaderSettings settings) =>
      XmlReader.Create(@this, settings);

    /// <summary>
    ///
    /// </summary>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static XmlReader CreateXmlReader(this XmlReader @this, XmlReaderSettings settings) =>
      XmlReader.Create(@this, settings);

    #endregion CreateXmlReader

    #endregion FromXml
  }
}