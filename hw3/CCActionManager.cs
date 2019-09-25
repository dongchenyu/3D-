using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{

    public FirstController SceneController;
    public GetOffBoat GetOff;
    public GetOnBoat GetOn;
    public BoatMoving Moving;

    // Use this for initialization
    protected void Start()
    {
        SceneController = (FirstController)SSDirector.GetInstance().CurrentSceneController;
    }

    // Update is called once per frame
    protected new void Update()
    {
        if (SceneController.state == FirstController.GameState.fail ||
            SceneController.state == FirstController.GameState.win) return;

        if (Input.GetMouseButtonDown(0) && !SceneController.BoatMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "devil" || hit.collider.gameObject.tag == "priest")
                {
                    SceneController.PlayerMove = hit.collider.gameObject;
                }
            }
        }
        if (SceneController.PlayerMove != null && SceneController.PlayerMove.transform.parent == null)
        {
            GetOn = GetOnBoat.GetSSAction();
            this.RunAction(SceneController.PlayerMove, GetOn, this);
        }
        if (SceneController.PlayerMove != null && SceneController.PlayerMove.transform.parent != null)
        {
            GetOff = GetOffBoat.GetSSAction();
            this.RunAction(SceneController.PlayerMove, GetOff, this);
        }
        if (SceneController.BoatMove)
        {
            Moving = BoatMoving.GetSSAction();
            this.RunAction(SceneController.boat, Moving, this);
        }
        base.Update();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Completed,
        int intParam = 0, string strParam = null, Object objectParam = null)
    {

    }
}
