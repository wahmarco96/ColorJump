using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject playerObj;
    public float smoothTime = 0.3f;
    Vector2 Velocity = Vector2.zero;

    public int yOffest;

    void Update()
    {
        Vector2 targetPosition = playerObj.transform.TransformPoint(new Vector3(0, yOffest, -10));

        if (targetPosition.y < transform.position.y) return;
        {
            targetPosition = new Vector3(0, targetPosition.y);
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref Velocity, smoothTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        }

    }

}
