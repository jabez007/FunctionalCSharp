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
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static XmlSerializer GetXmlSerializer<T>(this T @this) where T : class =>
      new XmlSerializer(@this.GetType());

    /// <summary>
    /// Basic serialization to an XML string using StringWriter in XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string ToXml<T>(this T @this) where T : class =>
      new StringWriter()
        .Using(writer => writer.Tee(w => @this.GetXmlSerializer().Serialize(w, @this)).ToString());

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
    public static XmlWriter CreateXmlWriter(this StringBuilder @this, XmlWriterSettings settings) =>
      XmlWriter.Create(@this, settings);

    /// <summary>
    /// Serialization to an XML string with defined settings using XmlWriter(StringBuilder) in XmlSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static string ToXml<T>(this T @this, XmlWriterSettings settings) where T : class =>
      new StringBuilder()
        .Tee(sb => sb.CreateXmlWriter(settings).Using(writer => @this.GetXmlSerializer().Serialize(writer, @this)))
        .ToString();

    /// <summary>
    /// Basic deserialization of an XML string to an object of the given type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static T FromXml<T>(this string @this) where T : class =>
      new StringReader(@this)
        .Using(reader => new XmlSerializer(typeof(T)).Deserialize(reader) as T);
  }
}