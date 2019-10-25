using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, Observer
{
    public Text scoreText;
    public Text infoText;
    private ScoreRecorder record;
    private UIController UI;
    private ObjectFactory fac;
    private float[] posx = { -5, 5, -5, 5 };
    private float[] posz = { -5, -5, 5, 5 };
    void Start()
    {
        record = new ScoreRecorder();
        record.scoreText = scoreText;
        UI = new UIController();
        UI.infoText = infoText;
        fac = Singleton<ObjectFactory>.Instance;

        Publish publisher = Publisher.getInstance();
        publisher.add(this);
        LoadResources();
    }

    private void LoadResources()
    {
        Instantiate(Resources.Load("prefabs/Ami"), new Vector3(2, 0, -2), Quaternion.Euler(new Vector3(0, 180, 0)));
        ObjectFactory factory = Singleton<ObjectFactory>.Instance;
        for (int i = 0; i < posx.Length; i++)
        {
            GameObject patrol = factory.setObjectOnPos(new Vector3(posx[i], 0, posz[i]), Quaternion.Euler(new Vector3(0, 180, 0)));
            patrol.name = "Patrol" + (i + 1);
        }
    }
    public void notified(ActorState state, int pos, GameObject actor)
    {
        if (state == ActorState.ENTER_AREA)
        {
            record.addScore(1);
        }
        else UI.loseGame();
    }
}
