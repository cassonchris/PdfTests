using System.Collections.Generic;
using System.IO;

namespace ITextTest.Web.Services
{
    public interface IPdfService
    {
        byte[] FillFormFields<T>(string templateName, T obj) where T : class;
    }
}
