using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterApi.Clases;
using Newtonsoft; 

namespace TesterApi
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiServicios api = new ApiServicios();
            Archivos archivo = new Archivos();

            // Abre el archivo de Factura
            // Antes de mandar a timbrar se debe de cambiar la fecha de la factura respetando el formato
            String cfdi = archivo.ConvertirStreamToStringUTF8(archivo.Abrir(ObtieneDirectorioAplicacion() + @"\Json\Factura.json"));
            string uuid = api.Timbrar(cfdi);

            //Ejemplo de la cancelacion
            Cancelar cancelacion = new Cancelar();
            cancelacion.RFC = "AAA010101AAA";
            cancelacion.Usuario = "PruebasTimbrado";
            cancelacion.Password = "@Notiene1";

            String pfx = archivo.ConvertirByteToBase64(archivo.ConvertirStreamToByte(archivo.Abrir(ObtieneDirectorioAplicacion() + @"\Certificado\AAA010101AAA.pfx")));
            
            cancelacion.PFX = pfx;
            cancelacion.PFXPassword = "12345678a";

            cancelacion.UUIDs = new List<string>();
            cancelacion.UUIDs.Add(uuid);

            api.Cancelar(cancelacion);

        }

        private static string ObtieneDirectorioAplicacion()
        {
            string cmdLine = Environment.CommandLine;
            string DirectorioInstalacionAplicacion = string.Empty;

            try
            {
                cmdLine = cmdLine.Replace("console", " ");
                cmdLine = cmdLine.Replace("\"", "");
                DirectorioInstalacionAplicacion = Path.GetDirectoryName(cmdLine);

                int indice = DirectorioInstalacionAplicacion.ToUpper().IndexOf("BIN");
                DirectorioInstalacionAplicacion = DirectorioInstalacionAplicacion.Substring(0, indice - 1);
            }
            catch (Exception)
            {
                cmdLine = cmdLine.Replace("console", " ");
                cmdLine = cmdLine.Replace("\"", "");
                DirectorioInstalacionAplicacion = Path.GetDirectoryName(cmdLine);
            }

            return DirectorioInstalacionAplicacion;
        }

    }
}
