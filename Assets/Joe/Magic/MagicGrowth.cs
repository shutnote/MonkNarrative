using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MagicGrowth : MagicBasic
{
    public MagicGrowth()
    {
        _Label = "Growth";
        _TargetTag = "Plant";
        _Length = 3.0f;
        _Radius = 0.5f;
        _UIColour = Color.green;
    }


    public override void OnUse(GameObject Target)
    {
        if (IsValid(Target.tag) && Target.transform.localScale.x < 1.5f)
        {
            Target.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
        }
    }
}
