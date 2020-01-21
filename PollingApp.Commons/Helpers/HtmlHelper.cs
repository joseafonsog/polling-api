using System;
using System.Collections.Generic;
using System.Text;

namespace PollingApp.Commons.Helpers
{
    public static class HtmlHelper
    {
        public static string GetHtmlBody(string content, string title)
        {
            var body = $@"
            <!doctype html>
            <html>
                <body>
                    <h1>{title}</h1>
                    <a href='{content}'>{content}</a>
                </body>
            </html>";

            return body;
        }
    }
}
