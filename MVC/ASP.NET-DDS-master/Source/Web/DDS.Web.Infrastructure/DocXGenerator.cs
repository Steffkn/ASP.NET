namespace DDS.Web.Infrastructure
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Novacode;

    public class DocXGenerator
    {
        public static void Generate(Dictionary<string, string> proparties)
        {
            // Modify to suit your machine:
            string fileName = @"F:\DocXExample.docx";

            //proparties = new Dictionary<string, string>();
            //proparties.Add("[USER_NAME]", "Стефан Николов Петров");
            //proparties.Add("[USER_FNUMBER]", "111210045");
            //proparties.Add("[USER_ADDRESS]", "гр. Кюстендил, кв. 'Запад', бл. 71, ап.21");
            //proparties.Add("[USER_TEL]", "0883332383");
            //proparties.Add("[USER_EMAIL]", "steff.kn@abv.bg");
            //proparties.Add("[DOC_TITLE]", "Very long title of diploma for making a system that makes diplomas. Amazing");
            //proparties.Add("[DOC_TECH]", "list of dechs");
            //proparties.Add("[DOC_BEGINDATE]", "01/2016");
            //proparties.Add("[DOC_ENDDATA]", "01/2017");

            // Create a document in memory:
            var doc = DocX.Create(fileName);
            doc.ApplyTemplate(@"F:\DiplomaTemplate.docx");

            foreach (var prop in proparties)
            {
                doc.ReplaceText(prop.Key, prop.Value);
            }

            // Save to the output directory:
            doc.Save();

            // Open in Word:
            Process.Start("WINWORD.EXE", fileName);
        }
    }
}
