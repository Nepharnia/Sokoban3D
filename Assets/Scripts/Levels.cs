using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level //Un seul level
{
    public List<string> Rows = new List<string>();

    public int Height {get {return Rows.Count;}}
    public int Width 
    {
        get 
        {
            int MaxLength = 0;
            foreach (var r in Rows)
            {
                if(r.Length > MaxLength) MaxLength = r.Length;
            }
            return MaxLength;
        }
    }
}

public class Levels : MonoBehaviour
{
    public string filename;
    public List<Level> levels;

    void Awake()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        if (!textAsset)
        {
            Debug.Log("Levels does not exist");
            return;
        }
        else
        {
            Debug.Log("Levels imported");
        }
        string completeText = textAsset.text;
        string[] lines;
        lines = completeText.Split(new string[] {"\n"}, System.StringSplitOptions.None);
        levels.Add(new Level());
        for (long i = 0; i < lines.LongLength; i++)
        {
            string line = lines[i];
            if (line.StartsWith(";"))
            {
                Debug.Log("New level added");
                levels.Add(new Level());
                continue;
            }
            levels[levels.Count - 1].Rows.Add(line);
        }
    }
}

