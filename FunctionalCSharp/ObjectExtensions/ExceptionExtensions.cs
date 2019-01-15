using System;

namespace FunctionalCSharp.ObjectExtensions
{
  /// <summary>
  ///
  /// </summary>
  public static class ExceptionExtensions
  {
    /// <summary>
    /// Gathers the exception type, message, and stack trace for the extended exception
    /// and all inner exceptions recursively.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public static string GetMessageStack(this Exception @this, string message = "") =>
      message
        .WhenNotNullOrEmpty(m => string.Format("{0}{1}", m, Environment.NewLine))
        .Append(@this._GetMessageStack())
        .TrimEnd();

    private static string _GetMessageStack(this Exception @this) =>
      ""
        .AppendFormat(
          "\tException type: {1}{0}\tException Message: {2}{0}\tStack trace: {3}{0}",
          Environment.NewLine, @this.GetType(), @this.Message, @this.StackTrace
        )
        .When(
          m => @this.InnerException != null,
          m => m.AppendFormat(
            "\t---- BEGIN Inner Exception----{0}{1}\t---- END Inner Exception ----{0}",
            Environment.NewLine, @this.InnerException._GetMessageStack()
            )
        );
  }
}