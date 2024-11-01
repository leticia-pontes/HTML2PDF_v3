using System;
using System.IO;
using System.Text;

namespace HTML2PDF_v3
{
    public class HTMLConverter
    {
        public string ConvertHtmlToText(string html)
        {
            string formatHtml = html.Replace("<!DOCTYPE html>", "\n")
                                .Replace("html lang=\"pt-BR\"", "\n")
                                .Replace("<br>", "").Replace("</br>", "\n")
                                .Replace("<hr>", "").Replace("</hr>", "\n")
                                .Replace("<strong>", "").Replace("</strong>", "\n")
                                .Replace("<em>", "").Replace("</em>", "\n")
                                .Replace("<small>", "").Replace("</small>", "\n")
                                .Replace("<mark>", "").Replace("</mark>", "\n")
                                .Replace("<blockquote>", "").Replace("</blockquote>", "\n")
                                .Replace("<cite>", "").Replace("</cite>", "\n")
                                .Replace("<ul>", "").Replace("</ul>", "\n")
                                .Replace("<ol>", "").Replace("</ol>", "\n")
                                .Replace("<li>", "").Replace("</li>", "\n")
                                .Replace("<a>", "").Replace("</a>", "\n")
                                .Replace("<img>", "").Replace("</img>", "\n")
                                .Replace("<figure>", "").Replace("</figure>", "\n")
                                .Replace("<figcaption>", "").Replace("</figcaption>", "\n")
                                .Replace("<table>", "").Replace("</table>", "\n")
                                .Replace("<tr>", "").Replace("</tr>", "\n")
                                .Replace("<td>", "").Replace("</td>", "\n")
                                .Replace("<th>", "").Replace("</th>", "\n")
                                .Replace("<thead>", "").Replace("</thead>", "\n")
                                .Replace("<tbody>", "").Replace("</tbody>", "\n")
                                .Replace("<tfoot>", "").Replace("</tfoot>", "\n")
                                .Replace("<head>", "").Replace("</head>", "\n")
                                .Replace("<meta charset=\"UTF-8\">", "\n")
                                .Replace("<title>", "").Replace("</title>", "\n")
                                .Replace("<body>", "").Replace("</body>", "\n")
                                .Replace("<header>", "").Replace("</header>", "\n")
                                .Replace("<h1>", "").Replace("</h1>", "\n")
                                .Replace("<nav>", "").Replace("</nav>", "\n")
                                .Replace("<section>", "").Replace("</section>", "\n")
                                .Replace("<h2>", "").Replace("</h2>", "\n")
                                .Replace("<footer>", "").Replace("</footer>", "\n")
                                .Replace("meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"", "")
                                .Replace("section id=\"sobre\"", "")
                                .Replace("<a>", "").Replace("</a>", "")
                                .Replace("&copy; 2024", "")
                                .Replace("<p>", "").Replace("</p>", "")
                                .Replace("<>", "").Replace("<", "").Replace(">", "")
                                .Replace("a href=\"#sobre\"", "")
                                .Replace("a href=\"#contato\"", "")
                                .Replace("/html", "");

            return formatHtml;
        }
    }
}
