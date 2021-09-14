using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedGenerator : MonoBehaviour
{
    public string string_seed; // "seed";
    public bool use_string_seed = true;
    public bool random_seed = false;
    public int seed = 1234;
    public InputField TextInput;

    void Start()
    {
        //Debug.Log("yeah: 673381950");
        //Debug.Log(seed);

    }

    public void SetSeed()
    {
        string_seed = TextInput.text.ToString();
        if (use_string_seed)
        {
            seed = string_seed.GetHashCode();
        }

        if (random_seed)
        {
            seed = Random.Range(1, 99999);
        }

        //TextInput.GetComponent<Text>().text = "" + string_seed;

        Random.InitState(seed);
        // return seed;
    }

    public void UpdateStringSeed(string s)
    {
        string_seed = s;
        Debug.Log(string_seed);
    }

}
