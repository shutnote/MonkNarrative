using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MagicBasic
{

    [SerializeField] protected string _TargetTag;
    [SerializeField] protected float _Damage;
    [SerializeField] protected Color _UIColour;

    [SerializeField] protected float _Radius;
    [SerializeField] protected float _Length;
    protected Vector3 _Position;

    protected string _Label;

    public MagicBasic()
    {
        _Label = "None";
        _UIColour = Color.black;
    }

    protected virtual bool IsValid(string Target)
    {
        return _TargetTag == Target || _TargetTag == "Any";
    }

    public virtual void OnStartUse(GameObject Target)
    {
        if (IsValid(Target.tag)) Debug.Log("Start");
    }

    public virtual void OnUse()
    {
        return;
    }

    public virtual void OnUse(GameObject Target)
    {
        if (IsValid(Target.tag)) Debug.Log("Continue");
    }

    public virtual void OnEndUse(GameObject Target)
    {
        if (IsValid(Target.tag)) Debug.Log("End");
    }

    public virtual float GetLength()
    {
        return _Length;
    }

    public virtual float GetRadius()
    {
        return _Radius;
    }

    public virtual Color GetColour()
    {
        return _UIColour;
    }

    public virtual string GetLabel()
    {
        return _Label;
    }
}
