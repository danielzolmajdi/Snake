using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    float time = 0;
    Vector3 newRotation;
    Vector3 oldRotation;
    Vector3 oldPosition = new Vector3(0, -1, 0);
    public GameObject bodyPrefab;

    List<SnakeBody> listBodys = new List<SnakeBody>();

    SnakeBody snakeBody_;
    UIScript uIScript;
    public Canvas canvas;


    void Start()
    {
        SnakeBody snakeBody_ = new SnakeBody(Instantiate(bodyPrefab, oldPosition, Quaternion.identity));
        listBodys.Add(snakeBody_);

        uIScript = canvas.GetComponent<UIScript>();
    }

    void Update()
    {
        CheckForDirection();


        time += Time.deltaTime;
        if (time >= 0.5)
        {
            time = 0;
            Movement();
        }
    }

    public void Movement()
    {
        //denying 180deg turn;
        if (oldRotation.z - newRotation.z == 180 || oldRotation.z - newRotation.z == -180)
        {
            newRotation = oldRotation;
        }

        //rotate
        this.transform.rotation = Quaternion.Euler(newRotation);

        MoveBody();
        //forword
        oldPosition = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y), Mathf.Round(this.transform.position.z));
        this.transform.position += transform.up;

        CheckForEnd();

        //setting the old rotation as current rotation
        oldRotation = newRotation;

    }

    void MoveBody()
    {
        for (int i = listBodys.Count - 1; i >= 0; i--)
        {
            if (i == 0)
            {
                listBodys[i].setBodyPosition(this.transform.position);
                return;
            }

            listBodys[i].setBodyPosition(listBodys[i - 1].getBodyPosition());
        }
    }

    public void CheckForDirection()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            newRotation = new Vector3(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            newRotation = new Vector3(0, 0, 270);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            newRotation = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            newRotation = new Vector3(0, 0, 180);
        }
    }

    void CheckForEnd()
    {
        //outofbounds
        if (this.transform.position.y >= 6 || this.transform.position.y <= -6 || this.transform.position.x >= 6 || this.transform.position.x <= -6)
        {
            Time.timeScale = 0;
            uIScript.LostUI();
        }

        //touching own snake body
        foreach (SnakeBody b in listBodys)
        {
            if (b.getBodyPosition() == this.transform.position)
            {
                Time.timeScale = 0;
                uIScript.LostUI();
            }
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for (int x = -5; x <= 5; x++)
        {
            for (int y = -5; y <= 5; y++)
            {
                Gizmos.DrawWireCube(new Vector2(x, y), new Vector2(1, 1));
            }
        }

    }


    public void pointReached()
    {
        uIScript.AddScore();
        snakeBody_ = new SnakeBody(Instantiate(bodyPrefab, listBodys[listBodys.Count - 1].getOldPosition(), Quaternion.identity));
        listBodys.Add(snakeBody_);
    }


    public bool PlacementOfPointClear(int x, int y)
    {
        foreach (SnakeBody sb in listBodys)
        {
            if (sb.getBodyPosition() == new Vector3(x, y, 0))
            {
                return false;
            }

        }
        if (this.transform.position == new Vector3(x, y, 0))
        {
            return false;
        }
        return true;
    }
}
