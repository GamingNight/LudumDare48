using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCubes : MonoBehaviour
{

    private float zPosition;
    private Animator anim;

    void Start()
    {

        zPosition = Random.Range(0.9f, 1.3f);
        transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);

        Animator anim = GetComponentInChildren<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));


    }

}
