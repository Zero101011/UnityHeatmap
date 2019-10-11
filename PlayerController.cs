using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance;
    
    private Vector3 playerSpos;
    private int level;
    public int lives = 3;
    public float speed;
    public float thrust;
    public int jumps;
    private int jumpsC;
    public bool isLanded;
    public bool isJumping;
    public Rigidbody playerRB;
    public Vector3 moveDir;
    public Vector3 deathPos;
    public TextAsset myDeathPoss;
    public BoxCollider bottom;
    Material m_Material;
    GameController controller;
    DataRecorder dataRecorder;

    // Use this for initialization
    void Start()
    {
        playerSpos = gameObject.transform.position;
        level = SceneManager.GetActiveScene().buildIndex;
        print("level = " + level);
        m_Material = GetComponent<Renderer>().material;
        m_Material.color = Color.blue;
        playerRB = GetComponent<Rigidbody>();
        controller = FindObjectOfType<GameController>();


    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        playerRB.AddForce(speed * Time.deltaTime, 0, 0, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("I collide");

        if (other.gameObject.tag != gameObject.tag && other.gameObject.tag != "Finish")
        {
            if (lives > 0)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                GetDeathPos();
                gameObject.transform.position = playerSpos;
                this.gameObject.tag = "blue";
                m_Material.color = Color.blue;
                lives--;
                WriteDeathPos();
                print("deded" + lives);
            }
        }
        //else if (other.gameObject.tag == "Finish" && level == 3)
        //{
        //    print("being triggered");
        //    //SceneManager.LoadScene(0, LoadSceneMode.Single);
        //}





        isLanded = true;
    }

    /*private void OnTriggerExit(Collider other)
    {
        
    }*/
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != gameObject.tag && lives > 0)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            gameObject.transform.position = playerSpos;
            this.gameObject.tag = "blue";
            m_Material.color = Color.blue;
            lives--;
            GetDeathPos();
            WriteDeathPos();
            print("you died at " + deathPos);

            print("deded" + lives);
        }
        else if (other.gameObject.tag != gameObject.tag && lives <= 0)
        {
            print("deded");
            lives = 3;
            OnGameOver();
            GetDeathPos();
            WriteDeathPos();
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isJumping == false)
        {
            //print("I no collide");
            isLanded = false;
        }
    }
    private void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0) && jumpsC > 0)
        {
            playerRB.AddForce(0, thrust, 0, ForceMode.Impulse);
            isJumping = true;
            jumpsC--;
        }

        if(Input.GetMouseButtonDown(1) && m_Material.color == Color.blue)
        {
            this.gameObject.tag = "green";
            m_Material.color = Color.green;
        }else if (Input.GetMouseButtonDown(1) && m_Material.color == Color.green)
        {
            this.gameObject.tag = "blue";
            m_Material.color = Color.blue;
        }
        


        if(isLanded == true)
        {
            isJumping = false;
            jumpsC = jumps; 
        }
        
    }

    public void GetDeathPos()
    {
        deathPos = this.gameObject.transform.position;
    }

    public void OnGameOver()
    {
        int deathLevel = SceneManager.GetActiveScene().buildIndex;
        float deathTime = Mathf.Round(Time.timeSinceLevelLoad);
        
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
        {
            { "death level ", deathLevel },
            { "death distance ", controller.dist },
            { "death time (s)", deathTime }
        });
        print("gameOverCalled");
    }

    public void WriteDeathPos()
    {
        //dataRecorder.appendToEnd(deathPos);

        ////DataRecorder mydata = new DataRecorder;
        ////mydata.appendToEnd()
        string path = "Assets/Resources/DeathPositions.txt";

        //Write death position vector to text file

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(deathPos.x + "," + deathPos.y + "," + deathPos.z);
        //writer.WriteLine(deathPos + ";");your version
        //writer.WriteLine(deathPos);
        writer.Close();

        ////Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset("Assets/Resources/DeathPositions");
        TextAsset asset = Resources.Load<TextAsset>("Assets/Resources/DeathPositions.txt");

        ////Print the text from the file
        print(myDeathPoss.text);
    }

}
