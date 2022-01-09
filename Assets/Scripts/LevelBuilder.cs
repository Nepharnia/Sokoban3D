using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelElement //each item necessary to build our maps
{
    public string character;
    public GameObject Prefab;

}

public class LevelBuilder : MonoBehaviour
{
    public int currentLevel;
    public List<LevelElement> levelElements;
    private Level level;

    GameObject GetPrefab(char c)
    {
        LevelElement levelElement = levelElements.Find(le => le.character == c.ToString());
        if (levelElement != null)
        {
            return levelElement.Prefab;
        } else
        {
            return null;
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        if(currentLevel >= GetComponent<Levels>().levels.Count)
        {
            currentLevel = 0; //return to the first level
        }
    }

    public void Build()
    {
        level = GetComponent<Levels>().levels[currentLevel];
        int startX = - level.Width / 2;
        int x = startX;
        int z = - level.Height / 2;
        foreach (var row in level.Rows)
        {
            foreach (var ch in row)
            {
                //Debug.Log(ch);
                GameObject prefab = GetPrefab(ch);
                if (prefab)
                {
                    //Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, 0.0f, z), Quaternion.identity);
                }
                x++;
            }
            z++;
            x = startX;
        }
    }
}

