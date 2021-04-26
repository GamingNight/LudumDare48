using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Player Input")]
public class PlayerInput : ScriptableObject
{
    public bool lockInput;
    public string axis;
    public string button;
    public KeyCode keyCode;

    public bool GetKeyDown() {
        if (lockInput)
            return false;
        return Input.GetKeyDown(keyCode);
    }

    public bool GetButtonDown() {
        if (lockInput)
            return false;
        return Input.GetButtonDown(button);
    }

    public float GetAxisRaw() {
        if (lockInput)
            return 0f;
        return Input.GetAxisRaw(axis);
    }
}
