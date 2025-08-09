using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody
{
    public GameObject body;
    public GameObject parent;
    public Vector3 bodyOldPostion;

    public SnakeBody(GameObject body_)
    {
        body = body_;
    }

    public Vector3 getBodyPosition()
    {
        return body.transform.position;
    }
    public void setBodyPosition(Vector3 newPostion)
    {
        bodyOldPostion = getBodyPosition();
        body.transform.position = newPostion;
    }

    public Vector3 getOldPosition()
    {
        return bodyOldPostion;
    }
}
