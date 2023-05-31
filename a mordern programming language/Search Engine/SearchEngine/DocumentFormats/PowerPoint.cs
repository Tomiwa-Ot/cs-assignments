using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using SearchEngine.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using A = DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml;
using System.IO;
using System;

namespace SearchEngine.DocumentFormats
{
    /// <summary>
    /// Wrapper for powerpoint documents
    /// </summary>
    public class PowerPoint: Document
    {
        public PowerPoint(FileInfo file): base(file)
        {
            Types = new DocumentType[] { DocumentType.ppt, DocumentType.pptx };

            if (!IsFileExtensionAllowed())
                throw new InvalidFileTypeException(Types, FileExtension);
        }

        public override void Parse()
        {
            try
            {
                using (PresentationDocument ppt = PresentationDocument.Open(FilePath, false))
                {
                    PresentationPart presentationPart = ppt.PresentationPart;

                    // Get the relationship ID of the first slide.
                    PresentationPart part = ppt.PresentationPart;
                    OpenXmlElementList slideIds = part.Presentation.SlideIdList.ChildElements;

                    for (int i = 0; i < presentationPart.SlideParts.Count(); i++)
                    {
                        string relId = (slideIds[i] as SlideId).RelationshipId;

                        // Get the slide part from the relationship ID.
                        SlidePart slide = (SlidePart)part.GetPartById(relId);

                        // Build a StringBuilder object.
                        StringBuilder paragraphText = new StringBuilder();

                        // Get the inner text of the slide:
                        IEnumerable<A.Text> texts = slide.Slide.Descendants<A.Text>();
                        foreach (A.Text text in texts)
                        {
                            paragraphText.Append(text.Text);
                        }
                        FileContent += paragraphText.ToString();
                    }
                }

                base.Parse();
            }
            catch (Exception exception) { throw exception; }
        }
    }
}
