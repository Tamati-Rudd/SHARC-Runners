using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script moves the firing point of the enemy turret
public class MoveTurret : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] float objectSpeed;
    Transform nextPos;
    int nextPosIndex;

    // Start is called before the first frame update
    private void Start()
    {
        nextPos = Positions[0];  
    }

    // Update is called once per frame
    private void Update()
    {
        MoveGameObject();
    }

    private void MoveGameObject()
    {
        if (transform.position == nextPos.position)
        {
            nextPosIndex++;
            if(nextPosIndex >= Positions.Length)
            {
                nextPosIndex = 0;
            }
            nextPos = Positions[nextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.deltaTime);
        }
    }
}
