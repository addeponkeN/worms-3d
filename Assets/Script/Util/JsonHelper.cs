using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Util
{
    public static class JsonHelper
    {
        public static bool Save<T>(string filePath, T objectToWrite) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contents = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, false);
                writer.Write(contents);
            }
            catch(Exception e)
            {
                Debug.Log($"JsonSaveError: {e}");
                return false;
            }
            finally
            {
                if(writer != null)
                    writer.Close();
            }
            return true;
        }

        public static T Load<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var contents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(contents);
            }
            catch(JsonException e)
            {
                Debug.Log($"JsonLoadError: {e}");
                throw e;
            }
            finally
            {
                if(reader != null)
                    reader.Close();
            }
        }
    }
}