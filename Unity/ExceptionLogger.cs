#define EXCEPTION_LOGGER
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine;
public class ExceptionLogger  {
    public static string LogException(Exception e, string prependToString, object reason )
    {
        string rv = "LOGGING FAILED";
        prependToString += " Exception Occured in Scene: " + SceneManager.GetActiveScene().name + "\n";
        DateTime now = DateTime.Now;
        string errorLogFileName = (Application.dataPath + "/Exceptions/");
        errorLogFileName += now.Year + "_" + now.Month + "_" + now.Day + "/" + reason.GetType() + "/";
        errorLogFileName = errorLogFileName.Replace("/", "\\");
        try
        {
            Directory.CreateDirectory(errorLogFileName);
            errorLogFileName += DateTime.Now.ToFileTime() + "_" + "(" + e.GetType() + ").txt";
            File.WriteAllText(errorLogFileName, prependToString + e.StackTrace);
            rv = errorLogFileName;
        }
        catch (Exception e2) { Debug.LogError("Could not create exceptions directory" + e2.GetType()); }
        return rv;
    }
}
