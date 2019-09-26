using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge
{
    public FirstController SceneController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool over(int numOfPriests, int numOfDevils)
    {
        if (numOfDevils > numOfPriests && numOfPriests != 0)
        {
            return true;
        }
        return false;
    }
  
}
