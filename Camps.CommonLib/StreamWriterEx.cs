using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Camps.CommonLib
{
    public static class SimpleMapper
    {
        public static void PropertyMap<T, U>(T source, U destination)
            where T : class, new()
            where U : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().Where(x=>x.GetValue(source,null)!=null).ToList<PropertyInfo>();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList<PropertyInfo>();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null && !destinationProperty.IsPrimaryKey())
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
        }
    }

    public static class FindPrimaryKey
    {
        public static bool IsPrimaryKey(this PropertyInfo pi)
        {
            System.Object[] attributes = pi.GetCustomAttributes(true);
            foreach (object attribute in attributes)
            {
                if (attribute is EdmScalarPropertyAttribute)
                {
                    if ((attribute as EdmScalarPropertyAttribute).EntityKeyProperty )
                        return true;
                }
                else if (attribute is ColumnAttribute)
                {

                    if ((attribute as ColumnAttribute).IsPrimaryKey == true)
                        return true;
                }
                else if (attribute is KeyAttribute)
                {
                        return true;
                }
            }
            return false;
        }
    }
    public class StreamReaderEx
    {

    }
    public class StreamWriterEx
    {
        private StreamWriter _writeStream;
        public StreamWriterEx WriteStream(string path)
        {
            _writeStream = new StreamWriter(path);
          

            return this;
        }
        public StreamWriterEx WriteStream(string path, bool append)
        {
            _writeStream = new StreamWriter(path, append);

            return this;
        }
        public StreamWriterEx WriteLine(string content)
        {

            try
            {
                _writeStream.WriteLine(content);
                return this;

            }
            catch (ObjectDisposedException ex)
            {
                
                 throw new Exception("The TextWriter is closed.", ex.InnerException);
            }
            catch (IOException ex)
            {
              
                  throw new Exception("An I/O error occurs. ", ex.InnerException);
            }
        }
        public StreamWriterEx Write(string content)
        {

            try
            {
                _writeStream.Write(content);
                return this;

            }
            catch (ObjectDisposedException ex)
            {
                
                throw new Exception("The TextWriter is closed.", ex.InnerException);
            }
            catch (IOException ex)
            {
              
                throw new Exception("An I/O error occurs. ", ex.InnerException);
            }
        }
        public StreamWriterEx Close()
        {
            _writeStream.Close();
            return this;
        }
        public StreamWriterEx Dispose()
        {
            _writeStream.Dispose();
            return this;
        }
    }
}