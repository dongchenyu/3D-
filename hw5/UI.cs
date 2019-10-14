using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Com.Mygame;

public class UI : MonoBehaviour
{ 
    public Text scoreText;  
    public Text roundText;
    public Text winText;  
    private int round;
    public GameObject bullet;          
 
    public float fireRate = .1f;       
    public float speed = 1000f;        

    private float nextFireTime;        

    private IUserInterface userInt;   
    private IQueryStatus queryInt;      

    void Start()
    {
        bullet = GameObject.Instantiate(bullet) as GameObject;
        userInt = SceneController.getInstance() as IUserInterface;
        queryInt = SceneController.getInstance() as IQueryStatus;
    }

    void Update()
    {
            if (queryInt.getRound() <= 3)
            {
                userInt.emitDisk();   
                if (queryInt.isShooting())
                {
                    winText.text = "";     
                }
                if (queryInt.isShooting() && Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
                {
                    nextFireTime = Time.time + fireRate;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   
                    bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;                      
                    bullet.GetComponent<Rigidbody>().position = transform.position;                
                    bullet.GetComponent<Rigidbody>().AddForce(ray.direction * speed, ForceMode.Impulse);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "disk")
                    {
                        hit.collider.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                winText.text = "You Win";
            }
        roundText.text = "Round: " + queryInt.getRound().ToString();
        scoreText.text = "Score: " + queryInt.getPoint().ToString();  
        if (round != queryInt.getRound())
        {
            round = queryInt.getRound();
            winText.text = "Round " + round.ToString();
        }
        
    }
}
