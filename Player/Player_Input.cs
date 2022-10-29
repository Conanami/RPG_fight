using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input 
{
    private KeyCode runKeyCode = KeyCode.LeftShift;
    private KeyCode attackKeyCode = KeyCode.J;
    public float Horizontal { get => Input.GetAxis("Horizontal"); }

    public float Vertical { get => Input.GetAxis("Vertical"); }

    //下面两个封装是为了改移动端方便
    public bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
    public bool GetRunKey()
    {
        return GetKey(runKeyCode);
    }

    public bool GetAttackKeyDown()
    {
        return GetKeyDown(attackKeyCode);
    }

}
