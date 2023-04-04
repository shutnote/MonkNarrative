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
        _Desc = "Growth is primarily used to heal plants however it can be used on all organic life with the right amount of dedication and practice.";
        _Texture = (Texture2D)Resources.Load("Textures/TestTexture.png");
        _TargetTag = "Plant";
        _Length = 15.0f;
        _Radius = 15.0f;
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
