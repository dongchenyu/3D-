using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tem.Action;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PatrolUI : SSActionManager, ISSActionCallback, Observer {

    public enum ActionState : int { IDLE, walkleft, walkforward, walkright, walkback }
    private Animator ani;
    private SSAction currentAction;
    private ActionState currentState;
    private const float walkSpeed = 1f;
    private const float runSpeed = 3f;
	new void Start () {
        ani = this.gameObject.GetComponent<Animator>();
        Publish publisher = Publisher.getInstance();
        publisher.add(this);
        currentState = ActionState.IDLE;
        idle();
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
	}

    public void SSEventAction(SSAction source, SSActionEventType events = SSActionEventType.COMPLETED, int intParam = 0, string strParam = null, Object objParam = null)
    {
        if(currentState > ActionState.walkback)
        {
            currentState = ActionState.IDLE;
        }
        else
        {
            currentState = (ActionState)((int)currentState + 1);
        }
        if (currentState == ActionState.walkleft)
        {
            Walkleft();
        }
        else if (currentState == ActionState.walkright)
        {
            Walkright();
        }
        else if (currentState == ActionState.walkforward)
        {
            Walkforward();
        }
        else if (currentState == ActionState.walkback)
        {
            Walkback();
        }
        else
        {
            idle();
        }
    }

    public void idle()
    {
        currentAction = IdleAction.GetIdleAction(Random.Range(1f, 2f), ani);
        this.runAction(this.gameObject, currentAction, this);
    }

    public void Walkleft()
    {
        Vector3 target = Vector3.left * Random.Range(3, 5) + this.transform.position;
        currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
        this.runAction(this.gameObject, currentAction, this);
    }
    public void Walkright()
    {
        Vector3 target = Vector3.right * Random.Range(3, 5) + this.transform.position;
        currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
        this.runAction(this.gameObject, currentAction, this);
    }

    public void Walkforward()
    {
        Vector3 target = Vector3.forward * Random.Range(3, 5) + this.transform.position;
        currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
        this.runAction(this.gameObject, currentAction, this);
    }
    
    public void Walkback()
    {
        Vector3 target = Vector3.back * Random.Range(3, 5) + this.transform.position;
        currentAction = WalkAction.GetWalkAction(target, walkSpeed, ani);
        this.runAction(this.gameObject, currentAction, this);
    }
    
    public void turnNextDirection()
    {
        currentAction.destory = true;
        if(currentState == ActionState.walkleft)
        {
            currentState = ActionState.walkright;
            Walkright();
        }
        else if(currentState == ActionState.walkright)
        {
            currentState = ActionState.walkleft;
            Walkleft();
        }
        else if (currentState == ActionState.walkforward)
        {
            currentState = ActionState.walkback;
            Walkback();
        }
        else if (currentState == ActionState.walkback)
        {
            currentState = ActionState.walkforward;
            Walkforward();
        }

    }
    public void getGoal(GameObject Obj)
    {
        currentAction.destory = true;
        currentAction = RunAction.GetRunAction(Obj.transform, runSpeed, ani);
        this.runAction(this.gameObject, currentAction, this);
    }
    public void loseGoal()
    {
        currentAction.destory = true;
        idle();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Transform parent = collision.gameObject.transform.parent;
        if (parent != null && parent.CompareTag("Wall"))
        {
            turnNextDirection();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            turnNextDirection();
        }
    }

    
    public void notified(ActorState state, int pos, GameObject actor)
    {
        if (state == ActorState.ENTER_AREA)
        {
           
            if (pos == this.gameObject.name[this.gameObject.name.Length - 1] - '0')
            {
                getGoal(actor);
            }
            else loseGoal();
        }
    }
}
