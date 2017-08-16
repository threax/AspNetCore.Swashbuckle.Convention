using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Xunit;

namespace Threax.AspNetCore.FileRepository.Tests
{
    public class TestVerifiers
    {
        [Fact]
        public void Pdf()
        {
            TestVerifier("TestFiles/Pdf.pdf", new FileVerifier().AddPdf(), FileVerifierFactory.PdfMimeType);
        }

        [Fact]
        public void Docx()
        {
            TestVerifier("TestFiles/Docx.docx", new FileVerifier().AddDocx(), FileVerifierFactory.DocxMimeType);
        }

        [Fact]
        public void Xlsx()
        {
            TestVerifier("TestFiles/Xlsx.xlsx", new FileVerifier().AddXlsx(), FileVerifierFactory.XlsxMimeType);
        }

        [Fact]
        public void Pptx()
        {
            TestVerifier("TestFiles/Pptx.pptx", new FileVerifier().AddPptx(), FileVerifierFactory.PptxMimeType);
        }

        [Fact]
        public void Doc()
        {
            TestVerifier("TestFiles/Doc.doc", new FileVerifier().AddDoc(), FileVerifierFactory.DocMimeType);
        }

        [Fact]
        public void Xls()
        {
            TestVerifier("TestFiles/Xls.xls", new FileVerifier().AddXls(), FileVerifierFactory.XlsMimeType);
        }

        [Fact]
        public void Ppt()
        {
            TestVerifier("TestFiles/Ppt.ppt", new FileVerifier().AddPpt(), FileVerifierFactory.PptMimeType);
        }

        [Fact]
        public void Mismatch()
        {
            var file = "TestFiles/Ppt.ppt";
            var verifier = new FileVerifier().AddXls();
            var mimeType = FileVerifierFactory.PptMimeType;

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Assert.ThrowsAny<Exception>(new Action(() => verifier.Validate(stream, file, mimeType)));
            }
        }

        public void TestVerifier(String file, IFileVerifier verifier, String mimeType)
        {
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                verifier.Validate(stream, file, mimeType);
            }
        }
    }
}
