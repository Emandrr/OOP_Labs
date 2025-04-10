using OOP_Lab2.FileSafe;
using OOP_Lab2.UserStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Lab2.ChangeFormatAdapters
{
    class CurrentToXMLAdapter
    {
        public void Parse(IUserStrategy admin, WorkWithCloud w_c, Document document)
        {
            if (document.type == 2)
            {
                document.SetText(FromMarkdownToXml(document.GetText()));
            }
            if (document.type == 4)
            {
                document.SetText(FromRtfToXml(document.GetText()));
            }
            document.type = 3;
            string[] tmp = document.name.Split(".");
            int i = 0;
            string txt = "";

            foreach (string s in tmp)
            {
                if (i == tmp.Length - 1) continue;
                else txt += s;
                ++i;
            }
            txt += ".xml";
            if (admin is AdminStrategy adm) adm.Upload(txt);
            if (admin is EditorStrategy ed) ed.Upload(txt);
            document.name = txt;
            //w_c.UploadFile(document.name, document.type, "17gYVcgPxxoM4UsNsyq-i2uk8K9RGI4Co", "");
            // admin.SaveCloud(document.GetText());
        }
        public static string FromMarkdownToXml(string markdown)
        {

            string xml = markdown;

            xml = Regex.Replace(xml, @"\*\*\*(.*?)\*\*\*", "<bold><italic>$1</italic></bold>");

            xml = Regex.Replace(xml, @"\*\*(.*?)\*\*", "<bold>$1</bold>");

            xml = Regex.Replace(xml, @"_(.*?)_", "<italic>$1</italic>");

            
            xml = Regex.Replace(xml, @"<u>(.*?)</u>", "<underline>$1</underline>");

            

            return xml;
        }
        public static string FromRtfToXml(string rtf)
        {
            var xml = new StringBuilder();


            string cleanRtf = rtf;

            string text = cleanRtf;

            text = Regex.Replace(text,
                @"\\b\\i\\ul\s?(.*?)\\ul0\\i0\\b0",
                "<bold><italic><underline>$1</underline></italic></bold>",
                RegexOptions.Singleline);

            text = Regex.Replace(text,
                @"\\b\\i\s?(.*?)\\i0\\b0",
                "<bold><italic>$1</italic></bold>",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"\\b\\ul\s?(.*?)\\ul0\\b0",
                "<bold><underline>$1</underline></bold>",
                RegexOptions.Singleline);
            text = Regex.Replace(text,
                @"\\i\\ul\s?(.*?)\\ul0\\i0",
                "<italic><underline>$1</underline></italic>",
                RegexOptions.Singleline);

            // 3. Одиночные стили
            text = Regex.Replace(text, @"\\b\s?(.*?)\\b0", "<bold>$1</bold>", RegexOptions.Singleline);
            text = Regex.Replace(text, @"\\i\s?(.*?)\\i0", "<italic>$1</italic>", RegexOptions.Singleline);
            text = Regex.Replace(text, @"\\ul\s?(.*?)\\ul0", "<underline>$1</underline>", RegexOptions.Singleline);



            xml.Append(text);

            return xml.ToString();
        }
    }
}
