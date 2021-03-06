

using C3xPAWM.Models.Entities;
using C3xPAWM.Models.InputModel;
using SelectPdf;

namespace C3xPAWM.Customizations.PdfExporter
{
    public class PdfExport
    {
        
        
        public bool GeneratePdf(Pacco pacco, ApplicationUser utente, Negozio negozio){
            
            #region RenderHTML
            
           var myHtml = @"<html>
                <head>
                
                <title>Etichetta</title>
                <style>
                * {
                    
                    font-family: Arial, Helvetica, sans-serif;
                }

                body {
                    margin: 0;
                    font-family: Arial, Helvetica, sans-serif;
                }


                    

                    .container{
                        margin-top: 50px;
                        margin-left: 50px;
                        width: 450px;
                        height: 400px;
                        text-align: center;
                        align-items: center;
                    }

                    .content {
                        
                        background-color: rgb(248, 248, 248);
                        border-radius: 25px;
                        border:  2px solid #0004FF;
                        
                    }
                    
                    /* Style the footer */
                    .footer {
                        background-color: #0004FF;
                        color: white;
                        height: 70px; 

                    }

                    p{
                        margin-top: 0;
                        margin-bottom: 0;
                    }

                </style>
                </head>
                <body>
                    <div class='container'>

                       
                        <div class='content'>
                        <br>
                        <p>
                            <b> Mittente </b> <br>
                            Spedito da " + negozio.Nome + @",<br>
                            " +  pacco.Partenza + @" <br>
                        </p>
                        <br>
                        <p>
                            <b>Destinatario </b>  <br>
                            " + utente.FullName + @" <br>
                            " + pacco.Destinazione + @" <br>
                        </p>
                        <br>
                            <p style='font-style: italic; color: grey;'> Grazie per averci scelto. <br> Il team di C3 </p>
                        <br>
                        </div>
                </body>
                </html>";
           #endregion

            try
            {
                HtmlToPdf converter = new HtmlToPdf();
                PdfDocument doc = converter.ConvertHtmlString(myHtml);
                doc.Save("C:/C3/Etichetta"+utente.FullName+""+pacco.PaccoId+".pdf");
                doc.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
            

        }

    }
}