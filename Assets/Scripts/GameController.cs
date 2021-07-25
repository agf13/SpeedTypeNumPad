using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public AudioSource soundManager;
    public AudioClip backgroundMusic;

    private GameObject numPad;
    private GameObject keyboard;
    private Camera mainCamera;

    public float speed = 1f;
    public Color[] colors = new Color[4];
    private Color presentColor;
    private Color nextColor;
    private float startTime;
    private float endTime;
    private float time = 0.25f;
    private int whereIsSonic;
    private float cameraYPos1 = -0.53f;
    private float cameraYPos2 = -1.42f;
    private int gameType; //1 = numPab; 2 = keyboard;

    public float timeForPlay = 30f;
    private float timerStart;
    private bool gameStarted = false;


    struct Coord
    {
        internal float x;
        internal float y;
    }
    private Coord[] sonicPositions = new Coord[10];
    GameObject sonic;


    private GameObject[] squaresKeypad = new GameObject[10];
    private GameObject[] squaresKeyboard = new GameObject[26];
    private float numberOfSquaresKeypad = 9;
    private float numberOfSquaresKeyboard = 25;

    private GameObject timeCount;
    private GameObject score;
    private int scorePoints = 0;

    // Use this for initialization
    void Start()
    {
        //InitializeCoordSonic();
        numPad = GameObject.Find("NumPad");
        keyboard = GameObject.Find("Keyboard");
        mainCamera = Camera.main;
        InitializeSquaresNumpad();
        InitializeSquaresKeyboard();
        InitializeColors();
        startTime = Time.time;
        sonic = GameObject.Find("Sonic");
        score = GameObject.Find("Score");
        timeCount = GameObject.Find("Time");
        whereIsSonic = Random.Range(1, 10);
        MoveSonicToPosition(whereIsSonic);
        soundManager = GetComponent<AudioSource>();


        keyboard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ///
        if(gameStarted == false && Input.GetKey(KeyCode.S))
        {
            scorePoints = 0;
            timerStart = Time.time;
            gameStarted = true;
            score.GetComponent<TextMesh>().text = scorePoints.ToString();
            soundManager.clip = backgroundMusic;
            soundManager.Play();
        }
        if(gameStarted == true)
        {
            int timePassed = (int) (Time.time - timerStart);
            timeCount.GetComponent<TextMesh>().text = "Seconds passed: " + timePassed.ToString();
            if(timePassed == timeForPlay)
            {
                timeCount.GetComponent<TextMesh>().text = "Time's up! Press S to restart.";
                gameStarted = false;
                soundManager.Stop();
            }
        }
        ///
        endTime = Time.time;
        float t = (Time.time - startTime) * speed;
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(presentColor, nextColor, t);
        if (endTime - startTime >= time)
        {
            startTime = Time.time;
            int rnd = Random.Range(0, colors.Length);
            presentColor = gameObject.GetComponent<Renderer>().material.color;
            nextColor = colors[rnd];
        }


        if (gameType == 1)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                //ChangeSquareColor(1);
                VerifyAndChange(1);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                //ChangeSquareColor(2);
                VerifyAndChange(2);
            }
            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                //ChangeSquareColor(3);
                VerifyAndChange(3);
            }
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                //ChangeSquareColor(4);
                VerifyAndChange(4);
            }
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                //ChangeSquareColor(5);
                VerifyAndChange(5);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                //ChangeSquareColor(6);
                VerifyAndChange(6);
            }
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                //ChangeSquareColor(7);
                VerifyAndChange(7);
            }
            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                //ChangeSquareColor(8);
                VerifyAndChange(8);
            }
            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                //ChangeSquareColor(9);
                VerifyAndChange(9);
            }
        }
        if(gameType == 2)
        {

        }
    }

    void ChangeSquareColor(int square)
    {
        int rnd = Random.Range(1, colors.Length);
        Renderer obj = squaresKeypad[square].GetComponent<Renderer>();
        Color presentColor = obj.material.color;
        Color nextColor = colors[rnd];
        while (presentColor == nextColor)
        {
            rnd = Random.Range(1, colors.Length);
            nextColor = colors[rnd];
        }
        obj.material.color = colors[rnd];
    }

    //void InitializeCoordSonic()
    //{

    //    sonicPositions[1].x = 1;
    //    sonicPositions[1].y = 1;

    //    sonicPositions[2].x = 2;
    //    sonicPositions[2].y = 2;

    //    sonicPositions[3].x = 1;
    //    sonicPositions[3].y = 1;

    //    sonicPositions[4].x = 1;
    //    sonicPositions[4].y = 1;

    //    sonicPositions[5].x = 1;
    //    sonicPositions[5].y = 1;

    //    sonicPositions[6].x = 1;
    //    sonicPositions[6].y = 1;

    //    sonicPositions[7].x = -0.5f;
    //    sonicPositions[7].y = 3.234f;

    //    sonicPositions[8].x = 2.37f;
    //    sonicPositions[8].y = 1.44f;

    //    sonicPositions[9].x = 1;
    //    sonicPositions[9].y = 1;

    //}

    void InitializeSquaresNumpad()
    {
        for (int i = 1; i <= numberOfSquaresKeypad; i++)
        {
            squaresKeypad[i] = GameObject.Find("SquarePad_" + i).transform.GetChild(0).gameObject;
        }
        CameraPositioning();
        gameType = 1;
    }

    private void CameraPositioning()
    {
        var cameraPos = mainCamera.transform.position;
        cameraPos.y = cameraYPos1;
        mainCamera.transform.position = cameraPos;
    }

    void InitializeSquaresKeyboard()
    {
        for (int i = 0; i <= numberOfSquaresKeyboard; i++)
        {
            Debug.Log(GameObject.Find("SquareKeyboard (" + i + ")").transform.GetChild(0).gameObject);
            squaresKeyboard[i] = GameObject.Find("SquareKeyboard (" + i + ")").transform.GetChild(0).gameObject;
            //Debug.Log("Nu am gasit patratul  " + i);
        }
        var cameraPos = mainCamera.transform.position;
        cameraPos.y = cameraYPos1;
        mainCamera.transform.position = cameraPos;
        gameType = 2;
    }

    void InitializeColors()
    {
        colors[0] = Color.blue;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.yellow;
        //colors[4] = Color.white;
        //colors[5] = Color.cyan;
        //colors[6] = Color.magenta;
        //colors[7] = Color.black;
    }

    void MoveSonicToPosition(int n)
    {
        if (n >= 1 && n <= 9)
        {
            var pos = sonic.transform.position;
            pos.x = squaresKeypad[n].transform.position.x;
            pos.y = squaresKeypad[n].transform.position.y;
            sonic.transform.position = pos;
        }
        else
        {
            Debug.LogError("Ceva e dubios. Variabila n iese din range");
        }

    }

    void MoveSonicToPosition2(GameObject obj)
    {
        var pos = sonic.transform.position;
        pos.x = obj.transform.position.x;
        pos.y = obj.transform.position.y;
        sonic.transform.position = pos;

    }

    void VerifyAndChange(int n)
    {
        if(whereIsSonic == n && gameStarted == true)
        {
            while(whereIsSonic == n)
                whereIsSonic = Random.Range(1, 10);
            MoveSonicToPosition(whereIsSonic);
            ChangeSquareColor(n);
            scorePoints++;
            score.GetComponent<TextMesh>().text = scorePoints.ToString();
        }
        else if(gameStarted == true)
        {
            scorePoints--;
            score.GetComponent<TextMesh>().text = scorePoints.ToString();
        }
    }

    void VerifyAndChange2(int n)
    {

    }
}
