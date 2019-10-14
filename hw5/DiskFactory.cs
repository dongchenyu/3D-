using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.Mygame;

namespace Com.Mygame
{
    public class DiskFactory : System.Object
    {
        private static DiskFactory instance;
        private static List<GameObject> diskList; 
        public GameObject diskTemplate;           

        public static DiskFactory getInstance()
        {
            if (instance == null)
            {
                instance = new DiskFactory();
                diskList = new List<GameObject>();
            }
            return instance;
        }
        public int getDisk()
        {
            for (int i = 0; i < diskList.Count; i++)
            {
                if (diskList[i].activeInHierarchy == false)
                {
                    return i;   
                }
            }
            diskList.Add(GameObject.Instantiate(diskTemplate) as GameObject);
            return diskList.Count - 1;
        }
        public GameObject getDiskObject(int id)
        {
            if(id < 0|| id >= diskList.Count)
            {
                return null;
            }
            else
            {
                return diskList[id];
            }
        }
        public void free(int id)
        {
            if (id > -1 && id < diskList.Count)
            { 
                diskList[id].GetComponent<Rigidbody>().velocity = Vector3.zero; 
                diskList[id].transform.localScale = diskTemplate.transform.localScale;
                diskList[id].SetActive(false);
            }  
        }
    }
}
