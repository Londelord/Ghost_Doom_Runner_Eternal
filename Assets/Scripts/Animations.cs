using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;
    private float lastTimeAtack = 0f;
    private float velocityRunStart = 0.45f;

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

    public void RunAnimation(Vector2 input)
    {
        if (input.x != 0 || input.y > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
