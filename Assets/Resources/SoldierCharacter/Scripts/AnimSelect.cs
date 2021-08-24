using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSelect : MonoBehaviour {

    public enum UIState
    {
        Idle,
        Idle_B,
        Idle_U,
        Move_U,
        Move_D,
        Move_RL,
        MoveFire_U,
        MoveFire_RL,
        MoveFire_D,
        Fire_URB,
        Fire_RB,
        Fire_U,
        Fire_D,
        Reload,
        Dead,
        Hit,
        Yellow,
        Red,
        Blue
    }

    public UIState US;

    public GameObject P_Char;
    public GameObject Effect;
    Animator Anim;
    Animator E_Anim;
	// Use this for initialization
	void Start () {

        P_Char = GameObject.Find("Solider");
        Effect = GameObject.Find("Effect");
        Anim = P_Char.GetComponent<Animator>();
        E_Anim = Effect.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        if(US == UIState.Idle)
        {
            Anim.SetTrigger("Idle");
        }
        else if(US == UIState.Idle_B)
        {
            Anim.SetTrigger("Idle(Breath)");
        }
        else if (US == UIState.Idle_U)
        {
            Anim.SetTrigger("Idle(Up)");
        }
        else if (US == UIState.Move_U)
        {
            Anim.SetTrigger("Move(Up)");
        }
        else if (US == UIState.Move_D)
        {
            Anim.SetTrigger("Move(Down)");
        }
        else if (US == UIState.Move_RL)
        {
            Anim.SetTrigger("Move(Right_Left)");
        }
        else if (US == UIState.MoveFire_U)
        {
            Anim.SetTrigger("MoveFire(Up)");
        }
        else if (US == UIState.MoveFire_D)
        {
            Anim.SetTrigger("MoveFire(Down)");
        }
        else if(US == UIState.MoveFire_RL)
        {
            Anim.SetTrigger("MoveFire(Right_Left)");
        }
        else if (US == UIState.Fire_U)
        {
            Anim.SetTrigger("Fire(Up)");
        }
        else if (US == UIState.Fire_D)
        {
            Anim.SetTrigger("Fire(Down)");
        }
        else if (US == UIState.Fire_RB)
        {
            Anim.SetTrigger("Fire(Rebound)");
        }
        else if (US == UIState.Fire_URB)
        {
            Anim.SetTrigger("Fire(UnRebound)");
        }
        else if (US == UIState.Reload)
        {
            Anim.SetTrigger("Reload");
        }
        else if (US == UIState.Dead)
        {
            Anim.SetTrigger("Dead");
        }
        else if (US == UIState.Hit)
        {
            Anim.SetTrigger("Hit");
        }
        else if(US == UIState.Yellow)
        {
            E_Anim.SetTrigger("Yellow");
        }
        else if(US == UIState.Red)
        {
            E_Anim.SetTrigger("Red");
        }
        else if(US == UIState.Blue)
        {
            E_Anim.SetTrigger("Blue");
        }
    }
}
