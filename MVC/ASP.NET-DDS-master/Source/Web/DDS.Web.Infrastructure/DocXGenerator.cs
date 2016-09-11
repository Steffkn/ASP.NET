namespace DDS.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Novacode;

    public class DocXGenerator
    {
        public static string Generate(Dictionary<string, string> proparties, string templateFilePath)
        {
            // Modify to suit your machine:
            //string fileName = @"F:\DocXExample.docx";

            // proparties = new Dictionary<string, string>();
            // proparties.Add("[USER_NAME]", "Стефан Николов Петров");
            // proparties.Add("[USER_FNUMBER]", "111210045");
            // proparties.Add("[USER_ADDRESS]", "гр. Кюстендил, кв. 'Запад', бл. 71, ап.21");
            // proparties.Add("[USER_TEL]", "0883332383");
            // proparties.Add("[USER_EMAIL]", "steff.kn@abv.bg");
            // proparties.Add("[DOC_TITLE]", "Very long title of diploma for making a system that makes diplomas. Amazing");
            // proparties.Add("[DOC_TECH]", "list of dechs");
            // proparties.Add("[DOC_BEGINDATE]", "01/2016");
            // proparties.Add("[DOC_ENDDATA]", "01/2017");

            // Create a document in memory:
            var newFilePath = templateFilePath.Replace("Templates\\DiplomaTemplate.docx", string.Format("{0}.docx", proparties["[USER_FNUMBER]"]));
            var doc = DocX.Create(newFilePath);

            doc.ApplyTemplate(templateFilePath);

            foreach (var prop in proparties)
            {
                doc.ReplaceText(prop.Key, prop.Value);
            }

            // Save to the output directory:
            //doc.SaveAs(templateFilePath.Replace("DiplomaTemplate.docx", "Yolo123.docx"));
            doc.Save();

            return newFilePath;
        }
    }
}
