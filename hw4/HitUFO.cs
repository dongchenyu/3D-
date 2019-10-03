using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.HitGame;

namespace Com.HitGame
{
    public class myFactory : System.Object
    {
        public int Round = 0; 
        public int Isbegin = 0; 
        public int Score = 0;
        public int useUFO = 0;
        public int GameState = 1;
        public int LoseNum = 0;
        public List<GameObject> UFOs = new List<GameObject>();
        private static myFactory _instance;
        public static myFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new myFactory();
            }
            return _instance;
        }

        public void launchUFO(float g, float delaytime, Vector3 speed, Color color,
            Vector3 size, Vector3 position)
        {
            if (UFOs.Count == 0)
            {
                GameObject UFO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                waittolanch wt = UFO.AddComponent<waittolanch>();
                wt.setting(g, UFO, UFOs, delaytime, speed, color, size, position);
            }
            else
            {
                GameObject UFO = UFOs[0];
                UFOs.RemoveAt(0);
                waittolanch wt = UFO.GetComponent<waittolanch>();
                wt.setting(g, UFO, UFOs, delaytime, speed, color, size, position);
            }
        }

        public class waittolanch : MonoBehaviour
        {
            public List<GameObject> UFOs;
            public GameObject UFO;
            public Camera cm;
            public float delaytime;
            public Vector3 speed;
            public float g;
            public Color color;
            public Vector3 size;
            public Vector3 position;
            public int state = 0;
            public void setting(float g, GameObject UFO, List<GameObject> UFOs, float delaytime,
                Vector3 speed, Color color, Vector3 size, Vector3 position)
            {
                this.UFO = UFO;
                this.UFOs = UFOs;
                this.delaytime = delaytime;
                this.speed = speed;
                this.g = g;
                this.color = color;
                this.size = size;
                this.position = position;
                StartCoroutine(launch());
            }

            public IEnumerator launch()
            {
                yield return new WaitForSeconds(delaytime);
                this.transform.position = position;
                this.transform.localScale = size;
                this.GetComponent<Renderer>().material.color = color;
                state = 1;
            }

            void Update()
            {
                if (state == 1)
                {
                    speed = new Vector3(speed.x, speed.y - g, speed.z);
                    this.transform.position = this.transform.position + speed;
                    if (this.transform.position.y <= 0f)
                    {
                        myFactory.GetInstance().LoseNum++;
                        this.transform.position = new Vector3(0f, 0f, -2f);
                        state = 0;
                        UFOs.Add(UFO);
                        myFactory.GetInstance().useUFO++;

                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mp = Input.mousePosition;
                    cm = Camera.main;
                    Ray ray = cm.ScreenPointToRay(mp);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == this.gameObject)
                        {

                            this.transform.position = new Vector3(0f, 0f, -2f);
                            state = 0;
                            UFOs.Add(UFO);
                            if (myFactory.GetInstance().Round == 1)
                            {
                                myFactory.GetInstance().Score++;
                                myFactory.GetInstance().useUFO++;
                            }
                            else if (myFactory.GetInstance().Round == 2)
                            {
                                myFactory.GetInstance().Score += 2;
                                myFactory.GetInstance().useUFO++;
                            }
                            else if (myFactory.GetInstance().Round == 3)
                            {
                                myFactory.GetInstance().Score += 3;
                                myFactory.GetInstance().useUFO++;
                            }

                        }
                    }
                }
            }

        }
    }
}

public class HitUFO : MonoBehaviour
{
    public Vector3 camera_pos;
    public Quaternion camera_qua;
    public Camera cm;
    public Color whiteColor;
    public Color blueColor;
    public Color RedColor;
    public Vector3 whiteSize;
    public Vector3 blueSize;
    public Vector3 redSize;
    public Vector3 whiteSpeed;
    public Vector3 blueSpeed;
    public Vector3 redSpeed;
    public Vector3 Move;
    public float g;
    public Vector3 leftposition;
    public Vector3 rightposition;
    void Start()
    {
        camera_pos = new Vector3(0f, 0f, 0f);
        camera_qua = Quaternion.Euler(340f, 0f, 0f);
        cm = Camera.main;
        cm.transform.position = camera_pos;
        cm.transform.rotation = camera_qua;
        whiteColor = new Color(0.9f, 0.9f, 0.9f);
        blueColor = new Color(0f, 0f, 0.7f);
        RedColor = new Color(0.7f, 0f, 0f);
        whiteSize = new Vector3(1.5f, 0.5f, 1.5f);
        blueSize = new Vector3(1f, 0.3f, 1f);
        redSize = new Vector3(0.8f, 0.2f, 0.8f);
        whiteSpeed = new Vector3(0f, 0.1f, 0.25f);
        blueSpeed = new Vector3(0f, 0.2f, 0.35f);
        redSpeed = new Vector3(0.1f, 0.2f, 0.5f);
        Move = new Vector3(0.05f, 0f, 0f);
        g = 0.001f;
        leftposition = new Vector3(-1f, 0f, 0f);
        rightposition = new Vector3(1f, 0f, 0f);
    }
    void Update()
    {
        if (myFactory.GetInstance().LoseNum >= 3)
        {
            myFactory.GetInstance().GameState = 3;
        }

        if (myFactory.GetInstance().Round == 1 && myFactory.GetInstance().Isbegin == 0 && myFactory.GetInstance().GameState != 3)
        {
            myFactory.GetInstance().Isbegin = 1;
            myFactory.GetInstance().GameState = 2;
            Invoke("Round1", 0f);
        }

        if (myFactory.GetInstance().Round == 1 && myFactory.GetInstance().useUFO == 5 && myFactory.GetInstance().GameState == 2)
        {
            myFactory.GetInstance().Round = 2;
            myFactory.GetInstance().useUFO = 0;
            Invoke("Round2", 1f);
        }

        if (myFactory.GetInstance().Round == 2 && myFactory.GetInstance().useUFO == 5 && myFactory.GetInstance().GameState == 2)
        {
            myFactory.GetInstance().Round = 3;
            myFactory.GetInstance().useUFO = 0;
            Invoke("Round3", 1f);
        }


    }

    void Round1()
    {
        for (int i = 1; i <= 5; i++)
        {
            float time = i;
            float Randkey = Random.Range(-5f, 5f);
            if (i % 2 == 0)
            {
                myFactory.GetInstance().launchUFO(g, time * 2, whiteSpeed + Move * Randkey, whiteColor, whiteSize, leftposition);

            }
            else
            {
                myFactory.GetInstance().launchUFO(g, time * 2, whiteSpeed - Move * Randkey, whiteColor, whiteSize, rightposition);

            }
        }
    }

    void Round2()
    {
        for (int i = 1; i <= 5; i++)
        {
            float time = i;
            float Randkey = Random.Range(-5f, 5f);
            if (i % 2 == 0)
            {
                myFactory.GetInstance().launchUFO(g, time * 2, blueSpeed + Move * Randkey, blueColor, blueSize, leftposition);

            }
            else
            {
                myFactory.GetInstance().launchUFO(g, time * 2, blueSpeed - Move * Randkey, blueColor, blueSize, rightposition);

            }
        }
    }

    void Round3()
    {
        for (int i = 1; i <= 5; i++)
        {
            float time = i;
            float Randkey = Random.Range(-5f, 5f);
            if (i % 2 == 0)
            {
                myFactory.GetInstance().launchUFO(g, time * 2, redSpeed + Move * Randkey, RedColor, redSize, leftposition);

            }
            else
            {
                myFactory.GetInstance().launchUFO(g, time * 2, redSpeed + Move * Randkey, RedColor, redSize, rightposition);

            }
        }
    }
}