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
    [SerializeField] protected Texture _Texture;

    [SerializeField] protected float _Radius;
    [SerializeField] protected float _Length;
    protected Vector3 _Position;

    protected string _Label;
    protected string _Desc;

    protected bool _SelfInflict;

    public MagicBasic()
    {
        _Label = "None";
        _Desc = "None";
        _Texture = Resources.Load<Texture2D>("Textures/TestTexture.png");
        _UIColour = Color.black;
        _SelfInflict = false;
    }

    public virtual void DisplayPopup(CutScenePopup Popup)
    {
        Popup.SetData(_Label, _Desc, _Texture);
    }


    protected virtual bool IsValid(string Target)
    {
        return _TargetTag == Target || _TargetTag == "Any";
    }

    public virtual void OnStartUse(GameObject Target)
    {
    }

    public virtual void OnUse()
    {
        return;
    }

    public virtual void OnUse(GameObject Target)
    {
    }

    public virtual void OnEndUse(GameObject Target)
    {
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

    public virtual bool GetSelfInflict()
    {
        return _SelfInflict;
    }
}
