using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController : MonoBehaviour, ISceneController
{

    SSDirector director;
    public GUISkin skin;

    enum BoatPosition { left, right };
    private BoatPosition boatPosition = BoatPosition.left;

    enum GameState { win, fail, stop, start };
    GameState state = GameState.stop;
    private List<GameObject> Left = new List<GameObject>();
    private List<GameObject> Right = new List<GameObject>();
    private List<GameObject> OnTheBoat = new List<GameObject>();

    private GameObject boat;
    private bool BoatMove = false;
    private GameObject PlayerMove = null;
    private GameObject pos1, pos2;
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
        if (state == GameState.fail || state == GameState.win) return;
        if (Input.GetMouseButtonDown(0) && !BoatMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "devil" || hit.collider.gameObject.tag == "priest")
                {
                    PlayerMove = hit.collider.gameObject;
                }
            }
        }
        if (PlayerMove != null && PlayerMove.transform.parent == null)
        {
            if (((boatPosition == BoatPosition.left && Left.Contains(PlayerMove)) ||
                (boatPosition == BoatPosition.right && Right.Contains(PlayerMove)))
                && OnTheBoat.Count < 2)
            {
                if (pos1 == null)
                {
                    PlayerMove.transform.position = boat.transform.position +
                        new Vector3(-0.5f, 1, 0);
                    pos1 = PlayerMove;
                }
                else
                {
                    PlayerMove.transform.position = boat.transform.position +
                        new Vector3(0.5f, 1, 0);
                    pos2 = PlayerMove;
                }
                PlayerMove.transform.parent = boat.transform;
                if (boatPosition == BoatPosition.left)
                {
                    Left.Remove(PlayerMove);
                }
                else if (boatPosition == BoatPosition.right)
                {
                    Right.Remove(PlayerMove);
                }
                OnTheBoat.Add(PlayerMove);
            }
            PlayerMove = null;
        }
        if (PlayerMove != null && PlayerMove.transform.parent != null)
        {
            if (boatPosition == BoatPosition.right)
            {
                PlayerMove.transform.position = new Vector3(5.5f + Convert.ToInt32(PlayerMove.name), 0, 0);
                PlayerMove.transform.parent = null;
                Right.Add(PlayerMove);
                if (Right.Count == 6) state = GameState.win;
            }
            else if (boatPosition == BoatPosition.left)
            {
                PlayerMove.transform.position = new Vector3(-11 + Convert.ToInt32(PlayerMove.name), 0, 0);
                PlayerMove.transform.parent = null;
                Left.Add(PlayerMove);
            }
            if (pos1 == PlayerMove) pos1 = null;
            else pos2 = null;
            OnTheBoat.Remove(PlayerMove);
            PlayerMove = null;
        }
        if (BoatMove)
        {
            if (boatPosition == BoatPosition.right)
            {
                CheckState1(Right);
                boat.transform.Translate(Vector3.left * Time.deltaTime * 3);
                if (boat.transform.position.x <= -4)
                {
                    BoatMove = false;
                    boatPosition = BoatPosition.left;
                    CheckState2(Left);
                }
            }
            if (boatPosition == BoatPosition.left)
            {
                CheckState1(Left);
                boat.transform.Translate(Vector3.right * Time.deltaTime * 3);
                if (boat.transform.position.x >= 4)
                {
                    BoatMove = false;
                    boatPosition = BoatPosition.right;
                    CheckState2(Right);
                }
            }
        }

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
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "PLAY"))
            {
                state = GameState.start;
                director.CurrentSceneController.LoadResources();
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 25, Screen.height / 2 - 150, 100, 100), "GO"))
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
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 - 50, 100, 100), "WIN!");
            if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2 + 50, 100, 50), "RESTART"))
            {
                director.CurrentSceneController.Restart();
            }
        }
    }
    private void CheckState1(List<GameObject> direction)
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
    private void CheckState2(List<GameObject> direction)
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