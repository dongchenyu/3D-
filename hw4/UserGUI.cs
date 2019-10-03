using UnityEngine;
using System.Collections;
using Com.HitGame;

public class UserGUI : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }
    int clicked = 0;

    void OnGUI()
    {
        GUIStyle Mystyle = new GUIStyle();
        Mystyle.fontSize = 40;
        Mystyle.normal.background = null;
        GUI.skin.button.fontSize = 40;
        if (clicked == 0)
        {
            if (GUI.Button(new Rect(700,200,200,200), "Start"))
            {
                myFactory.GetInstance().Round = 1;
                clicked = 1;
            }
        }
        string round = "Round: " + myFactory.GetInstance().Round.ToString();
        GUI.Label(new Rect(50, 50, 200, 100), round, Mystyle);
        string score = "Score: " + myFactory.GetInstance().Score.ToString();
        GUI.Label(new Rect(50, 200, 200, 100), score, Mystyle);
        string state;
        if (myFactory.GetInstance().GameState == 3)
        {
            state = "GameOver !";
            GUI.Label(new Rect(600, 300, 200, 200), state, Mystyle);
        }
        string Lose = "LostUFO: " + myFactory.GetInstance().LoseNum.ToString();
        GUI.Label(new Rect(50, 350, 200, 100), Lose, Mystyle);
        if (myFactory.GetInstance().Score == 30)
        {
            GUI.Button(new Rect(700, 200, 200, 200), "All Killed");
         }
            
    }

}

