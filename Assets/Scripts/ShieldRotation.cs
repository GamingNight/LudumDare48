using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 0, 30 * Time.deltaTime);
    }
}