using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    private bool _IsBroken = false;

    ParticleSystem[] particlesSystems = null;

    public bool IsBroken
    {
        get { return _IsBroken; }
        set 
        { 
            _IsBroken = value;
            if (_IsBroken)
                StartParticles();
            else
                StopParticles();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        particlesSystems = GetComponentsInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            IsBroken = !IsBroken;
    }

    void StartParticles()
    {
        foreach(ParticleSystem system in particlesSystems)
        {
            system.Play();
        }
    }

    void StopParticles()
    {
        foreach (ParticleSystem system in particlesSystems)
        {
            system.Stop();
        }
    }
}
