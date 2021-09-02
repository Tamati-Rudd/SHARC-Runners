using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private SpringJoint2D Hook;
    public LineRenderer lineCreator;
    private Vector3 GrapplePoint;
    public Transform Player;

    private void Awake()
    {
        lineCreator = GetComponent<LineRenderer>();
        Hook = transform.gameObject.AddComponent<SpringJoint2D>();
        Hook.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }

    public void StartGrapple()
    {
        lineCreator.forceRenderingOff = false;
        Vector3 playerposition = Player.position;
        GrapplePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] allHits;

        allHits = Physics2D.LinecastAll(playerposition, GrapplePoint);

        foreach (var hit in allHits)
        {
            // now filter by tag or name
                        

            if (hit.collider.tag != "Player")
            {
                if(hit.collider.tag == "Hook")
                {
                    Hook.connectedAnchor = GrapplePoint;
                    Hook.autoConfigureConnectedAnchor = false;
                    // float DistanceBetween = Vector3.Distance(playerposition, GrapplePoint);

                    // Hook.distance = DistanceBetween;
                    //Hook.dampingRatio = 4f;
                    //Hook.frequency = 2;
                    Hook.enabled = true;
                    lineCreator.positionCount = 2;
                }

            }
        }





    }

    private void LateUpdate()
    {
        Draw();
    }

    public void StopGrapple()
    {
        Hook.enabled = false;
        lineCreator.forceRenderingOff = true;
    }

    public void Draw()
    {
        if (Hook.isActiveAndEnabled)
        {
            if(lineCreator.positionCount> 0)
            {
                lineCreator.SetWidth(0.1f, 0.1f);
                lineCreator.startColor = Color.white;
                lineCreator.SetPosition(0, transform.position);
                lineCreator.SetPosition(1, GrapplePoint);
            }
        }
    }
}
