using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metronomee : MonoBehaviour
{
    public double bpm = 140.0F;

    public double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F; 
    bool ticked = false; 
    public AudioSource audioSource; 
    public AudioClip tick;

    public string time_signature = "4/4";
    int counter = 0;

    public event Action<double> Ticked;

    void Start()
    {
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;

        nextTick = startTick + (60.0F / bpm);
    }

    // Just an example OnTick here
    void OnTick()
    {
        Debug.Log("Tick");

        counter += 1;
        if (counter == 1)
        {
            audioSource.PlayOneShot(tick, 0.5f);
        }
        else
        {
            audioSource.PlayOneShot(tick, 0.3f);
        }

        if (time_signature == "4/4")
        {
            if(counter == 4)
            {
                counter = 0;
            }
        }
        else
        {
            if (counter == 3)
            {
                counter = 0;
            }
        }
    }

    public double GetNextTickTime()
    {
        return nextTick;
    }

    void FixedUpdate()
    {
        double timePerTick = 60.0f / bpm;
        double dspTime = AudioSettings.dspTime;

        while (dspTime >= nextTick)
        {

            ticked = false;
            nextTick += timePerTick;
        }

    }
    void LateUpdate()
    {
        if (!ticked && nextTick >= AudioSettings.dspTime)
        {
            ticked = true;
            BroadcastMessage("OnTick");
            //BroadcastMessage("GetNextTickTime");
        }
    }
}
