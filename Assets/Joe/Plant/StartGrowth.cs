using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGrowth : MonoBehaviour
{

    private bool _StartGrowing;

    // Update is called once per frame
    void Update()
    {
        if (!_StartGrowing || transform.localScale.x > 1.0f) return;
        transform.localScale = transform.localScale + new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0.5f * Time.deltaTime);
    }

    public void ToggleGrowing(bool Growing)
    {
        _StartGrowing = Growing;
    }
}
