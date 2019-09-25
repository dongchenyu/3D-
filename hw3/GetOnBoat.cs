using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOnBoat : SSAction
{

    public FirstController SceneController;
    public static GetOnBoat GetSSAction()
    {
        GetOnBoat action = ScriptableObject.CreateInstance<GetOnBoat>();
        return action;
    }
    // Use this for initialization
    public override void Start()
    {
        SceneController = (FirstController)SSDirector.GetInstance().CurrentSceneController;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (((SceneController.boatPosition == FirstController.BoatPosition.left &&
            SceneController.Left.Contains(SceneController.PlayerMove)) ||
            (SceneController.boatPosition == FirstController.BoatPosition.right &&
            SceneController.Right.Contains(SceneController.PlayerMove)))
                && SceneController.OnTheBoat.Count < 2)
        {
            if (SceneController.pos1 == null)
            {
                SceneController.PlayerMove.transform.position = SceneController.boat.transform.position +
                    new Vector3(-0.5f, 1, 0);
                SceneController.pos1 = SceneController.PlayerMove;
            }
            else
            {
                SceneController.PlayerMove.transform.position = SceneController.boat.transform.position +
                    new Vector3(0.5f, 1, 0);
                SceneController.pos2 = SceneController.PlayerMove;
            }
            SceneController.PlayerMove.transform.parent = SceneController.boat.transform;
            if (SceneController.boatPosition == FirstController.BoatPosition.left)
            {
                SceneController.Left.Remove(SceneController.PlayerMove);
            }
            else if (SceneController.boatPosition == FirstController.BoatPosition.right)
            {
                SceneController.Right.Remove(SceneController.PlayerMove);
            }
            SceneController.OnTheBoat.Add(SceneController.PlayerMove);
        }
        SceneController.PlayerMove = null;
        this.destroy = true;
        this.callback.SSActionEvent(this);
    }
}