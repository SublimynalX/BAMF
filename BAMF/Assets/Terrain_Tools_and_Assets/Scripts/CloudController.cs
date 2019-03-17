using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    ParticleSystem cloudSystem;
    public Color color;
    public Color lining;
    bool painted = false;
    public int numberOfParticles;
    public float minSpeed;
    public float maxSpeed;
    public float distance;
    Vector3 startPosition;
    float speed;



    // Start is called before the first frame update
    void Start()
    {
        cloudSystem = this.GetComponent<ParticleSystem>();
        Spawn();


    }
    void Spawn()
    {
        float xPos = UnityEngine.Random.Range(-0.5f, 0.5f);
        float yPos = UnityEngine.Random.Range(-0.5f, 0.5f);
        float zPos = UnityEngine.Random.Range(-0.5f, 0.5f);
        this.transform.localPosition = new Vector3(xPos, yPos, zPos);
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
        startPosition = this.transform.position;
    }

    void Paint()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[cloudSystem.particleCount];
        cloudSystem.GetParticles(particles);
        if(particles.Length > 0)
        {
            for(int i = 0; i < particles.Length; i++)
            {
                particles[i].startColor = Color.Lerp(lining, color, particles[i].position.y / cloudSystem.shape.scale.y);
            }
            painted = true;
            cloudSystem.SetParticles(particles, particles.Length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!painted)
        {
            Paint();
        }
        this.transform.Translate(0, 0, speed);
        if (Vector3.Distance(this.transform.position, startPosition) > distance)
        {
            Spawn();
        }
    }
}
