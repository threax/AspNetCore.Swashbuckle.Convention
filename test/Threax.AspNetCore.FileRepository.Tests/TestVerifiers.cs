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
            TestVerifier("TestFiles/Pdf.pdf", FileVerifierFactory.CreatePdfVerifier(), FileVerifierFactory.PdfMimeType);
        }

        [Fact]
        public void Docx()
        {
            TestVerifier("TestFiles/Docx.docx", FileVerifierFactory.CreateDocxVerifier(), FileVerifierFactory.DocxMimeType);
        }

        [Fact]
        public void Xlsx()
        {
            TestVerifier("TestFiles/Xlsx.xlsx", FileVerifierFactory.CreateXlsxVerifier(), FileVerifierFactory.XlsxMimeType);
        }

        [Fact]
        public void Pptx()
        {
            TestVerifier("TestFiles/Pptx.pptx", FileVerifierFactory.CreatePptxVerifier(), FileVerifierFactory.PptxMimeType);
        }

        [Fact]
        public void Doc()
        {
            TestVerifier("TestFiles/Doc.doc", FileVerifierFactory.CreateDocVerifier(), FileVerifierFactory.DocMimeType);
        }

        [Fact]
        public void Xls()
        {
            TestVerifier("TestFiles/Xls.xls", FileVerifierFactory.CreateXlsVerifier(), FileVerifierFactory.XlsMimeType);
        }

        [Fact]
        public void Ppt()
        {
            TestVerifier("TestFiles/Ppt.ppt", FileVerifierFactory.CreatePptVerifier(), FileVerifierFactory.PptMimeType);
        }

        [Fact]
        public void Mismatch()
        {
            var file = "TestFiles/Ppt.ppt";
            var verifier = FileVerifierFactory.CreateXlsVerifier();
            var mimeType = FileVerifierFactory.PptMimeType;

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Assert.False(verifier.IsValid(stream, file, mimeType), $"{file} validated as a .xls when it shouldn't have.");
            }
        }

        public void TestVerifier(String file, IFileVerifier verifier, String mimeType)
        {
            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Assert.True(verifier.IsValid(stream, file, mimeType), $"{file} did not validate as a {Path.GetExtension(file)}");
            }
        }
    }
}
