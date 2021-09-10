using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GrapplingHook : MonoBehaviour
{
    private SpringJoint2D Hook;
    public LineRenderer lineCreator;
    private Vector3 GrapplePoint;
    public Transform Player;
    public Rigidbody2D Playerrb;
    public Transform HookHolder;
    public PhotonView PV;
    public Button HookButton;
    public bool buttonPressed;
    public bool oneTime = false;

    private void Awake()
    {
        lineCreator = GetComponent<LineRenderer>();
        Hook = transform.gameObject.AddComponent<SpringJoint2D>();
        Hook.enabled = false;
        PV = GetComponent<PhotonView>();
        HookButton = GetComponent<Button>();
    }

    private void Start()
    {
        //Find Hook Button in Scene
        HookButton = GameObject.Find("Hook").GetComponent<Button>();
        
    }

    private void FixedUpdate()
    {
        //Find if the hook button was pressed
        buttonPressed = HookButton.GetComponent<HookBtn>().buttonPressed;
    }

         
    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine) { 

            //check if hook button was tapped
            if (buttonPressed && !oneTime)
            {
                StartGrapple();
                oneTime = true;
            }
            //if the hook button was released
            else if (!buttonPressed)
            {
                oneTime = false;
                lineCreator.positionCount = 0;
                //Destory the line for all multiplayer targets
                PV.RPC("DestroyLine", RpcTarget.All);
                StopGrapple();
            }

        }
    }

    


public void StartGrapple()
    {
        lineCreator.forceRenderingOff = false;

        //record the two locations of the grapple
        Vector3 p1 = Player.position;
        Vector3 p2 = HookHolder.position;            
        

        //record all the hits the ray cast hits with
        RaycastHit2D[] allHits;

        allHits = Physics2D.LinecastAll(p1, p2);

        foreach (var hit in allHits)
        {
            // now filter by tag or name
                        
            //ignore collision with the player
            if (hit.collider.tag != "Player")
            {
                if(hit.collider.tag == "Hook")
                {                    
                    GrapplePoint = hit.point;

                    //Setting up the Grapple Hook Settings
                    Hook.connectedAnchor = GrapplePoint;
                    Hook.autoConfigureConnectedAnchor = false;
                    Hook.enableCollision = true;
                    Hook.frequency = 50;
                    Hook.dampingRatio = 0.1f;    
                    Hook.enabled = true;

                    //Drawing the line
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
                    //Line Settings
                    lineCreator.SetWidth(0.1f, 0.1f);
                    lineCreator.startColor = Color.white;
                    lineCreator.SetPosition(0, transform.position);
                    lineCreator.SetPosition(1, GrapplePoint);

                    //Draw the line for all mulitplayer clients
                    if (PV.IsMine)
                    {
                        PV.RPC("DrawRPC", RpcTarget.All, GrapplePoint);
                    }
                }
            }

        }
    }

    //Draw the line for all mulitplayer clients
    [PunRPC]
    public void DrawRPC(Vector3 hookpoint)
    {
        lineCreator.positionCount = 2;
        if (lineCreator.positionCount > 0)
        {
            //Line Settings
            lineCreator.SetWidth(0.1f, 0.1f);
            lineCreator.startColor = Color.white;
            lineCreator.SetPosition(0, Player.position);
            lineCreator.SetPosition(1, hookpoint);
        }
    }

    //Destroying the line for all mulitplayer clients
    [PunRPC]
    public void DestroyLine()
    {
        if (!PV.IsMine)
        {
            lineCreator.positionCount = 0;
        }
    }


}
