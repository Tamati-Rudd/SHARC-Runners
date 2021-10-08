using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour { 

    public Vector3 LaunchOffset;
    public bool facing;
    public float speed = 4;

    // Start is called before the first frame update
    void Start()
    {
        if (facing)
        {
            var direction = transform.right + Vector3.up;// The angle for throwing
            GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(LaunchOffset);

        Destroy(gameObject, 6);// Destroy automatically after 10 seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (!facing)
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }
}