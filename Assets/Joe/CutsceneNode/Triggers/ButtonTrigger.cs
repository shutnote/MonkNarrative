using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

    [SerializeField] private string _ButtonInput;
    [SerializeField] private CutSceneManager _CutSceneManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(_ButtonInput))
        {
            _CutSceneManager.TogglePause(false);
        }
    }
}
