using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesOnDetect : MonoBehaviour
{
    [SerializeField] float burstParticlesValues = 500;
    [SerializeField] ParticleSystem ps = null;

    void Start()
    {
        if(ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Triggered Particles");

            var emission = ps.emission;

            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
            emission.GetBursts(bursts);

            try
            {
                emission.SetBursts(
                    new ParticleSystem.Burst[]
                    {
                        new ParticleSystem.Burst(0.0f, burstParticlesValues)
                    });

                var noise = ps.noise;
                noise.frequency = 1f;
                noise.positionAmount = 1f;
                //Debug.Log("Success");
            }
            catch (Exception e)
            {
                Debug.LogWarning("Exception handled from new particle system data: " + e.Message);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Exit");

            var emission = ps.emission;
            //emission.enabled = true;
            try
            {
                ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
                emission.GetBursts(bursts);

                emission.SetBursts(
                    new ParticleSystem.Burst[]
                    {
                new ParticleSystem.Burst(0.0f, 0)
                    });

                var noise = ps.noise;
                noise.frequency = .1f;
                noise.positionAmount = .1f;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Exception handled from new particle system data: " + e.Message);
            }
        }
    }
}
