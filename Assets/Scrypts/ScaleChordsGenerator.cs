using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChordsGenerator : MonoBehaviour
{

    public string note_name = "C";

    private static List<string> circulo_quintas_mayores = new List<string>() { "C", "G", "D", "A", "E", "B", "Gb", "Db", "Ab", "Eb", "Bb", "F" };
    private static List<string> circulo_quintas_menores = new List<string>() { "Am", "Em", "Bm", "F#m", "C#m", "G#m", "Ebm", "Bbm", "Fm", "Cm", "Gm", "Dm" };

    private static List<string> escala_cromatica_8ve   = new List<string>() { "C3", "C3#", "D3", "D3#", "E3", "F3", "F3#", "G3", "G3#", "A3", "A3#", "B3", "C4", "C4#", "D4", "D4#", "E4", "F4", "F4#", "G4", "G4#", "A4", "A4#", "B4", "C5", "C5#", "D5" };
    private static List<string> escala_cromatica       = new List<string>() { "C",  "C#",  "D",  "D#",  "E",  "F",  "F#",  "G",  "G#",  "A",  "A#",  "B",  "C",  "C#",  "D",  "D#",  "E",  "F",  "F#",  "G",  "G#",  "A",  "A#",  "B",  "C",  "C#",  "D"  };

    private static Dictionary<string, string> sharpsNFlats = new Dictionary<string, string>() { 
        { "C#"  ,"Db"  },
        { "C#m" ,"Dbm" },
        { "D#"  ,"Eb"  },
        { "D#m" ,"Ebm" },
        { "F#"  ,"Gb"  },
        { "F#m" ,"Gbm" },
        { "G#"  ,"Ab"  },
        { "G#m" ,"Abm" },
        { "A#"  ,"Bb"  },
        { "A#m" ,"Bbm" },
    };

    private static Dictionary<string, string> sharpsNFlats_8ve = new Dictionary<string, string>() {
        { "C3#"  ,"D3b"  },
        { "C3#m" ,"D3bm" },
        { "D3#"  ,"E3b"  },
        { "D3#m" ,"E3bm" },
        { "F3#"  ,"G3b"  },
        { "F3#m" ,"G3bm" },
        { "G3#"  ,"A3b"  },
        { "G3#m" ,"A3bm" },
        { "A3#"  ,"B3b"  },
        { "A3#m" ,"B3bm" },

        { "C4#"  ,"D4b"  },
        { "C4#m" ,"D4bm" },
        { "D4#"  ,"E4b"  },
        { "D4#m" ,"E4bm" },
        { "F4#"  ,"G4b"  },
        { "F4#m" ,"G4bm" },
        { "G4#"  ,"A4b"  },
        { "G4#m" ,"A4bm" },
        { "A4#"  ,"B4b"  },
        { "A4#m" ,"B4bm" },
    };

    private static Dictionary<int, string> romanNumbers = new Dictionary<int, string>() {
        { 1,"I    Tonica" },
        { 2,"II   Subdominante" },
        { 3,"III  Tonica" },
        { 4,"IV   Subdominante" },
        { 5,"V    Dominante" }, //cambiado a novena con bajo en la 5ta por que la sonoridad de maj7 no suena bien.
        { 6,"VI   Tonica" },
        { 7,"VII° Sensible" }
    };

    // Start is called before the first frame update
    void Start()
    {
        List<List<string>> chords_listt = scaleFromNote(note_name);
        progressionGenerator(chords_listt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string getSharpsFromFlat(string val){   
        foreach (KeyValuePair<string, string> chord in sharpsNFlats_8ve){
            if (val.Equals(chord.Value)){
                return chord.Key;
            }
        }
        return "El acorde # no existe";
    }

    private static List<List<string>> scaleFromNote(string note)
    {
        bool sharp = false;
        if (note.Contains("#"))
        {
            note = sharpsNFlats[note];
            sharp = true;
        }

        if (circulo_quintas_mayores.Contains(note))
        {
            int index = circulo_quintas_mayores.IndexOf(note);
            List<string> scale = new List<string>() { note };
            if (note.Equals("C"))
            {
                scale.Add(circulo_quintas_menores[circulo_quintas_menores.Count - 1]);
                scale.Add(circulo_quintas_menores[index + 1]);
                scale.Add(circulo_quintas_mayores[circulo_quintas_menores.Count - 1]);
                scale.Add(circulo_quintas_mayores[index + 1]);
                scale.Add(circulo_quintas_menores[index]);
                scale.Add(circulo_quintas_menores[index + 2]);
            }
            else if (note.Equals("F"))
            {
                scale.Add(circulo_quintas_menores[index - 1]);
                scale.Add(circulo_quintas_menores[0]);
                scale.Add(circulo_quintas_mayores[index - 1]);
                scale.Add(circulo_quintas_mayores[0]);
                scale.Add(circulo_quintas_menores[index]);
                scale.Add(circulo_quintas_menores[1]);
            }
            else if (note.Equals("Bb"))
            {
                scale.Add(circulo_quintas_menores[index - 1]);
                scale.Add(circulo_quintas_menores[index + 1]);
                scale.Add(circulo_quintas_mayores[index - 1]);
                scale.Add(circulo_quintas_mayores[index]);
                scale.Add(circulo_quintas_menores[index + 1]);
                scale.Add(circulo_quintas_menores[0]);
            }
            else
            {
                scale.Add(circulo_quintas_menores[index - 1]);
                scale.Add(circulo_quintas_menores[index + 1]);
                scale.Add(circulo_quintas_mayores[index - 1]);
                scale.Add(circulo_quintas_mayores[index + 1]);
                scale.Add(circulo_quintas_menores[index]);
                scale.Add(circulo_quintas_menores[index + 2]);
            }

            List<List<string>> chords_list = new List<List<string>>();

            if (sharp)
            {
                List<string> arranged_scale = new List<string>();
                for (int i = 0; i < scale.Count; i++) {
                    string chord = scale[i];
                    if (chord.Contains("b")) {
                        arranged_scale.Add(getSharpsFromFlat(chord));
                    } else {
                        arranged_scale.Add(chord);
                    }
                }

                for (int c = 0; c < arranged_scale.Count; c++)
                {
                    List<string> the_chordd = new List<string>();
                    if (c == 4) //para modificar el acorde dominante
                    {
                        the_chordd = notesFromChord(arranged_scale[c], false, true);
                    }
                    else
                    {
                        the_chordd = notesFromChord(arranged_scale[c]);
                    }
                    
                    Debug.Log(romanNumbers[c + 1] + ": " + arranged_scale[c] + " - " + string.Join(",", the_chordd));
                    chords_list.Add(the_chordd);
                }
            }
            else
            {
                List<string> arranged_scale = new List<string>();
                for (int i = 0; i < scale.Count; i++)
                {
                    string chord = scale[i];
                    if (chord.Contains("#"))
                    {
                        arranged_scale.Add(sharpsNFlats_8ve[chord]);
                    }
                    else
                    {
                        arranged_scale.Add(chord);
                    }
                }

                for (int c = 0; c < arranged_scale.Count; c++)
                {

                    List<string> the_chordd = new List<string>();
                    if (c == 4)  //para modificar el acorde dominante
                    {
                        the_chordd = notesFromChord(arranged_scale[c], true, true);
                    }
                    else
                    {
                        the_chordd = notesFromChord(arranged_scale[c], true);
                    }
                    
                    Debug.Log(romanNumbers[c + 1] + ": " + arranged_scale[c] + " - " + string.Join(",", the_chordd));
                    chords_list.Add(the_chordd);
                }
            }

            return chords_list;
        }
        else
        {
            Debug.Log("No se puede generar una escala MAYOR a paritr de esa nota!");
            return null;
        }

    }

    private static List<string> notesFromChord(string chord, bool flat_scale=false,bool dominant=false)
    {
        bool minor = false;
        bool flat = false;
        if(chord.Contains("m") && chord.Contains("b")){
            chord = chord.Replace("m", "");
            chord = getSharpsFromFlat(chord);
            minor = true;
            flat = true;
        }else if(chord.Contains("b")){
            chord = getSharpsFromFlat(chord);
            flat = true;
        }else if (chord.Contains("m")){
            chord = chord.Replace("m", "");
            minor = true;
        }

        if(flat_scale == true){
            flat = true;
        }

        if (escala_cromatica.Contains(chord)){
            int index = escala_cromatica.IndexOf(chord);
            List<string> chord_notes = new List<string>();

            //for alterarations
            int bass_note;
            int ninth_note;

            //for no altered chords
            int first_note;
            int third_note;
            int fifth_note;
            int seventh_note;

            if (dominant) { //siempre sera mayor
                first_note = 1 + index;
                ninth_note = 3 + index;
                third_note = 5 + index;
                fifth_note = 8 + index;
                bass_note = fifth_note - 12;
                chord_notes.Add(escala_cromatica_8ve[bass_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[first_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[ninth_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[third_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[fifth_note - 1]);
            }
            else if (minor){
                first_note = 1 + index;
                third_note = 4 + index;
                fifth_note = 8 + index;
                seventh_note = 11 + index;
                chord_notes.Add(escala_cromatica_8ve[first_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[third_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[fifth_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[seventh_note - 1]);
            }
            else{
                first_note = 1 + index;
                third_note = 5 + index;
                fifth_note = 8 + index;
                seventh_note = 12 + index;
                chord_notes.Add(escala_cromatica_8ve[first_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[third_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[fifth_note - 1]);
                chord_notes.Add(escala_cromatica_8ve[seventh_note - 1]);
            }
            

            if (flat){
                List<string> arranged_chord_notes = new List<string>();
                for (int i = 0; i < chord_notes.Count; i++){
                    string chordd = chord_notes[i];
                    if (chordd.Contains("#")){
                        arranged_chord_notes.Add(sharpsNFlats_8ve[chordd]);
                    }else{
                        arranged_chord_notes.Add(chordd);
                    }
                }
                return arranged_chord_notes;
            }else{
                return chord_notes;
            }
        }else{
            Debug.Log("La nota ingresada no existe! No se puede generar el acorde de triada.");
            return null;
        }
    }

    private static List<string> progressionGenerator(List<List<string>> chords_list){
        Dictionary<string, List<string>> chords_dict = new Dictionary<string, List<string>>(){
            {"1", chords_list[0]},
            {"2", chords_list[1]},
            {"3", chords_list[2]},
            {"4", chords_list[3]},
            {"5", chords_list[4]},
            {"6", chords_list[5]},
            {"7", chords_list[6]},
        };
        List<string> tonics = new List<string>() { "1", "3", "6" };
        List<string> subdominants = new List<string>() { "2", "4" };
        List<string> dominants = new List<string>() { "5" };
        List<string> sensibles = new List<string>() { "7" };
        //32 compases aprox un minuto de cancion. 



        List<string> progression = new List<string>();

        List<string> verse1 = verseGenerator(tonics, subdominants);
        List<string> verse2 = verseGenerator(tonics, subdominants);
        for (int i = 0; i < verse1.Count; i++){
            progression.Add(verse1[i]);
        }
        for (int i = 0; i < verse2.Count; i++){
            progression.Add(verse2[i]);
        }
        List<string> chorus1 = chorusGenerator(tonics, subdominants, dominants, sensibles,1);
        for (int i = 0; i < chorus1.Count; i++) {
            progression.Add(chorus1[i]);
        }
        for (int i = 0; i < chorus1.Count; i++)
        {
            progression.Add(chorus1[i]);
        }
        for (int i = 0; i < verse2.Count; i++)
        {
            progression.Add(verse2[i]);
        }
        for (int i = 0; i < chorus1.Count; i++)
        {
            progression.Add(chorus1[i]);
        }
        List<string> chorus2 = chorusGenerator(tonics, subdominants, dominants, sensibles, 2);
        for (int i = 0; i < chorus2.Count; i++)
        {
            progression.Add(chorus2[i]);
        }
        for (int i = 0; i < verse1.Count; i++)
        {
            progression.Add(verse1[i]);
        }


        //Debug.Log("progression");
        //Debug.Log(string.Join(",", progression));
        return progression;
    }

    public static List<List<string>> chordsList(string note_name)
    {
        List<List<string>> chords_list = scaleFromNote(note_name);
        return chords_list;
    }

    public static List<string> progressionToUse(List<List<string>> chords_list)
    {
        return progressionGenerator(chords_list);
    }

    private static List<string> verseGenerator(List<string> tonics, List<string> subdominants)
    {
        List<string> progression = new List<string>();

        int random_tonic;
        int random_subd;
        for (int i = 1; i < 5; i++)
        {
            random_tonic = Random.Range(0, 3); //plus 1 because the max value is not included
            random_subd = Random.Range(0, 2); //plus 1 because the max value is not included
            //primer compas siempre tendra el primer acorde de tonica
            if (i == 1)
            {
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
            
            }
            else if (4 % i == 0) // compases pares tendran tonica o subdominante
            {
                int choice = Random.Range(0, 2); //plus 1 because the max value is not included
                if (choice == 0)
                { //se escogio tonica 
                    string ton = tonics[random_tonic];
                    //si el acorde de este compas es igual al acorde del anterior
                    if (ton == progression[progression.Count - 1])
                    {
                        List<string> tonics_without_repeated_chord = new List<string>();
                        for (int e = 0; e < tonics.Count; e++)
                        {
                            tonics_without_repeated_chord.Add(tonics[e]);
                        }
                        tonics_without_repeated_chord.Remove(ton);
                        int random_tonicc = Random.Range(0, 2); //plus 1 because the max value is not included

                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                    }
                    else
                    {
                        progression.Add(ton);
                        progression.Add(ton);
                        progression.Add(ton);
                        progression.Add(ton);
                    }
                }
                else
                {
                    string subd = subdominants[random_subd];
                    //si el acorde de este compas es igual al acorde del anterior
                    if (subd == progression[progression.Count - 1])
                    {
                        List<string> subdominants_withou_repeated_chord = new List<string>();
                        for (int e = 0; e < subdominants.Count; e++)
                        {
                            subdominants_withou_repeated_chord.Add(tonics[e]);
                        }
                        subdominants_withou_repeated_chord.Remove(subd);

                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                    }
                    else
                    {
                        progression.Add(subd);
                        progression.Add(subd);
                        progression.Add(subd);
                        progression.Add(subd);
                    }
                }
            }
            else
            {
                //primer acorde de tonica
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
            }
        }
        return progression;
    }

    private static List<string> chorusGenerator(List<string> tonics, List<string> subdominants, List<string> dominants, List<string> sensibles,int chorus = 1)
    {
        List<string> progression = new List<string>();

        List<string> tonics_without_base_note = new List<string>();
        for (int e = 0; e < tonics.Count; e++)
        {
            tonics_without_base_note.Add(tonics[e]);
        }
        tonics_without_base_note.Remove("1");

        int random_tonic;
        int random_subd;

        if(chorus == 1)
        {
            for (int i = 1; i < 5; i++)
            {
                random_tonic = Random.Range(0, 2); //plus 1 because the max value is not included
                random_subd = Random.Range(0, 2); //plus 1 because the max value is not included
                //primer compas siempre tendra tonica
                if (i == 1)
                {
                    progression.Add(tonics_without_base_note[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics_without_base_note[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics_without_base_note[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics_without_base_note[random_tonic]); //3 or 6, except 1
                }
                else if (4 % i == 0) // compases pares tendran tonica o subdominante
                {
                    int choice = Random.Range(0, 4); //plus 1 because the max value is not included
                    if (choice == 0)
                    { //sera tonica 
                        string ton = tonics[random_tonic];
                        //si el acorde de este compas es igual al acorde del anterior
                        if (ton == progression[progression.Count - 1])
                        {
                            List<string> tonics_without_repeated_chord = new List<string>();
                            for (int e = 0; e < tonics.Count; e++)
                            {
                                tonics_without_repeated_chord.Add(tonics[e]);
                            }
                            tonics_without_repeated_chord.Remove(ton);
                            int random_tonicc = Random.Range(0, 2); //plus 1 because the max value is not included

                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        }
                        else
                        {
                            progression.Add(ton);
                            progression.Add(ton);
                            progression.Add(ton);
                            progression.Add(ton);
                        }
                    }
                    else if (choice == 1)
                    {//sera subdominante
                        string subd = subdominants[random_subd];
                        //si el acorde de este compas es igual al acorde del anterior
                        if (subd == progression[progression.Count - 1])
                        {
                            List<string> subdominants_withou_repeated_chord = new List<string>();
                            for (int e = 0; e < subdominants.Count; e++)
                            {
                                subdominants_withou_repeated_chord.Add(tonics[e]);
                            }
                            subdominants_withou_repeated_chord.Remove(subd);

                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                        }
                        else
                        {
                            progression.Add(subd);
                            progression.Add(subd);
                            progression.Add(subd);
                            progression.Add(subd);
                        }
                    }
                    else if (choice == 2)
                    {//sera dominante
                        string dom = dominants[0];
                        progression.Add(dom);
                        progression.Add(dom);
                        progression.Add(dom);
                        progression.Add(dom);

                    }
                    else if (choice == 3)
                    {//sera sensible
                        string sens = sensibles[0];
                        progression.Add(sens);
                        progression.Add(sens);
                        progression.Add(sens);
                        progression.Add(sens);

                    }
                }
                else
                {
                    string ton = tonics_without_base_note[random_tonic];
                    if (ton == progression[progression.Count - 1])
                    {
                        List<string> tonics_without_repeated_chord = new List<string>();
                        for (int e = 0; e < tonics_without_base_note.Count; e++)
                        {
                            tonics_without_repeated_chord.Add(tonics[e]);
                        }
                        tonics_without_repeated_chord.Remove(ton);

                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                    }
                    else
                    {
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                    }
                }
            }
        }
        else if (chorus == 2) //this chorus, does not have sensibles and can include the base tonic chord
        {
            for (int i = 1; i < 5; i++)
            {
                random_tonic = Random.Range(0, 3); //plus 1 because the max value is not included
                random_subd = Random.Range(0, 2); //plus 1 because the max value is not included
                                                  //primer compas siempre tendra tonica
                if (i == 1)
                {
                    progression.Add(tonics[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics[random_tonic]); //3 or 6, except 1
                    progression.Add(tonics[random_tonic]); //3 or 6, except 1
                }
                else if (4 % i == 0) // compases pares tendran tonica o subdominante
                {
                    int choice = Random.Range(0, 3); //plus 1 because the max value is not included
                    if (choice == 0)
                    { //se escogio tonica 
                        string ton = tonics[random_tonic];
                        //si el acorde de este compas es igual al acorde del anterior
                        if (ton == progression[progression.Count - 1])
                        {
                            List<string> tonics_without_repeated_chord = new List<string>();
                            for (int e = 0; e < tonics.Count; e++)
                            {
                                tonics_without_repeated_chord.Add(tonics[e]);
                            }
                            tonics_without_repeated_chord.Remove(ton);
                            int random_tonicc = Random.Range(0, 2); //plus 1 because the max value is not included

                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                            progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        }
                        else
                        {
                            progression.Add(ton);
                            progression.Add(ton);
                            progression.Add(ton);
                            progression.Add(ton);
                        }
                    }
                    else if (choice == 1)
                    {//sera subdominante
                        string subd = subdominants[random_subd];
                        //si el acorde de este compas es igual al acorde del anterior
                        if (subd == progression[progression.Count - 1])
                        {
                            List<string> subdominants_withou_repeated_chord = new List<string>();
                            for (int e = 0; e < subdominants.Count; e++)
                            {
                                subdominants_withou_repeated_chord.Add(tonics[e]);
                            }
                            subdominants_withou_repeated_chord.Remove(subd);

                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                            progression.Add(subdominants_withou_repeated_chord[0]);
                        }
                        else
                        {
                            progression.Add(subd);
                            progression.Add(subd);
                            progression.Add(subd);
                            progression.Add(subd);
                        }
                    }
                    else if (choice == 2)
                    {//sera dominante
                        string dom = dominants[0];
                        progression.Add(dom);
                        progression.Add(dom);
                        progression.Add(dom);
                        progression.Add(dom);
                    }
                }
                else
                {
                    string ton = tonics[random_tonic];
                    if (ton == progression[progression.Count - 1])
                    {
                        List<string> tonics_without_repeated_chord = new List<string>();
                        for (int e = 0; e < tonics.Count; e++)
                        {
                            tonics_without_repeated_chord.Add(tonics[e]);
                        }
                        tonics_without_repeated_chord.Remove(ton);

                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                        progression.Add(tonics_without_repeated_chord[0]);
                    }
                    else
                    {
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                        progression.Add(ton); //3 or 6, except 1
                    }
                }
            }
        }
        
        return progression;
    }

    private List<string> autroGenerator(List<string> tonics, List<string> subdominants)
    {
        List<string> progression = new List<string>();

        int random_tonic;
        int random_subd;
        for (int i = 1; i < 5; i++)
        {
            random_tonic = Random.Range(0, 3); //plus 1 because the max value is not included
            random_subd = Random.Range(0, 2); //plus 1 because the max value is not included
            //primer compas siempre tendra el primer acorde de tonica
            if (i == 1)
            {
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"

            }
            else if (4 % i == 0) // compases pares tendran tonica o subdominante
            {
                int choice = Random.Range(0, 2); //plus 1 because the max value is not included
                if (choice == 0)
                { //se escogio tonica 
                    string ton = tonics[random_tonic];
                    //si el acorde de este compas es igual al acorde del anterior
                    if (ton == progression[progression.Count - 1])
                    {
                        List<string> tonics_without_repeated_chord = new List<string>();
                        for (int e = 0; e < tonics.Count; e++)
                        {
                            tonics_without_repeated_chord.Add(tonics[e]);
                        }
                        tonics_without_repeated_chord.Remove(ton);
                        int random_tonicc = Random.Range(0, 2); //plus 1 because the max value is not included

                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                        progression.Add(tonics_without_repeated_chord[random_tonicc]);
                    }
                    else
                    {
                        progression.Add(ton);
                        progression.Add(ton);
                        progression.Add(ton);
                        progression.Add(ton);
                    }
                }
                else
                {
                    string subd = subdominants[random_subd];
                    //si el acorde de este compas es igual al acorde del anterior
                    if (subd == progression[progression.Count - 1])
                    {
                        List<string> subdominants_withou_repeated_chord = new List<string>();
                        for (int e = 0; e < subdominants.Count; e++)
                        {
                            subdominants_withou_repeated_chord.Add(tonics[e]);
                        }
                        subdominants_withou_repeated_chord.Remove(subd);

                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                        progression.Add(subdominants_withou_repeated_chord[0]);
                    }
                    else
                    {
                        progression.Add(subd);
                        progression.Add(subd);
                        progression.Add(subd);
                        progression.Add(subd);
                    }
                }
            }
            else
            {
                //primer acorde de tonica
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
                progression.Add(tonics[0]); //"1"
            }
        }
        return progression;
    }
}
