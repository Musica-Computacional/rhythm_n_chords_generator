using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChordsGenerator : MonoBehaviour
{
    private List<string> circulo_quintas_mayores = new List<string>() { "C", "G", "D", "A", "E", "B", "Gb", "Db", "Ab", "Eb", "Bb", "F" };
    private List<string> circulo_quintas_menores = new List<string>() { "Am", "Em", "Bm", "F#m", "C#m", "G#m", "Ebm", "Bbm", "Fm", "Cm", "Gm", "Dm" };

    private List<string> escala_cromatica = new List<string>() { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B",  "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

    private Dictionary<string, string> sharpsNFlats = new Dictionary<string, string>() { 
        { "C#"  ,"Db" },
        { "C#m" ,"Dbm" },
        { "D#"  ,"Eb" },
        { "D#m" ,"Ebm" },
        { "F#"  ,"Gb" },
        { "F#m" ,"Gbm" },
        { "G#"  ,"Ab" },
        { "G#m" ,"Abm" },
        { "A#"  ,"Bb" },
        { "A#m" ,"Bbm" },
    };

    private Dictionary<int, string> romanNumbers = new Dictionary<int, string>() {
        { 1,"I    Tonica" },
        { 2,"II   Subdominante" },
        { 3,"III  Tonica" },
        { 4,"IV   Subdominante" },
        { 5,"V    Dominante" },
        { 6,"VI   Tonica" },
        { 7,"VII° Sensible" }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string getSharpsFromFlat(string val){   
        foreach (KeyValuePair<string, string> chord in sharpsNFlats){
            if (val.Equals(chord.Value)){
                return chord.Key;
            }
        }
        return "El acorde # no existe";
    }

    private List<List<string>> scaleFromNote(string note)
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
                    Debug.Log(romanNumbers[c + 1] + ": " + arranged_scale[c] + " - " + notesFromChord(arranged_scale[c]));
                    chords_list.Add(notesFromChord(arranged_scale[c]));

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
                        arranged_scale.Add(sharpsNFlats[chord]);
                    }
                    else
                    {
                        arranged_scale.Add(chord);
                    }
                }

                for (int c = 0; c < arranged_scale.Count; c++)
                {
                    Debug.Log(romanNumbers[c + 1] + ": " + arranged_scale[c] + " - " + notesFromChord(arranged_scale[c],true));
                    chords_list.Add(notesFromChord(arranged_scale[c],true));
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

    private List<string> notesFromChord(string chord, bool flat_scale=false)
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

            int first_note;
            int third_note;
            int fifth_note;
            int seventh_note;

            if (minor){
                first_note = 1 + index;
                third_note = 4 + index;
                fifth_note = 8 + index;
                seventh_note = 11 + index;
            }else{
                first_note = 1 + index;
                third_note = 5 + index;
                fifth_note = 8 + index;
                seventh_note = 12 + index;
            }
            chord_notes.Add(escala_cromatica[first_note - 1]);
            chord_notes.Add(escala_cromatica[third_note - 1]);
            chord_notes.Add(escala_cromatica[fifth_note - 1]);
            chord_notes.Add(escala_cromatica[seventh_note - 1]);

            if (flat){
                List<string> arranged_chord_notes = new List<string>();
                for (int i = 0; i < chord_notes.Count; i++){
                    string chordd = chord_notes[i];
                    if (chordd.Contains("#")){
                        arranged_chord_notes.Add(sharpsNFlats[chordd]);
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

}
