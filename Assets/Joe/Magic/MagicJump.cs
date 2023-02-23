using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicJump : MagicBasic
{
    Transform _Transform;

    public MagicJump(Transform Trans)
    {
        _Label = "High Jump";
        _Transform = Trans;
        _Length = 0.0f;
        _Radius = 0.0f;
        _UIColour = Color.yellow;
    }

    public override void OnStartUse(GameObject Target)
    {
        _Transform.GetComponent<Rigidbody>().AddForce(0, 1000, 0);
    }
}
