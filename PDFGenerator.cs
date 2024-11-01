using System;
using System.IO;
using System.Text;

namespace HTML2PDF_v3
{
    public class PDFGenerator
    {
        private HTMLConverter _htmlConverter;

        public PDFGenerator()
        {
            _htmlConverter = new HTMLConverter();
        }

        // Adaptação para aceitar qualquer Stream
        public void GeneratePDF(string htmlContent, Stream outputStream)
        {
            string pdfText = _htmlConverter.ConvertHtmlToText(htmlContent);

            using (BinaryWriter writer = new BinaryWriter(outputStream, Encoding.UTF8, leaveOpen: true))
            {
                WritePDFHeader(writer);
                WritePDFPage(writer, pdfText);
                WritePDFTrailer(writer);
            }

            Console.WriteLine("PDF gerado com sucesso.");
        }

        private void WritePDFHeader(BinaryWriter writer)
        {
            writer.Write(Encoding.ASCII.GetBytes("%PDF-1.4\n"));
            writer.Write(Encoding.ASCII.GetBytes("1 0 obj\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Type /Catalog\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Pages 2 0 R\n"));
            writer.Write(Encoding.ASCII.GetBytes(">>\n"));
            writer.Write(Encoding.ASCII.GetBytes("endobj\n"));
        }

        private void WritePDFPage(BinaryWriter writer, string pdfText)
        {
            writer.Write(Encoding.ASCII.GetBytes("2 0 obj\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Type /Pages\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Kids [3 0 R]\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Count 1\n"));
            writer.Write(Encoding.ASCII.GetBytes(">>\n"));
            writer.Write(Encoding.ASCII.GetBytes("endobj\n"));

            writer.Write(Encoding.ASCII.GetBytes("3 0 obj\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Type /Page\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Parent 2 0 R\n"));
            writer.Write(Encoding.ASCII.GetBytes("/MediaBox [0 0 612 792]\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Contents 4 0 R\n"));
            writer.Write(Encoding.ASCII.GetBytes(">>\n"));
            writer.Write(Encoding.ASCII.GetBytes("endobj\n"));

            float yPosition = 700;
            float lineHeight = 14;
            StringBuilder contentStream = new StringBuilder();

            foreach (var line in pdfText.Split('\n'))
            {
                if (yPosition - lineHeight < 0)
                {
                    yPosition = 700;
                    contentStream.AppendLine("BT /F1 24 Tf 100 " + yPosition + " Td (Continued) Tj ET\n");
                }

                contentStream.AppendLine($"BT /F1 11 Tf 100 {yPosition} Td ({line}) Tj ET");
                yPosition -= lineHeight;
            }

            writer.Write(Encoding.ASCII.GetBytes("4 0 obj\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Length " + (contentStream.Length + 15) + " >>\n"));
            writer.Write(Encoding.ASCII.GetBytes("stream\n"));
            writer.Write(Encoding.Latin1.GetBytes(contentStream.ToString()));
            writer.Write(Encoding.ASCII.GetBytes("endstream\n"));
            writer.Write(Encoding.ASCII.GetBytes("endobj\n"));

            writer.Write(Encoding.ASCII.GetBytes("5 0 obj\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Type /Font\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Subtype /Type1\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Name /F1\n"));
            writer.Write(Encoding.ASCII.GetBytes("/BaseFont /Helvetica\n"));
            writer.Write(Encoding.ASCII.GetBytes(">>\n"));
            writer.Write(Encoding.ASCII.GetBytes("endobj\n"));
        }

        private void WritePDFTrailer(BinaryWriter writer)
        {
            writer.Write(Encoding.ASCII.GetBytes("xref\n"));
            writer.Write(Encoding.ASCII.GetBytes("0 6\n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000000 65535 f \n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000010 00000 n \n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000069 00000 n \n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000120 00000 n \n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000190 00000 n \n"));
            writer.Write(Encoding.ASCII.GetBytes("0000000270 00000 n \n"));
            writer.Write(Encoding.ASCII.GetBytes("trailer\n"));
            writer.Write(Encoding.ASCII.GetBytes("<< /Size 6\n"));
            writer.Write(Encoding.ASCII.GetBytes("/Root 1 0 R\n"));
            writer.Write(Encoding.ASCII.GetBytes(">>\n"));
            writer.Write(Encoding.ASCII.GetBytes("startxref\n"));
            writer.Write(Encoding.ASCII.GetBytes("310\n"));
            writer.Write(Encoding.ASCII.GetBytes("%%EOF\n"));
        }
    }
}
