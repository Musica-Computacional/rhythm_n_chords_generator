using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RythmGenerator : MonoBehaviour
{

    //public string time_signature; // { "3/4", "4/4" };
    //public string sub_division; // { "1/8", "1/16" };
    //public double tempo = 60.0;
    public  bool remove_beats_random = false;
    //public static bool remove_beats_randomly;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        /*if (remove_beats_random)
        {
            remove_beats_randomly = true;
        }
        else
        {
            remove_beats_randomly = false;
        }*/

        //DebugMethod();
        /*List<string> actual_clave = ClaveGenerator(time_signature,sub_division);
        Debug.Log("actual_clave: " + string.Join(",", actual_clave));
        List<List<int>> metric_clave_filler = FillerGenerator(actual_clave, sub_division);
        Debug.Log("metric_pattern: " + string.Join(",", metric_clave_filler[0]));
        Debug.Log("clave_pattern_o: " + string.Join(",", metric_clave_filler[1]));
        Debug.Log("fill_pattern: " + string.Join(",", metric_clave_filler[2]));
        string result = string.Join("", metric_clave_filler[0]) 
            + "_" + string.Join("", actual_clave)
            + "_" + string.Join("", metric_clave_filler[1])
            + "_" + string.Join("", metric_clave_filler[2]);
        Debug.Log(result);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<List<int>> Calculations(string time_signature, string sub_division,bool remove_beats_randomly)
    {
        //DebugMethod();
        List<string> actual_clave = ClaveGenerator(time_signature, sub_division);
        //Debug.Log("actual_clave: " + string.Join(",", actual_clave));
        List<List<int>> metric_clave_filler = FillerGenerator(time_signature,actual_clave, sub_division,remove_beats_randomly);
        //Debug.Log("metric_pattern: " + string.Join(",", metric_clave_filler[0]));
        //Debug.Log("clave_pattern_o: " + string.Join(",", metric_clave_filler[1]));
        //Debug.Log("fill_pattern: " + string.Join(",", metric_clave_filler[2]));

        List<int> actual_clave_int = new List<int>();

        for (var i = 0; i < actual_clave.Count; i++)
        {
            actual_clave_int.Add(int.Parse(actual_clave[i]));
        }

        List<List<int>> result = new List<List<int>>();
        result.Add(metric_clave_filler[0]);
        result.Add(metric_clave_filler[1]);
        result.Add(metric_clave_filler[2]);
        result.Add(actual_clave_int);

        return result;
    }

    private static List<string> ClaveGenerator(string time_signature,string sub_division)
    {
        List<string> all_possible_claves = new List<string>();

        if (time_signature == "3/4")
        {
            if (sub_division == "1/8")
            {
                all_possible_claves = new List<string>() { "33", "222" };
            }
            else //sub_division == "1/16"
            {
                List<string> perr34 = FindPermutations("22233");
                var per34 = RemoveDuplicatesSet(perr34);
                all_possible_claves = per34.Union<string>(perr34).ToList<string>();
                all_possible_claves.Add("222222");
                all_possible_claves.Add("3333");
            }

        }
        else if (time_signature == "4/4")
        {
            if (sub_division == "1/8")
            {
                // clave para 4/4 con 1/8 de subdivision
                List<string> perrr = FindPermutations("332");
                all_possible_claves = RemoveDuplicatesSet(perrr);
                all_possible_claves.Add("2222");
            }
            else //sub_division == "1/16"
            {
                // clave para 4/4 con 1/16 de subdivision
                List<string> perr = FindPermutations("223333");
                List<string> perr2 = FindPermutations("2222233");
                var per = RemoveDuplicatesSet(perr);
                var per2 = RemoveDuplicatesSet(perr2);
                all_possible_claves = per.Union<string>(per2).ToList<string>();
                all_possible_claves.Add("22222222");
            }
        }
        //Debug.Log("time_signature: " + time_signature + " sub_division: " + sub_division);
        //Debug.Log("all_possible_claves: " + string.Join(",", all_possible_claves));

        string chosen_clave = all_possible_claves[Random.Range(0, all_possible_claves.Count())];
        List<string> chosen_clave_list = new List<string>();

        foreach (char s in chosen_clave){
            chosen_clave_list.Add(s.ToString());
        }
        return chosen_clave_list;
    }

    private static List<List<int>> FillerGenerator(string time_signature, List<string> clave, string sub_division,bool remove_beats_randomly)
    {
        List<int> clave_pattern = new List<int>();
        List<int> fill_pattern = new List<int>();
        List<int> clave_pattern_o = new List<int>();
        List<int> metric_pattern;

        if (time_signature == "4/4")
        {
            metric_pattern = new List<int>() { 1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0 };
        }
        else //(time_signature = "3/4")
        {
            metric_pattern = new List<int>() { 1,0,0,1,0,0,1,0,0,1,0,0 };
        }
        
        // Si la subdivision es en corcheas entonces podemos subdividir mas.
        if (sub_division == "1/8")
        {
  
            for (int i = 0; i < clave.Count; i++)
            {
                if (clave[i] == "2")
                {
                    //clave_pattern.Add("4");
                    clave_pattern.Add(1);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                }
                else if (clave[i] == "3")
                {
                    //clave_pattern.Add("6");
                    clave_pattern.Add(1);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                }
            }

            //Debug.Log("clave_pattern +div: " + string.Join(",", clave_pattern));
        }
        else //(sub_division == "1/16")
        {
            for (int i = 0; i < clave.Count; i++)
            {
                if (clave[i] == "2")
                {
                    clave_pattern.Add(1);
                    clave_pattern.Add(0);
                }
                else if (clave[i] == "3")
                {
                    clave_pattern.Add(1);
                    clave_pattern.Add(0);
                    clave_pattern.Add(0);
                }
            }
            //Debug.Log("clave_pattern: " + string.Join(",", clave_pattern));
        }

        //hacemos copia de la clave ya que aca ya esta construida
        for (int i = 0;i < clave_pattern.Count; i++)
        {
            clave_pattern_o.Add(clave_pattern[i]);
        }

        // Convertimos los beats a silencios y bicerversa para obtener mas resultados
        if (remove_beats_randomly)
        {
            // hace shuffle de la lista
            //fill_pattern = clave_pattern.OrderBy(i => System.Guid.NewGuid()).ToList();
            clave_pattern = ShuffleList(clave_pattern);
        }
        
        //clave_pattern = clave_pattern.OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < clave_pattern.Count; i++)
        {
            if (clave_pattern[i] == 1)
            {
                fill_pattern.Add(0);
            }
            else
            {
                fill_pattern.Add(1);
            }
        }

        List<List<int>> result = new List<List<int>>();
        result.Add(metric_pattern);
        result.Add(clave_pattern_o);
        result.Add(fill_pattern);
        return result;
    }

    private static List<int> ShuffleList(List<int> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
        return ts;
    }

    private static List<string> FindPermutations(string set)
    {
        var output = new List<string>();
        if (set.Length == 1)
        {
            output.Add(set);
        }
        else
        {
            foreach (var c in set)
            {
                // Remove one occurrence of the char (not all)
                var tail = set.Remove(set.IndexOf(c), 1);
                foreach (var tailPerms in FindPermutations(tail))
                {
                    output.Add(c + tailPerms);
                }
            }
        }
        return output;
    }

    public static List<T> RemoveDuplicatesSet<T>(List<T> items)
    {
        // Use HashSet to remember items seen.
        var result = new List<T>();
        var set = new HashSet<T>();
        for (int i = 0; i < items.Count; i++)
        {
            // Add if needed.
            if (!set.Contains(items[i]))
            {
                result.Add(items[i]);
                set.Add(items[i]);
            }
        }
        return result;
    }

    void ExampleRemoveDuplicates()
    {
        var input = new List<string>() { "j", "x", "j", "x", "y" };
        var output = RemoveDuplicatesSet(input);
        Debug.Log("Input: " + string.Join(",", input));
        Debug.Log("Output: " + string.Join(",", output));
    }

    void DebugMethod()
    {
        Debug.Log("clave para 3/4 con 1/8 de subdivision");
        List<string> er = new List<string>() { "33", "222" };
        Debug.Log("Output3: " + string.Join(",", er));
        Debug.Log("clave para 3/4 con 1/16 de subdivision");
        List<string> perr34 = FindPermutations("22233");
        var per34 = RemoveDuplicatesSet(perr34);
        List<string> all_claves32 = per34.Union<string>(perr34).ToList<string>();
        all_claves32.Add("222222");
        all_claves32.Add("3333");
        Debug.Log("Output3: " + string.Join(",", all_claves32));
        Debug.Log("clave para 4/4 con 1/8 de subdivision");
        List<string> perrr = FindPermutations("332");
        var all_claves = RemoveDuplicatesSet(perrr);
        all_claves.Add("2222");
        Debug.Log("Output: " + string.Join(",", all_claves));
        Debug.Log("clave para 4/4 con 1/16 de subdivision");
        List<string> perr = FindPermutations("223333");
        List<string> perr2 = FindPermutations("2222233");
        var per = RemoveDuplicatesSet(perr);
        var per2 = RemoveDuplicatesSet(perr2);
        List<string> all_claves2 = per.Union<string>(per2).ToList<string>();
        all_claves.Add("22222222");
        //Debug.Log("Output: " + string.Join(",", per));
        //Debug.Log("Output2: " + string.Join(",", per2));
        Debug.Log("Output3: " + string.Join(",", all_claves2));
    }

}

