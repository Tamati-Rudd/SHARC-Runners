using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private SpringJoint2D Hook;
    public LineRenderer lineCreator;
    private Vector3 GrapplePoint;
    public Transform Player;
    public Rigidbody2D Playerrb;
    public Transform HookHolder;
    public PhotonView PV;
    
    private void Awake()
    {
        lineCreator = GetComponent<LineRenderer>();
        Hook = transform.gameObject.AddComponent<SpringJoint2D>();
        Hook.enabled = false;
        PV = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) { 

            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartGrapple();
            }
            else if (Input.GetKeyUp(KeyCode.Q))
            {
                lineCreator.positionCount = 0;
                PV.RPC("DestroyLine", RpcTarget.All);
                StopGrapple();
            }

        }
    }

    public void StartGrapple()
    {
        lineCreator.forceRenderingOff = false;

        Vector3 p1 = Player.position;
        Vector3 p2 = HookHolder.position;
            
        //Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] allHits;

        allHits = Physics2D.LinecastAll(p1, p2);

        foreach (var hit in allHits)
        {
            // now filter by tag or name
                        

            if (hit.collider.tag != "Player")
            {
                if(hit.collider.tag == "Hook")
                {                    
                    GrapplePoint = hit.point;
                    Hook.connectedAnchor = GrapplePoint;
                    Hook.autoConfigureConnectedAnchor = false;
                    Hook.enableCollision = true;
                    Hook.frequency = 50;
                    Hook.dampingRatio = 0.1f;    
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
        if(Hook != null)
        {        
            if (Hook.isActiveAndEnabled)
            {
                if (lineCreator.positionCount > 0)
                {
                    lineCreator.SetWidth(0.1f, 0.1f);
                    lineCreator.startColor = Color.white;
                    lineCreator.SetPosition(0, transform.position);
                    lineCreator.SetPosition(1, GrapplePoint);

                    if (PV.IsMine)
                    {
                        PV.RPC("DrawRPC", RpcTarget.All, GrapplePoint);
                    }
                }
            }

        }
    }

    [PunRPC]
    public void DrawRPC(Vector3 hookpoint)
    {
        lineCreator.positionCount = 2;
        if (lineCreator.positionCount > 0)
        {
            lineCreator.SetWidth(0.1f, 0.1f);
            lineCreator.startColor = Color.white;
            lineCreator.SetPosition(0, Player.position);
            lineCreator.SetPosition(1, hookpoint);
        }
    }

    [PunRPC]
    public void DestroyLine()
    {
        if (!PV.IsMine)
        {
            lineCreator.positionCount = 0;
        }
    }


}
