using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstController : MonoBehaviour, ISceneController
{

    SSDirector director;
    public SSActionManager actionManager { get; set; }
    public GUISkin skin;

    public enum BoatPosition { left, right };
    public BoatPosition boatPosition = BoatPosition.left;

    public enum GameState { win, fail, stop, start };
    public GameState state = GameState.stop;

    public List<GameObject> Left = new List<GameObject>();
    public List<GameObject> Right = new List<GameObject>();
    public List<GameObject> OnTheBoat = new List<GameObject>();

    public GameObject boat;
    public bool BoatMove = false;
    public GameObject PlayerMove = null;
    public GameObject pos1, pos2;
    void Awake()
    {
        director = SSDirector.GetInstance();
        director.CurrentSceneController = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadResources()
    {
        Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/River"),
            new Vector3(0, -4, 0), Quaternion.identity);
        Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ground"),
            new Vector3(-9, -3, 0), Quaternion.identity);
        Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ground"),
            new Vector3(9, -3, 0), Quaternion.identity);
        boat = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Boat"),
            new Vector3(-4, -1, 0), Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            Left.Add(Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Priest"),
            new Vector3(-12 + i, 0, 0), Quaternion.identity));
            Left[i].name = i.ToString();
        }
        for (int i = 0; i < 3; i++)
        {
            Left.Add(Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Devil"),
            new Vector3(-9 + i, 0, 0), Quaternion.identity));
            Left[i + 3].name = (i + 3).ToString();
        }
    }
    public void Pause()
    {

    }
    public void Resume()
    {

    }
    private void OnGUI()
    {
        GUI.skin = skin;
        if (state == GameState.stop)
        {
            if (GUI.Button(new Rect(Screen.width / 2-30, Screen.height / 2-30, 100, 100), "PLAY"))
            {
                state = GameState.start;
                director.CurrentSceneController.LoadResources();
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 - 150, 100, 100), "GO"))
            {
                if (OnTheBoat.Count != 0)
                {
                    BoatMove = true;
                }
            }
        }
        if (state == GameState.fail)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 50, 100, 100), "FAIL!");
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50, 100, 50), "RESTART"))
            {
                director.CurrentSceneController.Restart();
            }
        }
        else if (state == GameState.win)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 50, 160, 100), "WIN!");
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50, 100, 50), "RESTART"))
            {
                director.CurrentSceneController.Restart();
            }
        }
    }
    public void CheckState1(List<GameObject> direction)
    {
        int numOfDevils = 0;
        int numOfPriests = 0;
        for (int i = 0; i < direction.Count; i++)
        {
            if (direction[i].transform.tag == "priest")
            {
                numOfPriests++;
            }
            else if (direction[i].transform.tag == "devil")
            {
                numOfDevils++;
            }
        }
        if (numOfDevils > numOfPriests && numOfPriests != 0) state = GameState.fail;
    }
    public void CheckState2(List<GameObject> direction)
    {
        int numOfDevils = 0;
        int numOfPriests = 0;
        for (int i = 0; i < direction.Count; i++)
        {
            if (direction[i].transform.tag == "priest")
            {
                numOfPriests++;
            }
            else if (direction[i].transform.tag == "devil")
            {
                numOfDevils++;
            }
        }
        for (int i = 0; i < OnTheBoat.Count; i++)
        {
            if (OnTheBoat[i].transform.tag == "priest")
            {
                numOfPriests++;
            }
            else if (OnTheBoat[i].transform.tag == "devil")
            {
                numOfDevils++;
            }
        }
        if (numOfDevils > numOfPriests && numOfPriests != 0) state = GameState.fail;
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}