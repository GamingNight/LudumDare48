using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitionSFX : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Down");
        };
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetTrigger("Up");
        };
    }
}
