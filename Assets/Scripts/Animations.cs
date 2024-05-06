using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;
    private float lastTimeAtack = 0f;
    private bool nAtack = false;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }

    private void Update()
    {
        lastTimeAtack += Time.deltaTime;
    }

    public void AtackAnimation()
    {
        if (animator.GetBool("Block")) return;
        if (lastTimeAtack > 0.3f)
        {
            animator.SetTrigger("Atack");
            lastTimeAtack = 0f;
        }
        else
        {
            animator.SetTrigger("Atack_2");
        }
    }

    public void BlockAnimations(bool isBlocking)
    {
        animator.SetBool("Block", isBlocking);
    }
}
