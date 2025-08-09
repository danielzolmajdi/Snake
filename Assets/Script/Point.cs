using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Point : MonoBehaviour
{

    public GameObject pointPrefab;
    int x;
    int y;
    GameObject SnakeHead;
    SnakeHead snakeHead;

    void Start()
    {
        SnakeHead = GameObject.FindGameObjectWithTag("Player");
        snakeHead = SnakeHead.GetComponent<SnakeHead>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hello");
        if (col.gameObject.tag == "Player")
        {
            x = Random.Range(-5, 6);
            y = Random.Range(-5, 6);
            while (!snakeHead.PlacementOfPointClear(x, y))
            {
                x = Random.Range(-5, 6);
                y = Random.Range(-5, 6);
            }

            col.gameObject.GetComponent<SnakeHead>().pointReached();
            Instantiate(pointPrefab, new Vector3(x, y, 0), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
