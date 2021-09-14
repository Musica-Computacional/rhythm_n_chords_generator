using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bateria : MonoBehaviour
{

    public AudioSource audioSource;
    public List<AudioClip> tick;
    public List<AudioClip> metric;
    public List<AudioClip> clave;
    public List<AudioClip> filler;
    public int style = 0;

    private static List<string> rythm = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        getRythm("4/4","1/8",false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void getRythm(string time_signature, string sub_division,bool random_filler)
    {
        List<List<int>> rythm = RythmGenerator.Calculations(time_signature,sub_division,random_filler);
        // metric,clave,clave_pattern,filler_pathern
        
        AssignSamples(rythm);
    }

    private static List<List<AudioClip>> AssignSamples(List<List<int>> rythm)
    {
        List<int> metric = rythm[0];
        List<int> clave  = rythm[0];
        List<int> filler = rythm[0];


        List<AudioClip> metric_ = new List<AudioClip>();
        List<AudioClip> clave_ = new List<AudioClip>();
        List<AudioClip> filler_ = new List<AudioClip>();

        /*foreach (int beat in metric)
        {
            if (beat == 1)
            {

            }
        }*/

        List<List<AudioClip>> result = new List<List<AudioClip>>();
        result.Add(metric_);
        result.Add(clave_);
        result.Add(filler_);
        return result;
    }
}
