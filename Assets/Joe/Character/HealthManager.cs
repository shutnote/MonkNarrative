using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] protected float _HealthCur;
    [SerializeField] protected float _HealthMax;

    // Start is called before the first frame update
    void Start()
    {
        _HealthCur = _HealthMax;
    }

    public void Damage(float Delta)
    {
        _HealthCur -= Delta;

        if(_HealthCur < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
