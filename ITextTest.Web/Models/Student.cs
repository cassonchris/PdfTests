using ITextTest.Web.Attributes;

namespace ITextTest.Web.Models
{
    public class Student
    {
        [PdfFormField("Given Name Text Field")]
        [PdfFormField("Given Name Text Box")]
        public string Firstname { get; set; }

        [PdfFormField("Last name")]
        [PdfFormField("Last Name Text Field")]
        [PdfFormField("Last Name Text Box")]
        [PdfFormField("Family Name Text Box")]
        public string Lastname { get; set; }

        public string Major { get; set; }
    }
}
