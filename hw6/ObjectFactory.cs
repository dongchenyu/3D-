using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{

    private static List<GameObject> used = new List<GameObject>();
    private static List<GameObject> free = new List<GameObject>();
    public void freeObject(GameObject Obj)
    {
        Obj.SetActive(false);
        used.Remove(Obj);
        free.Add(Obj);
    }
    public GameObject setObjectOnPos(Vector3 targetposition, Quaternion faceposition)
    {
        if(free.Count != 0)
        {
            used.Add(free[0]);
            free.RemoveAt(0);
            used[used.Count - 1].SetActive(true);
            used[used.Count - 1].transform.position = targetposition;
            used[used.Count - 1].transform.localRotation = faceposition;
        }
        else
        {
            GameObject aGameObject = Instantiate(Resources.Load("prefabs/Patrol")
               , targetposition, faceposition) as GameObject;
            used.Add(aGameObject);
        }
        return used[used.Count - 1];
    }

   
}
