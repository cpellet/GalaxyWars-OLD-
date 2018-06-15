using UnityEngine;
using System.Collections;
using uDoil;

public class LocomotionSMB : StateMachineBehaviour
{

    public float m_Damping = 0.15f;

    // private readonly int m_HashHorizontalPara = Animator.StringToHash ("Horizontal");
    // private readonly int m_HashVerticalPara = Animator.StringToHash ("Vertical");

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float horizontal = 0.GetAxis("L_HORIZONTAL");
        float vertical = 0.GetAxis("L_VERTICAL");       

        //   Vector2 input = new Vector2 (horizontal, vertical).normalized;


        //  animator.SetFloat ("Horizontal", input.x, m_Damping, Time.deltaTime);
        //   animator.SetFloat ("Vertical", input.y, m_Damping, Time.deltaTime);
        animator.SetFloat("Horizontal", horizontal, m_Damping, Time.deltaTime);
        animator.SetFloat("Vertical", vertical, m_Damping, Time.deltaTime);

    }
}