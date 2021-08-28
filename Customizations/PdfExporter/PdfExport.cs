

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
                    box-sizing: border-box;
                    font-family: Arial, Helvetica, sans-serif;
                }

                body {
                    margin: 0;
                    font-family: Arial, Helvetica, sans-serif;
                }


                    header{
                    background-color: #0004FF;
                    color: white;
                    padding: 10px; 
                    }

                    .container{
                    padding: 10px;
                        width: 600px;
                        height: 400px;
                        text-align: center;
                    align-items: center;
                    }

                    .content {
                        
                        border-left: 2px solid #0004FF;
                        border-right: 2px solid #0004FF;
                        border-bottom: 2px solid #0004FF;
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

                    img{
                        height: 70px;
                        width: 70px;
                    }
                </style>
                </head>
                <body>
                    <div class='container'>

                        <header>
                            <h3> C3 </h3>
                        </header>
                    
                        <div class='content'>
                        <br>
                        <p>
                            Spedito da " + negozio.Nome + @",<br>
                            " +  pacco.Partenza + @" <br>
                            ITALIA
                        </p>
                        <br>
                        <hr>
                        <br>
                       
                        <p>
                            " + utente.FullName + @" <br>
                            " + pacco.Destinazione + @" <br>
                            ITALIA
                        </p>
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