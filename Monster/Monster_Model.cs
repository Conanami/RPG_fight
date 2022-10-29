using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Model : MonoBehaviour
{
    private Animator animator;
    private int currHurtAnimIndex=1;
    public void Init()
    {
        animator = GetComponent<Animator>();
        currHurtAnimIndex = 1;
    }
    
    public void PlayHurtAnim()
    {
        animator.SetTrigger("hurt" + currHurtAnimIndex);
        if (currHurtAnimIndex == 1) currHurtAnimIndex = 2;
        else currHurtAnimIndex = 1;

    }

    public void StopHurtAnim()
    {
        animator.SetTrigger("hurtOver");
        
    }
}
