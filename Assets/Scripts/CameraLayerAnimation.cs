using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLayerAnimation : MonoBehaviour
{

    public GlobalGameData globalGameData;
    public Animator anim;

    private int lastIndex = -1;

    private void Awake()
    {
        globalGameData.InitLayer();
    }

    void Start()
    {
        lastIndex = -1;
    }

    void Update()
    {

        int currentIndex = globalGameData.GetCurrentLayerIndex();
        if (lastIndex != currentIndex)
        {
            lastIndex = currentIndex;
            switch (lastIndex)
            {
                case 0:
                    anim.SetInteger("Layer", 0);
                    break;
                case 1:
                    anim.SetInteger("Layer", 1);
                    break;
                case 2:
                    anim.SetInteger("Layer", 2);
                    break;
                default:
                    break;
            }
        }
    }

}
