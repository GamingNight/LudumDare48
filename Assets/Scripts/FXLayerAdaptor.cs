using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXLayerAdaptor : MonoBehaviour
{

    public GlobalGameDataSO globalGameDate;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        animator.SetInteger("Layer Number", globalGameDate.GetCurrentLayerIndex());
    }
}
