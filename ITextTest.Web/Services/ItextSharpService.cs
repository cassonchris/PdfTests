using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;

namespace ITextTest.Web.Services
{
    public class ItextSharpService// : IPdfService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ItextSharpService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public byte[] FillFormFields(string templateName, Dictionary<string, string> values)
        {
            using (var outStream = new MemoryStream())
            {
                using (var reader = new PdfReader($"{_hostingEnvironment.ContentRootPath}" +
                                                  $"{Path.DirectorySeparatorChar}CardTemplates" +
                                                  $"{Path.DirectorySeparatorChar}{templateName}.pdf"))
                {
                    using (var stamper = new PdfStamper(reader, outStream))
                    {

                        var acroFields = stamper.AcroFields;
                        foreach (var kvPair in values)
                        {
                            acroFields.SetField(kvPair.Key, kvPair.Value);
                        }

                        // this will flatten it but also remove the values set
                        //stamper.FormFlattening = true;
                    }
                }

                return outStream.ToArray();
            }
        }
    }
}
