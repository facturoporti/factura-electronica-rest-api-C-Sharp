using System;
using System.Web;
using System.Text;
using System.Collections;
using System.IO;
using System.IO.Compression;

using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace TesterApi
{
    /// <summary>
    /// Clase que permite realizar operaciones basicas y comunes 
    /// del manejo de archivos como son: salvar, eliminar 
    /// abrir archivos a una ruta especifica
    /// </summary>
    public class Archivos
    {
        public bool resultado { get; set; }
        /// <summary>
        /// Crea una instancia de Modulo
        /// </summary>
        public Archivos()
        {
        }

        /// <summary>
        /// Libera los recursos de la memoria
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Es el destructor de la clase
        /// </summary>
        ~Archivos()
        {
            this.Dispose();
        }

        #region "Metodos Privados" 

        #endregion "Metodos Privados" 

        #region "Metodos Publicos" 
   
        /// <summary>
        /// Abrir un archivo de una ruta especifica en forma de Strea,
        /// </summary>
        /// <param name="ruta">Es la ruta de los archivos </param>
        /// <returns></returns>
        public FileStream Abrir(string ruta)
        {
            FileStream resultado = null;
            
            try
            {
                resultado = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Delete);
            }
            catch (Exception ex)
            {
                ////LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        public String AbrirBase64(string ruta)
        {
            String file = string.Empty; 

            try
            {
                file = Convert.ToBase64String(File.ReadAllBytes(ruta));                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(string contenido, string ruta)
        {
            bool resultado = false;

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    StreamWriter tmpArchivo = new StreamWriter(fs);

                    tmpArchivo.Write(contenido);
                    tmpArchivo.Flush();
                    tmpArchivo.Close();
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                ////LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(Stream archivo, string ruta)
        {
            bool resultado = false;

            try
            {
                long tamaño = archivo.Length;
                byte[] archivoBytes = new byte[tamaño];
                
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    archivo.Read(archivoBytes, 0, archivoBytes.Length);
                    fs.Write(archivoBytes, 0, archivoBytes.Length);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                ////LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }


        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public bool Guardar(byte[] archivo, string ruta)
        {
            bool resultado = false;

            try
            {
                using (FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
                {
                    fs.Write(archivo, 0, archivo.Length);
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                ////LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return resultado;
        }

        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public byte[] ConvertirStreamToByte(Stream archivo)
        {
            byte[] archivoBytes = null;

            try
            {
                archivoBytes = new byte[archivo.Length];
                archivo.Read(archivoBytes, 0, archivoBytes.Length);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI,  enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return archivoBytes;
        }

        public string ConvertirStreamToString(Stream archivo, Encoding encoding)
        {
            string respuesta = string.Empty;

            try
            {
                var archivoBytes = new byte[archivo.Length];
                archivo.Read(archivoBytes, 0, archivoBytes.Length);
                respuesta = encoding.GetString(archivoBytes);                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return respuesta;
        }

        public byte[] ConvertirBase64ToByte(String file)
        {
            byte[] archivoBytes = null;

            try
            {
                archivoBytes = Convert.FromBase64String(file);                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return archivoBytes;
        }

        public String ConvertirStringToBase64(string valor)
        {
            String file = string.Empty;

            try
            {
                file = Convert.ToBase64String(Encoding.UTF8.GetBytes(valor));
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        public String ConvertirBase64ToString(string valor)
        {
            String file = string.Empty;

            try
            {
                byte[] data = System.Convert.FromBase64String(valor);
                file = Encoding.ASCII.GetString(data);                
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        public String ConvertirByteToBase64(byte[] valor)
        {
            String file = string.Empty;

            try
            {
                file = Convert.ToBase64String(valor);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        public Stream ConvertirByteToStream(byte[] valor)
        {
            Stream file = new MemoryStream();

            try
            {
                file = new MemoryStream(valor);
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return file;
        }

        /// <summary>
        /// Guarda un archivo en una ubicacion que se especifica como parametro
        /// La condicion es que debe de tener permisos el usuario de asp net para poderlo realizar
        /// </summary>
        public byte[] ConvertirStreamToByteCloseFile(Stream archivo)
        {
            byte[] archivoBytes = null;

            try
            {
                archivoBytes = new byte[archivo.Length];
                archivo.Read(archivoBytes, 0, archivoBytes.Length);
                archivo.Close();
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            return archivoBytes;
        }

        public string ConvertirStreamToStringUTF8(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public Stream ConvertirStringToStreamUTF8(string fuente)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(fuente);
            return new MemoryStream(byteArray);
        }


        public Stream ConvertirBase64ToStream(string fuente)
        {
            byte[] byteArray = Convert.FromBase64String(fuente);
            return new MemoryStream(byteArray);
        }

        public byte[] ObtieneBytes(string fileName)
        {
            byte[] buffer = null;

            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, System.IO.FileAccess.Read);
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Close();
            }
            catch (Exception ex)
            {
                //LoggingBlock.RegistraEvento(enumTipoArchivo.UI, enumServicios.FrameWork, enumActividad.Generica, enumPrioridad.Media, ex.Message + " - " + ex.StackTrace, enumCategoria.Error);
            }
            
            return buffer;
        }

        #endregion
    }
    
}
