using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Input")]
public class PlayerInput : ScriptableObject
{
    public string axis;
    public string button;
    public KeyCode keyCode;


    public bool GetKeyDown() {

        return Input.GetKeyDown(keyCode);
    }

    public bool GetButtonDown() {

        return Input.GetButtonDown(button);
    }

    public float GetAxisRaw() {

        return Input.GetAxisRaw(axis);
    }
}
