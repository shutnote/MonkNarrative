using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPush : MagicBasic
{
    Transform _Transform;
    public MagicPush(Transform Trans)
    {
        _Label = "Push";
        _TargetTag = "Any";
        _Transform = Trans;
        _Length = 10.0f;
        _Radius = 3.0f;
        _UIColour = Color.magenta;
    }

    public override void OnStartUse(GameObject Target)
    {
        if (IsValid(Target.tag))
        {
            Vector3 Delta = (Target.transform.position - _Transform.position).normalized;

            if (Target.GetComponent<Rigidbody>())
            {
                Target.GetComponent<Rigidbody>().AddForce(Delta * 350);
            }

        }

    }
}
