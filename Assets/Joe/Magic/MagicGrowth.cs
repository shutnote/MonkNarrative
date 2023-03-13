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
        _Length = 15.0f;
        _Radius = 15.0f;
        _UIColour = Color.green;
    }


    public override void OnUse(GameObject Target)
    {
        if (IsValid(Target.tag) && Target.transform.localScale.x < 0.2f)
        {
            Target.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime;
        }
    }
}
