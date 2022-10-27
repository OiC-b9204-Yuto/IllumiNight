using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class FileManager
{
    public static void Save(string fileName, string data)
    {
        string path = Application.dataPath + "/" + fileName;
        using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
        {
            sw.WriteLine(data);
        }
    }
    public static string Load(string fileName,out bool result)
    {
        string readStr = "";
        string path = Application.dataPath + "/" + fileName;
        try
        {
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                readStr = sr.ReadToEnd();
            }
        }
        catch (FileNotFoundException)
        {
            result = false;
            return readStr;
        }
        result = true;
        return readStr;
    }
}
