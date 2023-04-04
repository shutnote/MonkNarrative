using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MagicStatis : MagicBasic
{
    public MagicStatis()
    {
        _Label = "Statis";
        _TargetTag = "Enemy";
        _Desc = "Statis is an advanced spell taught to Monks. Used to freeze objects in place.";
        _Texture = (Texture2D)Resources.Load("Assets/Textures/TestTexture.png");
        _Length = 25.0f;
        _Radius = 25.0f;
        _UIColour = Color.magenta;
    }


    public override void OnUse(GameObject Target)
    {
        if (Target.GetComponent<Rigidbody>() && Target.tag == _TargetTag)
        {
            Target.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Target.GetComponent<OnCastedTrigger>().OnCasted();
        }
    }
}
