using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class JSONHandler : MonoBehaviour
{
    /**
 * Saves the json string to a given filepath.
 * @param     json the json string.
 * @param     filepath the path to where to save.
 * @return    void
 */
    public static void SaveJson(string json, string filePath)
    {

        File.WriteAllText(Application.dataPath + filePath, json);

    }

    /**
* Creates MessageInformation elements from JSON (From Phase 3)
* @param     filepath the path to where to read the json data from.
* @return    MessageInformation
*/
    public static MessageInformation ReadJsonFromFile(string filePath)
    {

        string json = File.ReadAllText(Application.dataPath + filePath);
        //Debug.Log("Before" + json);
        string postprocessedjson = PostProcessJson(json);
        //Debug.Log("After" + postprocessedjson);
        MessageInformation mi = JsonUtility.FromJson<MessageInformation>(postprocessedjson);
        return mi;
    }


    /**
    * Reads the Content of the JSON File (From Phase 1)
    * @param     filepath the path to where to read the json data from.
    * @return    Content
    */
    public static Content ReadJsonContentFromFile(string filePath)
    {
        string json = File.ReadAllText(Application.dataPath + filePath);
        Content c = JsonUtility.FromJson<Content>(json);
        return c;
    }
    /**
    * Reads the MessageInformation of the JSON String (From Phase 3)
    * @param     json the json string to read from.
    * @return    MessageInformation
    */
    public static MessageInformation ReadJsonFromString(string json)
    {
        //Debug.Log("Before" + json);
        string postprocessedjson = PostProcessJson(json);
        //Debug.Log("After" + postprocessedjson);
        MessageInformation mi = JsonUtility.FromJson<MessageInformation>(postprocessedjson);
        return mi;
    }

    /**
   * Reads the Content of the JSON String (From Phase 1)
   * @param     filepath the json string to read from.
   * @return    Content
   */
    public static Content ReadJsonContentFromString(string json)
    {
        string postprocessedjson = PostProcessJson(json);
        Content c = JsonUtility.FromJson<Content>(postprocessedjson);
        return c;
    }

    /**
    * Replaces single quotes with double quotes -> post processes incoming json files.
    * @param     filepath the json string to read from.
    * @return    string
    */
    private static string PostProcessJson(string json)
    {

        string postprocessedjson = json.Replace("\'", "\"");
        int removeamount = 0;
        if (removeamount > 0)
        {
            postprocessedjson = postprocessedjson.Remove(0, removeamount);
            postprocessedjson = postprocessedjson.Remove(postprocessedjson.Length - removeamount);
        }

        return postprocessedjson;
    }


}
