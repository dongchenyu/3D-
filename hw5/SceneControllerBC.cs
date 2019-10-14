using UnityEngine;
using System.Collections;
using Com.Mygame;
public class SceneControllerBC : MonoBehaviour
{
    private Color color;
    private Vector3 emitPos;
    private Vector3 emitDir;
    private float speed;

    void Awake()
    {
        SceneController.getInstance().setSceneControllerBC(this);
    }
    public int index = 0;
    public void loadRoundData(int round)
    {
        switch (round)
        {
            case 1:    
                color = Color.yellow;
                if (index % 2 == 0)
                {
                    emitPos = new Vector3(-3f, 0f, -5f);
                    emitDir = new Vector3(25f, 45f, 60f);
                }
                else
                {
                    emitPos = new Vector3(3f, 0f, -5f);
                    emitDir = new Vector3(-25f, 40f, 60f);
                }
                index++;
                speed = 3;
                SceneController.getInstance().getGameModel().setting(color, emitPos, emitDir.normalized, speed, 1);
                break;
            case 2:     
                color = Color.red;
                if (index % 2 == 0)
                {
                    emitPos = new Vector3(-3f, 0f, -5f);
                    emitDir = new Vector3(25f, 45f, 60f);
                }
                else
                {
                    emitPos = new Vector3(3f, 0f, -5f);
                    emitDir = new Vector3(-25f, 40f, 60f);
                }
                index++;
                speed = 4;
                SceneController.getInstance().getGameModel().setting(color, emitPos, emitDir.normalized, speed, 1);
                break;
            case 3:    
                color = Color.blue;
                if (index % 2 == 0)
                {
                    emitPos = new Vector3(-3f, 0f, -5f);
                    emitDir = new Vector3(25f, 45f, 60f);
                }
                else
                {
                    emitPos = new Vector3(3f, 0f, -5f);
                    emitDir = new Vector3(-25f, 40f, 60f);
                }
                index++;
                speed = 5;
                SceneController.getInstance().getGameModel().setting(color, emitPos, emitDir.normalized, speed, 1);
                break;
        }
    }
}