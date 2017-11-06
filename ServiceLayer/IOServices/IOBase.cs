using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using Camps.CommonLib;

namespace Comps.ServiceLayer.IOServices
{
    public interface IInOutBinaryService
    {
        StreamWriterEx StreamWriterEx { get; set; }
        FileStream CreateFile(string path);
        bool DeleteFile(string path);
        DirectoryInfo CreateFolder(string path);
        bool DeleteFolder(string path);
        string[] GetFiles(string path, string pattern, SearchOption searchOption);

    }

    public class InOutBinaryService : IInOutBinaryService
    {
        public InOutBinaryService()
        {
            StreamWriterEx = new StreamWriterEx();
        }
        public  StreamWriterEx StreamWriterEx { get; set; }


        public  FileStream CreateFile(string path)
        {

            try
            {

                return File.Create(path);


            }

            catch (ArgumentNullException ex)
            {

                throw new Exception("No path exists. ", ex);

            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("The user does not have the required permission to write in the selected path.", ex.InnerException);

            }
            catch (ArgumentException ex)
            {

                throw new Exception("The The path is a zero-length string, contains only white space, or contains one or more invalid character",  ex.InnerException);

            }

            catch (PathTooLongException ex)
            {

                throw new Exception("The selected file name is too long",  ex.InnerException);

            }
            catch (DirectoryNotFoundException ex)
            {

                throw new Exception("The specified path is invalid",  ex.InnerException);

            }
            catch (IOException ex)
            {

                throw new Exception("An I/O error occurred while creating the file. ",  ex.InnerException);

            }
            catch (NotSupportedException ex)
            {

                throw new Exception("The path has an invalid format. ",  ex.InnerException);

            }
            catch (Exception ex)
            {

                throw new Exception("The path is invalid. ", ex.InnerException);

            }
            return null;
        }
        public  bool DeleteFile(string path)
        {

            try
            {
                //if (ThisIsLocalPath(path))
                //{
                //    throw new WriteNetPathException("The user can't write in the network path");
                //}
                File.Delete(path);

                return true;
            }
            catch (ArgumentNullException ex)
            {
             
                throw new Exception("No path exists. ",  ex.InnerException);

            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("The user does not have the required permission to write in the selected path.", ex.InnerException);

            }
            catch (ArgumentException ex)
            {

                throw new Exception("The The path is a zero-length string, contains only white space, or contains one or more invalid character",  ex.InnerException);

            }

            catch (PathTooLongException ex)
            {

                throw new Exception("The selected file name is to long",  ex.InnerException);

            }
            catch (DirectoryNotFoundException ex)
            {

                throw new Exception("The specified path is invalid", ex.InnerException);

            }
            catch (IOException ex)
            {

                throw new Exception("An I/O error occurred while creating the file. ", ex.InnerException);

            }
            catch (NotSupportedException ex)
            {

                throw new Exception("The path has an invalid format. ",  ex.InnerException);

            }
            catch (Exception ex)
            {

                throw new Exception("The path has an invalid format. ", ex.InnerException);

            }
            return false;
        }

    
        public  DirectoryInfo CreateFolder(string path)
        {

            try
            {

                return Directory.CreateDirectory(path);


            }
            catch (ArgumentNullException ex)
            {

                throw new Exception("No path exists. ",  ex.InnerException);

            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("The user does not have the required permission to write in the selected path.",  ex.InnerException);

            }
            catch (ArgumentException ex)
            {

                throw new Exception("The path is a zero-length string, contains only white space, or contains one or more invalid character",  ex.InnerException);

            }

            catch (PathTooLongException ex)
            {

                throw new Exception("The selected file name is too long",  ex.InnerException);

            }
            catch (DirectoryNotFoundException ex)
            {

                throw new Exception("The specified path is invalid",  ex.InnerException);

            }
            catch (IOException ex)
            {

                throw new Exception("An I/O error occurred while creating the file. ",  ex.InnerException);

            }
            catch (NotSupportedException ex)
            {

                throw new Exception("The path has an invalid format. ", ex.InnerException);

            }
            return null;
        }
        public  bool DeleteFolder(string path)
        {

            try
            {

                Directory.Delete(path);
                return true;

            }

            catch (ArgumentNullException ex)
            {

                throw new Exception("No path exists. ",  ex.InnerException);

            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception("The user does not have the required permission to write in the selected path.",  ex.InnerException);

            }
            catch (ArgumentException ex)
            {

                throw new Exception("The The path is a zero-length string, contains only white space, or contains one or more invalid character",  ex.InnerException);

            }

            catch (PathTooLongException ex)
            {

                throw new Exception("The selected file name is too long",  ex.InnerException);

            }
            catch (DirectoryNotFoundException ex)
            {

                throw new Exception("The specified path is invalid",  ex.InnerException);

            }
            catch (IOException ex)
            {

                throw new Exception("An I/O error occurred while creating the file. ",  ex.InnerException);

            }
            catch (NotSupportedException ex)
            {

                throw new Exception("The path has an invalid format. ", ex.InnerException);

            }
            return false;
        }

        public string[] GetFiles(string path, string pattern, SearchOption searchOption)
        {
            
            var fullpath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(fullpath)) return new string[0];

            return Directory.GetFiles(fullpath, pattern, searchOption).Select(x=>path+"/"+Path.GetFileName(x)).ToArray(); 
        }
    }

  
}