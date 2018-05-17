using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AsposePdfTest
{
    class Program
    {
        static void Main()
        {
            var program = new Program();
            var path = @"\users\chris casson\desktop\pdfformexample.pdf";
            var dict = new Dictionary<string, string>
            {
                {"Given Name Text Box", "bob"}
            };
            program.FillField(path, dict);
        }

        public void CreatePdf(string path)
        {
            var reader = new PdfReader(path);
            var oot = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            var stamp = new PdfStamper(reader, oot);
            var field = new TextField(stamp.Writer, new Rectangle(100,100,100,100), "title");
            stamp.AddAnnotation(field.GetTextField(), 1);
            stamp.Close();
        }

        public void FillField(string path, Dictionary<string, string> values)
        {
            var reader = new PdfReader(path);
            var stamper = new PdfStamper(reader, new FileStream(@"\users\chris casson\desktop\itext_stamped.pdf", FileMode.Create));
            var acroFields = stamper.AcroFields;
            foreach (var fieldInformation in acroFields.Fields)
            {
                Console.WriteLine(fieldInformation.Key);
            }

            foreach (var kvPari in values)
            {
                acroFields.SetField(kvPari.Key, kvPari.Value);
            }
            stamper.Close();
        }

        //public void CreatePdf(string path)
        //{
        //    var pdf = new Document();
        //    pdf.Pages.Add();
        //    var field = new TextBoxField(pdf.Pages[1], new Rectangle(100, 100, 100, 100))
        //    {
        //        PartialName = "title",
        //        Value = "bla bla bla"
        //    };
        //    field.Border = new Border(field) {Width = 5};
        //    pdf.Form.Add(field);
        //    foreach (var formField in pdf.Form.Fields)
        //    {
        //        Console.WriteLine(formField.PartialName);
        //    }
        //    pdf.Save(path);
        //}

        //public void FillField(string path)
        //{
        //    var pdf = new Document(path);
        //    pdf.Form.Fields[0].Value = "bla bla bla";
        //    pdf.Save(path);
        //}
    }
}
