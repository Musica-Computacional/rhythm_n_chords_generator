using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public string time_signature; // "4/4"; // { "3/4", "4/4" };
    public string sub_division; // { "1/8", "1/16" };

    //UI things -----------------------------
    public InputField tempoInput;
    public Dropdown metricDropdown;
    public Dropdown subDivDropdown;
    public Dropdown styleDropdown;
    public Toggle randomFillerToggle;
    public Text metricText;
    public Text claveText;
    public Text fillerText;


    //variables -------------------------------------------------------------
    public double bpm; //120.0F;
    private double interval = 0.0f;
    private double sub_interval = 0.0f;

    //Samples -------------------------------------------------------------
    public AudioSource audioSource;
    //rhythm
    public List<AudioClip> tick;
    public List<AudioClip> metric;
    public List<AudioClip> clave;
    public List<AudioClip> filler;
    public int style;
    //harmony
    public List<AudioClip> notes_list;

    //Rythm related
    List<List<int>> rythm = new List<List<int>>();
    List<int> metric_pattern = new List<int>();
    List<int> clave_pattern = new List<int>();
    List<int> clave_pattern_int = new List<int>();
    List<int> filler_pattern = new List<int>();
    bool random_filler;

    //Metronome -----------------------------
    public bool playMetronome = true;
    public int counterControl = 0;
    public int counter = 0;

    //Coroutine
    Coroutine co;

    void Start()
    {
        
    }

    public void GenerateRythm()
    {

        //UI ------------
        string styleName = styleDropdown.options[styleDropdown.value].text;
        if (styleName == "lofi"){style = 0;}
        else if (styleName == "pop"){style = 1;} 
        else if (styleName == "techno"){style = 2;}

        random_filler = randomFillerToggle.isOn;


        bpm = Double.Parse(tempoInput.text.ToString());
        sub_division = subDivDropdown.options[subDivDropdown.value].text;

        time_signature = metricDropdown.options[metricDropdown.value].text;

        Debug.Log("bpm" + bpm);
        Debug.Log("tsg" + time_signature);
        Debug.Log("sub" + sub_division);

        //-- ------------

        rythm = RythmGenerator.Calculations(time_signature, sub_division, random_filler);
        metric_pattern = rythm[0];
        clave_pattern = rythm[1];
        clave_pattern_int = rythm[3];
        filler_pattern = rythm[2];

        metricText.GetComponent<Text>().text = "" + string.Join(",", metric_pattern);
        //claveIntText.GetComponent<Text>().text = "" + string.Join(",", clave_pattern_int);
        claveText.GetComponent<Text>().text = "" + string.Join(",", clave_pattern);
        fillerText.GetComponent<Text>().text = "" + string.Join(",", filler_pattern);


        Debug.Log("metric_pattern: " + string.Join(",", metric_pattern));
        Debug.Log("clave_pattern_int:  " + string.Join(",", clave_pattern_int));
        Debug.Log("clave_pattern:  " + string.Join(",", clave_pattern));
        Debug.Log("fill_pattern:   " + string.Join(",", filler_pattern));
    }



    private bool EvalFillerPattern()
    {
        int internMetric = (int)time_signature[0];
        //Debug.Log("asdfasdfaL: "+ time_signature[0]);
        int indexx = counter % internMetric;
        return filler_pattern[counter] == 1 ? true : false;
    }

    private bool EvalClavePattern()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return clave_pattern[counter] == 1 ? true : false;
    }

    private bool EvalMetricPattern()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return metric_pattern[counter] == 1 ? true : false;
    }

    public void PlayRythm()
    {
        co = StartCoroutine(PlaySamples());
    }

    public void StopRythm()
    {

        counterControl = 0;
        counter = 0;
        StopCoroutine(co);
    }

    public IEnumerator PlaySamples()
    {

        while(Time.time < 1000 && playMetronome)
        {
            //audioSource.PlayOneShot(metric, 0.5f);
            counterControl++;
            //Debug.Log("counterControl:"+counterControl);
            //Debug.Log("counter: "+counter);
            //Debug.Log("clave beat: "+EvalClavePattern());

            audioSource.PlayOneShot(tick[style], 0.5f);
            if (counterControl % 2 == 0)
            {
                Debug.Log("clave beat: " + EvalClavePattern());

                //001
                if (EvalFillerPattern() == false && EvalClavePattern() == false && EvalMetricPattern() == true )
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(metric[style], 0.5f);
                }
                //010
                else if (EvalFillerPattern() == false && EvalClavePattern() == true && EvalMetricPattern() == false)
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(clave[style], 0.5f);
                }
                //011
                else if (EvalFillerPattern() == false && EvalClavePattern() == true && EvalMetricPattern() == true)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(metric[style], 0.5f);
                    audioSource.PlayOneShot(clave[style], 0.5f);
                }
                //100
                else if (EvalFillerPattern() == true && EvalClavePattern() == false && EvalMetricPattern() == false)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(filler[style], 0.3f);
                }
                //101
                else if (EvalFillerPattern() == true && EvalClavePattern() == false && EvalMetricPattern() == true)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(metric[style], 0.5f);
                    audioSource.PlayOneShot(filler[style], 0.3f);
                }
                //110
                else if (EvalFillerPattern() == true && EvalClavePattern() == true && EvalMetricPattern() == false)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(filler[style], 0.3f);
                    audioSource.PlayOneShot(clave[style], 0.5f);
                }
                //111
                else if (EvalFillerPattern() == true && EvalClavePattern() == true && EvalMetricPattern() == true)
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(metric[style], 0.5f);
                    audioSource.PlayOneShot(clave[style], 0.5f);
                    audioSource.PlayOneShot(filler[style], 0.3f);
                }
                
                counter++;
                if (time_signature.StartsWith("4"))
                {
                    if (counter == 16)
                    {
                        counter = 0;
                    }
                }
                else if (time_signature.StartsWith("3"))
                {
                    if (counter == 12)
                    {
                        counter = 0;
                    }
                }
            }

            interval = 60.0f / bpm;
            sub_interval = interval / 2;
            yield return new WaitForSecondsRealtime((float)sub_interval);
        }
    }


}
