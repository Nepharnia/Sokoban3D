using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool onGoal;

    private GameObject[] walls;
    private GameObject[] boxes;
    private GameObject[] goals;


    public bool Move(Vector3 direction) {
        if (BoxBlocked(transform.position, direction)) {
            return false;
        }
        else {
            transform.Translate(direction);
            TransformWhenOnGoal();
            return true;
        }
    }

    private void TransformWhenOnGoal() {
        foreach (GameObject goal in goals) {
            if (transform.position.x == goal.transform.position.x && transform.position.z == goal.transform.position.z) {
                GetComponent<SpriteRenderer>().color = Color.red;
                onGoal = true;
                return;
            }
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        onGoal = false;
    }

    private bool BoxBlocked(Vector3 position, Vector3 direction) {
        Vector3 newPos = new Vector3(position.x, 0.0f, position.z) + direction;
        /*newPos.ToString();
        Debug.Log(newPos);*/
        foreach (GameObject wall in walls) {
            Vector3 wallPos = new Vector3(wall.transform.position.x, 0.0f, wall.transform.position.z);
            if (wallPos == newPos) {
                return true;
            }
        }
        foreach (GameObject box in boxes) {
            Vector3 boxPos = new Vector3(box.transform.position.x, 0.0f, box.transform.position.z);
            if (boxPos == newPos) {
                Box bx = box.GetComponent<Box>();
                if (bx && bx.Move(direction)) {
                    return false;
                }
                else {
                    return true;
                }
            }
        }
        return false;
    }


}
