namespace DDS.Web.Helpers
{
    using System.Web.Mvc;

    public static class CustomHelpers
    {
        public static MvcHtmlString Alert(this HtmlHelper html, string shortTitle, string message, AlertType alertType)
        {
            var div = new TagBuilder("div");
            var alertTypeCSS = string.Format("alert-{0}", alertType.ToString().ToLower());
            div.AddCssClass("alert");
            div.AddCssClass(alertTypeCSS);

            var a = new TagBuilder("a");
            a.AddCssClass("close");
            a.Attributes.Add("data-dismiss", "alert");
            a.InnerHtml = "&times;";

            div.InnerHtml = string.Format("{0}<strong>{1} </strong>{2}", a.ToString(), shortTitle, message);
            return new MvcHtmlString(div.ToString());
        }
    }

    public enum AlertType
    {
        Info,
        Success,
        Warning,
        Danger
    }
}

// <div class="alert alert-success fade in">
//     <a href = "#" class="close" data-dismiss="alert">&times;</a>
//     <strong>Success!</strong> Your message has been sent successfully.
// </div>
