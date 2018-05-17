using System;

namespace ITextTest.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PdfFormFieldAttribute : Attribute
    {
        public string FieldName { get; set; }

        public PdfFormFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

    }
}
