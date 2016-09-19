using UnityEngine;
using System.Collections;
using System.Linq;

public class CSVUtil {

    public static string ToCSV(object[] fields)
    {
        var ws = from o in fields select o.ToString();
        return string.Join(", ", ws.ToArray<string>());
    }

}
