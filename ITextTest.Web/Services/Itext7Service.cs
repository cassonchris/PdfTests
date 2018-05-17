using System.IO;
using System.Reflection;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using ITextTest.Web.Attributes;
using Microsoft.AspNetCore.Hosting;

namespace ITextTest.Web.Services
{
    public class Itext7Service : IPdfService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public Itext7Service(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public byte[] FillFormFields<T>(string templateName, T obj) where T : class 
        {
            using (var outStream = new MemoryStream())
            {
                using (var pdfDoc = new PdfDocument(
                    new PdfReader($"{_hostingEnvironment.ContentRootPath}" +
                                  $"{Path.DirectorySeparatorChar}CardTemplates" +
                                  $"{Path.DirectorySeparatorChar}{templateName}.pdf"),
                    new PdfWriter(outStream)))
                {
                    var form = PdfAcroForm.GetAcroForm(pdfDoc, false);

                    var objProperties = typeof(T).GetProperties();
                    foreach (var property in objProperties)
                    {
                        GetMatchingFormField(form, property)?.SetValue(property.GetValue(obj).ToString());
                    }

                    form.FlattenFields();
                }

                return outStream.ToArray();
            }
        }

        public PdfFormField GetMatchingFormField(PdfAcroForm form, PropertyInfo property)
        {
            var field = form.GetField(property.Name);
            if (field != null)
            {
                return field;
            }

            var attributes = property.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (attribute is PdfFormFieldAttribute pdfAttribute)
                {
                    field = form.GetField(pdfAttribute.FieldName);
                    if (field != null)
                    {
                        return field;
                    }
                }
            }

            return null;
        }
    }
}
