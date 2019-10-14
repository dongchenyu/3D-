using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Mygame;


public class DiskFactoryBC : MonoBehaviour
{
    public GameObject disk;

    void Awake()
    { 
        DiskFactory.getInstance().diskTemplate = disk;
    }
}
