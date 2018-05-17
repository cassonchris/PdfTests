using ITextTest.Web.Models;
using ITextTest.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITextTest.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly IPdfService _pdfService;

        public CardController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public IActionResult Student()
        {
            var student = new Student
            {
                Firstname = "steve",
                Lastname = "rogers",
                Major = "shields"
            };

            var pdfBytes = _pdfService.FillFormFields("PdfFormExample", student);
            
            return File(pdfBytes, "application/pdf", "studentcard.pdf");
        }
    }
}