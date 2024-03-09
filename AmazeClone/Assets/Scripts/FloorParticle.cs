using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private int _particleCount=0;
    void Start()
    {
        GetComponent<FloorParticle>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            _particleCount++;
            _particleSystem.Play();
            
        }
    }
}
