using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;


/*
 * this class is developed by for asdakjsd..sd.as.d
 */
public class DataRecorder : MonoBehaviour {
    public string m_path;
    public string m_filename = "GarenHeatmap";
    public PlayerController playerController;
    

	// Use this for initialization
	void Start () {
		
	}
    //open the file, empty contents save. Or create new file and replace old one. 
    public void clearFile() {
    }

    /*
     * This function will open the file using the path variable
     * and then adds whatever the user is sending to the beginning of the file
     * If you don't specify a seperator for this function, it will automatically uses ;
     */
    public bool addToStart(string _whatever,string _seperator = ";") {
        return true;
    }
    /*
     * This function will open the file using the path variable
     * and then adds whatever the user is sending to the end of the file
     * If you don't specify a seperator for this function, it will automatically uses ;
     * if the user doesn'tspecity whether to add timestamp. by default this funciton will add it as the last column for each line or before the seperator.
     */
    public bool appendToEnd(string _whatever, string _seperator = ";", bool _addTimeStape = true)
    {
        string path = "Assets/Resources/DeathPositions.txt";

        //Write death position vector to text file

        StreamWriter writer = new StreamWriter(path, true);
        //Write death position in the form of 3 individual floats with commas to split values & semicolon to break vector3's
        writer.WriteLine(playerController.deathPos.x + "," + playerController.deathPos.y + "," + playerController.deathPos.z + ";"); //writer.WriteLine(playerController.deathPos + ";"); /*<<optional variant for position grab>>*/
        writer.Close();

        ////Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset("Assets/Resources/DeathPositions");
        TextAsset asset = Resources.Load<TextAsset>("Assets/Resources/DeathPositions.txt");

        ////Print the text from the file
        print(playerController.myDeathPoss.text);
        return true;
    }
    public bool appendToEnd(Vector3 _pos, string _seperator = ";", bool _addTimeStape = true)
    {
        string path = "Assets/Resources/DeathPositions.txt";

        //Write death position vector to text file

        StreamWriter writer = new StreamWriter(path, true);
        //writer.WriteLine(deathPos.x + "," + deathPos.y + "," + deathPos.z + ";");
        writer.WriteLine(_pos + ";");
        writer.Close();

        ////Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset("Assets/Resources/DeathPositions");
        TextAsset asset = Resources.Load<TextAsset>("Assets/Resources/DeathPositions.txt");

        ////Print the text from the file
        print(playerController.myDeathPoss.text);
        return true;
    }
    public bool appendToEnd(Vector2 _pos, string _seperator = ";", bool _addTimeStape = true)
    {
        //string path = "Assets/Resources/DeathPositions.txt";

        ////Write death position vector to text file

        //StreamWriter writer = new StreamWriter(path, true);
        ////writer.WriteLine(deathPos.x + "," + deathPos.y + "," + deathPos.z + ";");
        //writer.WriteLine(deathPos + ";");
        //writer.Close();

        //////Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset("Assets/Resources/DeathPositions");
        //TextAsset asset = Resources.Load<TextAsset>("Assets/Resources/DeathPositions.txt");

        //////Print the text from the file
        //print(myDeathPoss.text);
        return true;
    }

    public bool createNewFile(string _name)
    {
        return true;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
