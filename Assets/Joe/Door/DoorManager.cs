using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [SerializeField] private bool _IsOpen;

    private float _Max;
    private float _Counter;

    // Start is called before the first frame update
    void Start()
    {
        _Max = 90;
    }

    // Update is called once per frame
    void Update()
    {
        float Openness = 0;
        switch (_IsOpen)
        {
            case false:
                Openness = Mathf.Lerp(270, 360, _Counter);
                break;
            case true:
                Openness = Mathf.Lerp(270, 360, 1 - _Counter);
                break;
        }
        _Counter += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, Openness, 0);
        
    }

    public void Toggle()
    {
        _IsOpen = !_IsOpen;
        _Counter = 0.0f;
    }
}
