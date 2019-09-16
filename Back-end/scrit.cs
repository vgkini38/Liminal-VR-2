using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ParticleScaler;

public class scrit : MonoBehaviour
{
    public GameObject a;
    private bool status=false;
    int num = 0;
    private ParticleSystem hitParticles;

    // Start is called before the first frame update
    void Start()
    {
        hitParticles = this.GetComponentInChildren<ParticleSystem>();
        hitParticles.Stop();
    }
    void OnPointing()
    {

            status = true;

        
    }

    void OnLeave()
    {
       
            status = false;
       
    }
    // Update is called once per frame
    void Update()
    {
        if (status)
        {
            if (num == 0)
            {
                hitParticles.Play();
            }


        }
        else
        {
            hitParticles.Stop();
        }
    }
}
