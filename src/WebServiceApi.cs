using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using Newtonsoft;
using TesterApi.Clases;
using Newtonsoft.Json.Linq; 

namespace TesterApi
{
    public class ApiServicios
    {
        public ApiServicios()
        {

        }

        private string Url = "https://wcfpruebas.facturoporti.com.mx/Timbrado/Servicios.svc/";
       // private string Url = "https://localhost:52860/Servicios.svc/";

        public string Timbrar(string datos)
        {
            string ruta = Url + "ApiTimbrarCFDI";
            string respuesta = string.Empty;

            JavaScriptSerializer JSON = new JavaScriptSerializer();

            WebClient webClient = new WebClient();
            byte[] resByte;
            byte[] reqString;
            String UUID = string.Empty;

            Console.WriteLine("Inicia proceso de Timbrado");
            Console.WriteLine("");

            try
            {
                reqString = Encoding.Default.GetBytes(datos);
                webClient.Headers["content-type"] = "application/json";
                webClient.Headers["accept-encoding"] = "gzip,deflate";
                webClient.Headers["user-agent"] = "Apache-HttpClient/4.1.1 (java 1.5)";

                resByte = webClient.UploadData(ruta, "POST", reqString);
                Stream responseStream = new Archivos().ConvertirByteToStream(resByte);
                StreamReader ReaderResponse = new StreamReader(responseStream, Encoding.UTF8);
                var resultado = ReaderResponse.ReadToEnd();

                Newtonsoft.Json.Linq.JObject cfdi = Newtonsoft.Json.Linq.JObject.Parse(resultado);

                UUID = cfdi["CFDITimbrado"]["Respuesta"]["UUID"].ToString();
                    
                Console.WriteLine(((Newtonsoft.Json.Linq.JContainer)cfdi.Root).First().ToString());
                Console.WriteLine("");
                Console.WriteLine("Dentro del Json de respuesta encontraras el timbre, cadena original, etc. verifica la documentacion para mas información");        

                webClient.Dispose();
            }
            catch (Exception ex)
            {
                ex = ex;
            }

            return UUID;
        }

        public void Cancelar(Cancelar cancelacion)
        {
            string ruta = Url + "ApiCancelarCFDI";
            string respuesta = string.Empty;

            JavaScriptSerializer JSON = new JavaScriptSerializer();

            WebClient webClient = new WebClient();
            byte[] resByte;
            byte[] reqString;

            Console.WriteLine("");
            Console.WriteLine("Inicia proceso de cancelacion");
            Console.WriteLine("");

            try
            {
                reqString = Encoding.Default.GetBytes(JSON.Serialize(cancelacion));
                webClient.Headers["content-type"] = "application/json";
                webClient.Headers["accept-encoding"] = "gzip,deflate";
                webClient.Headers["user-agent"] = "Apache-HttpClient/4.1.1 (java 1.5)";

                resByte = webClient.UploadData(ruta, "POST", reqString);
                Stream responseStream = new Archivos().ConvertirByteToStream(resByte);

                StreamReader ReaderResponse = new StreamReader(responseStream, Encoding.UTF8);
                var resultado = ReaderResponse.ReadToEnd();

                JObject cfdi = JObject.Parse(resultado);
         
                Console.WriteLine(cfdi["FoliosRespuesta"]);
                Console.WriteLine("");
                Console.WriteLine("Dentro de cada uuid está el detalle de la cancelacion mas información en la documentacion");

                Console.ReadKey();

                webClient.Dispose();
            }
            catch (Exception ex)
            {
                ex = ex;
            }
        }

    }

    public class LoginPeticionPrueba
    {
        public string Correo { get; set; }

        public string Password { get; set; }

        public string IdEmpresa { get; set; }
    }

    public class TokenRespuesta
    {
        public string Token { get; set; }
    }
}
