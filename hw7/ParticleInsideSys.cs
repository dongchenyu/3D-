using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMes
{
    public float radius = 0f;      
    public float angle = 0f;       
    public ParticleMes (float radius_, float angle_)
    {
        radius = radius_;  
        angle = angle_;
    }
}

public class ParticleInsideSys : MonoBehaviour {
    private ParticleSystem particleSys;           
    private ParticleSystem.Particle[] particle;     
    private ParticleMes[] particleMes;           
    public int Num;       
    public float minradius;             
    public float maxradius;             
    public float speed;            
    // Use this for initialization
    void Start () {
        Num = 2000;
        minradius = 5f;
        maxradius = 10f;
        speed = 8f; 
        particle = new ParticleSystem.Particle[Num];
        particleMes = new ParticleMes[Num];
        particleSys = this.GetComponent<ParticleSystem>();                 
        particleSys.maxParticles = Num;     
        particleSys.Emit(Num);             
        particleSys.GetParticles(particle);
        for (int i = 0; i < Num; i++)
        {  
            float midRadius = (minradius + maxradius) / 2;
            float radiusrate = Random.Range(1.0f, midRadius / minradius);
            float rate = Random.Range(midRadius / maxradius, 1.0f);
            float radius = Random.Range(minradius * radiusrate, maxradius * rate);
            float angle = Random.Range(0.0f, 360.0f);
            float alpha = angle / 180 * Mathf.PI;
            particleMes[i] = new ParticleMes(radius, angle);
            particle[i].position = new Vector3(particleMes[i].radius * Mathf.Cos(alpha), 0f, particleMes[i].radius * Mathf.Sin(alpha));
        }
        particleSys.SetParticles(particle, Num);
    }
	
	// Update is called once per frame
	void Update () {
        int tier = 5;
        for (int i = 0; i < Num; i++)
        {
            particleMes[i].angle -= (i % tier + 1) * (speed / particleMes[i].radius / tier);
            particleMes[i].angle = (360.0f + particleMes[i].angle) % 360.0f;
            float beta = particleMes[i].angle / 180 * Mathf.PI;
            particle[i].position = new Vector3(particleMes[i].radius * Mathf.Cos(beta), 0f, particleMes[i].radius * Mathf.Sin(beta));
        }
        particleSys.SetParticles(particle, Num);
    }
}
