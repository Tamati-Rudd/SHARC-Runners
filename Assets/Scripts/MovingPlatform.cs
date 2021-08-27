using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform point1, point2;
    public float speed;
    public Transform startPosition;

    Vector3 nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = startPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == point1.position)
        {
            nextPosition = point2.position;
        } else if(transform.position == point2.position)
        {
            nextPosition = point1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

    }
}
