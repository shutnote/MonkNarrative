using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutScenePopup : MonoBehaviour
{
    private TextMeshProUGUI _TitleText;
    private TextMeshProUGUI _DescText;
    private RawImage _Image;

    private void Start()
    {
        _TitleText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _DescText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        _Image = transform.GetChild(3).GetComponent<RawImage>();
    }

    private void Awake()
    {
        
    }

    public void ToggleVisable(bool Vis)
    {
        gameObject.SetActive(Vis);
    }

    public void SetData(string Title, string Desc, Texture texture)
    {
        ToggleVisable(true);
        SetTitle(Title);
        SetDesc(Desc);
        SetImage(texture);
    }

    void SetTitle(string String)
    {
        _TitleText.text = String;
    }

    void SetDesc(string String)
    {
        _DescText.text = String;
    }

    void SetImage(Texture texture)
    {
        _Image.texture = texture;
    }

}
