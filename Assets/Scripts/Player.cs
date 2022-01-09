using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private GameObject[] walls;
    private GameObject[] boxes;  

    void Start()
    {
        if(walls == null) 
        {
            walls = GameObject.FindGameObjectsWithTag("Wall");
            //Debug.Log("Wall added to walls");
            //if(walls == null){Debug.Log("No wall in walls");};
        }
        if(boxes == null)
        {
            boxes = GameObject.FindGameObjectsWithTag("Box");
            //Debug.Log("Box added to boxes");
            //if(walls == null){Debug.Log("No box in boxes");};
        }
    }

    public bool Move(Vector3 direction) {
        //Debug.Log("Player is moving");
        if (Mathf.Abs(direction.x) < 0.5) {
            direction.x = 0;
        }
        else {
            direction.z = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction)) {
            Debug.Log("Player blocked");
            return false;
        }
        else {
            transform.Translate(direction);
            return true;
        }
    }

    private bool Blocked(Vector3 position, Vector3 direction) {
        Vector3 newPos = new Vector3(position.x, 0.0f, position.z) + direction;
        newPos.ToString();
        Debug.Log(newPos + "player");
        foreach (GameObject wall in walls) {
            Vector3 wallPos = new Vector3(wall.transform.position.x, 0.0f, wall.transform.position.z);
            wallPos.ToString();
            Debug.Log(wallPos + "wall");
            if (wallPos == newPos) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        foreach (GameObject box in boxes) {
            Vector3 boxPos = new Vector3(box.transform.position.x, 0.0f, box.transform.position.z);
            /*boxPos.ToString();
            Debug.Log(boxPos);*/
            if (boxPos == newPos) 
            {
                Box bx = box.GetComponent<Box>();
                if(bx && bx.Move(direction)) {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
        }
        return false;
    }
}
