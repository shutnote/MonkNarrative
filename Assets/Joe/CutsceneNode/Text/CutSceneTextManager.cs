using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutSceneTextManager : MonoBehaviour
{

    private TextMeshProUGUI _Text;

    // Start is called before the first frame update
    void Start()
    {
        _Text = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string Text) 
    {
        _Text.text = Text;
    }
}
