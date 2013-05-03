using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ReSharper disable CheckNamespace
namespace LiteDispatch.Web
// ReSharper restore CheckNamespace
{
  using System.Threading;
  using System.Web.Mvc;

  public static class HtmlHelperExtensions
  {
    /// <summary>
    /// Converts the .net supported date format current culture format into JQuery Datepicker format.
    /// </summary>
    /// <param name="html">HtmlHelper object.</param>
    /// <returns>Format string that supported in JQuery Datepicker.</returns>
    /// <remarks>
    /// This code was found at:
    /// http://www.rajeeshcv.com/post/details/31/jqueryui-datepicker-in-asp-net-mvc
    /// Example code
    /// <script language="javascript">
    ///    $(document).ready(function() {
    ///    $('#StartDate').datepicker({ dateFormat: '@Html.ConvertDateFormat()' });
    ///    $('#EndDate').datepicker({ dateFormat: '@Html.ConvertDateFormat()' });
    ///    });
    /// </script>
    /// </remarks>
    public static string ConvertDateFormat(this HtmlHelper html)
    {
      return ConvertDateFormat(html, Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern);
    }/// <summary>
    /// Converts the .net supported date format current culture format into JQuery Datepicker format.
    /// </summary>
    /// <param name="html">HtmlHelper object.</param>
    /// <param name="format">Date format supported by .NET.</param>
    /// <returns>Format string that supported in JQuery Datepicker.</returns>
    /// /// <remarks>
    /// This code was found at:
    /// http://www.rajeeshcv.com/post/details/31/jqueryui-datepicker-in-asp-net-mvc
    /// </remarks>
    public static string ConvertDateFormat(this HtmlHelper html, string format)
    {
      /*
       *  Date used in this comment : 5th - Nov - 2009 (Thursday)
       *
       *  .NET    JQueryUI        Output      Comment
       *  --------------------------------------------------------------
       *  d       d               5           day of month(No leading zero)
       *  dd      dd              05          day of month(two digit)
       *  ddd     D               Thu         day short name
       *  dddd    DD              Thursday    day long name
       *  M       m               11          month of year(No leading zero)
       *  MM      mm              11          month of year(two digit)
       *  MMM     M               Nov         month name short
       *  MMMM    MM              November    month name long.
       *  yy      y               09          Year(two digit)
       *  yyyy    yy              2009        Year(four digit)             *
       */

      var currentFormat = format;

      // Convert the date
      currentFormat = currentFormat.Replace("dddd", "DD");
      currentFormat = currentFormat.Replace("ddd", "D");

      // Convert month
      if (currentFormat.Contains("MMMM"))
      {
        currentFormat = currentFormat.Replace("MMMM", "MM");
      }
      else if (currentFormat.Contains("MMM"))
      {
        currentFormat = currentFormat.Replace("MMM", "M");
      }
      else if (currentFormat.Contains("MM"))
      {
        currentFormat = currentFormat.Replace("MM", "mm");
      }
      else
      {
        currentFormat = currentFormat.Replace("M", "m");
      }

      // Convert year
      currentFormat = currentFormat.Contains("yyyy") 
        ? currentFormat.Replace("yyyy", "yy") 
        : currentFormat.Replace("yy", "y");

      return currentFormat;
    }

  }
}