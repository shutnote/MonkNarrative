using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicTaser : MagicBasic
{

    private Transform _Transform;
    private List<GameObject> _Targets;
    public MagicTaser(Transform Trans)
    {
        _Label = "Taser";
        _TargetTag = "Enemy";
        _Transform = Trans;
        _Length = 3.0f;
        _Radius = 3.0f;
        _Damage = 10.0f;
        _UIColour = Color.blue;
        List<GameObject> _Targets = new List<GameObject> { };
    }

    public override void OnStartUse(GameObject Target)
    {
        if (IsValid(Target.tag))
        {
            Target.AddComponent<TrailRenderer>();
            Target.GetComponent<TrailRenderer>().autodestruct = false;
            Target.GetComponent<TrailRenderer>().AddPosition(_Transform.position);
            Target.GetComponent<TrailRenderer>().material.color = Color.cyan;
            Target.GetComponent<TrailRenderer>().startWidth = 0.25f;
            Target.GetComponent<TrailRenderer>().endWidth = 0.25f;
            Target.GetComponent<TrailRenderer>().AddPosition(Target.transform.position);
        }
    }

    public override void OnUse(GameObject Target)
    {
        if (IsValid(Target.tag))
        {
            Target.GetComponent<HealthManager>().Damage(_Damage * Time.deltaTime);
            Target.GetComponent<TrailRenderer>().SetPosition(0, _Transform.position + new Vector3(0, 0.0f, 0));
            Target.GetComponent<TrailRenderer>().SetPosition(1, Target.transform.position + new Vector3(0, 0.5f, 0));
        }
    }


    public override void OnEndUse(GameObject Target)
    {
        Component.Destroy(Target.GetComponent<TrailRenderer>());
    }
}
