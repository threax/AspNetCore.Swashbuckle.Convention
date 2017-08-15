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
            var verifier = FileVerifierFactory.CreatePdfVerifier();
            var file = "TestFiles/Pdf.pdf";

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Assert.True(verifier.IsValid(stream, file, "application/pdf"), $"{file} did not validate as a pdf");
            }
        }
    }
}
