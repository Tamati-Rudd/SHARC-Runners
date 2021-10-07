using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    public PlayerController pController;
    public Collectable collectable;
    public float speed = 3;
    public bool thrown;
    public Vector3 Launch;

    // Start is called before the first frame update
    public void Start()
    {
        var direction = -transform.right + Vector3.up;

        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);

        Destroy(gameObject, 10);// Destroy automaticlly after 10 seconds
    }

    //Change the Player Speed
    public void ActivateProjectile(bool t)
    {
        
    }
}
