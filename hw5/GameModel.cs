using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.Mygame;

public class GameModel : MonoBehaviour
{
    public float timedown = 2f;    
    public float timeEmit;        
    public bool timing;        
    public bool shooting;       
    public List<GameObject> disks = new List<GameObject>();    
    public List<int> diskIds = new List<int>();               
    public Color diskColor;              
    public Vector3 emitposition;       
    public Vector3 emitdirection;     
    public float emitspeed;             
    public int emitnumber;              
    public bool emitenable;            

    private SceneController scene;

    void Awake()
    {
        scene = SceneController.getInstance();
        scene.setGameModel(this);
    } 
    public void setting(Color color, Vector3 emitpos, Vector3 emitdir, float speed, int num)
    {
        diskColor = color;
        emitposition = emitpos;
        emitdirection = emitdir;
        emitspeed = speed;
        emitnumber = num;
    }
    public void prepareToEmitDisk()
    {
        if (!timing && !shooting)
        {
            timeEmit = timedown;
            emitenable = true;
        }
    } 
    void emitDisks()
    {
        for (int i = 0; i < emitnumber; i++)
        {
            diskIds.Add(DiskFactory.getInstance().getDisk());
            disks.Add(DiskFactory.getInstance().getDiskObject(diskIds[i]));
            disks[i].GetComponent<Renderer>().material.color = diskColor;
            disks[i].transform.position = new Vector3(emitposition.x, emitposition.y, emitposition.z); 
            disks[i].GetComponent<Rigidbody>().AddForce(emitdirection * Random.Range(emitspeed*0.5f, emitspeed), ForceMode.Impulse);
            disks[i].SetActive(true); 
        }
    }
    void freeDisk(int i)
    {
        DiskFactory.getInstance().free(diskIds[i]);
        disks.RemoveAt(i);
        diskIds.RemoveAt(i);
    }

    void FixedUpdate()
    {
        if (timeEmit > 0)
        {
            timing = true;
            timeEmit -= Time.deltaTime;
        }
        else
        {
            timing = false;
            if (emitenable)
            {
                emitDisks();
                shooting = true;
                emitenable = false;
                
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < disks.Count; ++i)
        {
            if (!disks[i].activeInHierarchy)
            {  
                scene.getJudge().score(); 
                freeDisk(i);
            }
            if (disks[i].transform.position.y < 0)
            {   
                scene.getJudge().fail(); 
                freeDisk(i);
            }
        }
        if (disks.Count == 0)
        {
            shooting = false;
        }
    }
}