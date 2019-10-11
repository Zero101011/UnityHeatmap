using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Heatmap : MonoBehaviour
{
    private static List<Vector3> m_deathPositions = new List<Vector3>();
    private static GameObject heatmapPrefab;
    // Use this for initialization
    void Start()
    {
        /*
         * Check whether heatmap game object exists, if not, create it.
         * then add everything under that and then allow the user to turn them on and off by just switching the rendering of the parent game object. 
         * check if they have the special prefab, if not make it. 
         */
    }

    // Update is called once per frame
    void Update()
    {

    }

    //This will collect death position data, re-convert it into a vector 3 format and spawn transparent blocks on death positions recorded via PlayerController 

    //NOTE: THIS MAY BREAK WHEN MOVING BETWEEN LEVELS, NEED TO INTRODUCE LEVEL BASED HEATMAP TXT FILES
    [MenuItem("Tools/Heatmap/Generate")]
    static void ReadDeathData()
    {
        heatmapPrefab = (GameObject)Resources.Load("Prefabs/hMap_Sphere", typeof(GameObject));
        string path = "Assets/Resources/DeathPositions.txt";

        //Read the text from directly from the txt file
        StreamReader reader = new StreamReader(path);
        string deathCoords = "";
        while ((deathCoords = reader.ReadLine()) != null) {
            m_deathPositions.Add(stringToVec(deathCoords));
            deathCoords = "";
        }
        reader.Close();
        renderDeathData();
        //USE THIS TO CONVERT THE STRING ARRAY OF VECTOR3 INTO ACTUAL VECTOR 3
        //https://answers.unity.com/questions/1134997/string-to-vector3.html
    }

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3

        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        print("this is the result " + result);
        return result;
        //m_deathPositions.Add(/*your vector here*/);
    }
    public static Vector3 stringToVec(string _st)
    {
        Vector3 result = new Vector3();
        string[] vals = _st.Split(',');
        print(vals.Length + " !!!!  " + vals[0] + " st is : " + _st);
        if (vals.Length == 3)
        {
            result.Set(float.Parse(vals[0]), float.Parse(vals[1]), float.Parse(vals[2]));
        }
        return result;
    }

    public static void renderDeathData()
    {
        foreach (Vector3 deathPos in m_deathPositions) {
            Instantiate(heatmapPrefab, deathPos, Quaternion.identity);
        }
    }

    [MenuItem("Tools/Heatmap/Clear")]
    public static void destroyHeatmapObjects()
    {
        foreach (Vector3 deathPos in m_deathPositions)
        {
            GameObject[] hMap_Spheres = GameObject.FindGameObjectsWithTag("heatmap");
            for (int i = 0; i < hMap_Spheres.Length; i++)
            {
                GameObject.DestroyImmediate(hMap_Spheres[i]);
            }
        }
    }
}
