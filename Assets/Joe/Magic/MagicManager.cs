using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicManager : MonoBehaviour
{

    private CapsuleCollider _Collision;
    [SerializeField] private List<MagicBasic> _Spells;
    [SerializeField] private MagicBasic _CurrentSpell;
    [SerializeField] private int _SpellIndex;

    // Start is called before the first frame update
    void Start()
    {
        _Collision = gameObject.AddComponent<CapsuleCollider>();
        _Collision.isTrigger = true;
        _Collision.enabled = true;
        _Collision.direction = 2;
        
        AddNewSpell(new MagicBasic());
        AddNewSpell(new MagicGrowth());
        AddNewSpell(new MagicTaser(transform));
        AddNewSpell(new MagicPush(transform));

    }

    protected void AddNewSpell(MagicBasic Spell)
    {
        Transform UISpells = transform.GetChild(1).GetChild(2);
        Transform UISpellsText = transform.GetChild(1).GetChild(3);

        _Spells.Add(Spell);

        GameObject UIElement = Instantiate(new GameObject());
        GameObject UIElementText = Instantiate(new GameObject());
        UIElement.AddComponent<Image>();
        UIElement.GetComponent<Image>().color = Spell.GetColour();
        UIElement.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        UIElement.transform.position = UISpells.transform.position;
        UIElement.transform.parent = UISpells;

        UIElementText.AddComponent<TextMeshProUGUI>();
        UIElementText.GetComponent<TextMeshProUGUI>().text = Spell.GetLabel();
        UIElementText.GetComponent<TextMeshProUGUI>().fontSize = 24;
        UIElementText.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        UIElementText.transform.position = UISpellsText.transform.position;
        UIElementText.transform.parent = UISpellsText;
    }

    public void MagicUISelection()
    {
        transform.GetChild(1).gameObject.SetActive(true);

        Transform UISpells = transform.GetChild(1).GetChild(2);
        Transform UISpellsText = transform.GetChild(1).GetChild(3);
        Transform UIArrow = transform.GetChild(1).GetChild(1);

        float Rads = ((2 * Mathf.PI) / UISpells.childCount) / 2;
        float Offset = 130.0f;

        Vector2 Mouse = new Vector2(Camera.main.ScreenToViewportPoint(Input.mousePosition).x,
            Camera.main.ScreenToViewportPoint(Input.mousePosition).y);

        float Angle = Vector2.SignedAngle(Vector2.up, Mouse - new Vector2(0.5f, 0.5f));
        if (Angle < 0) Angle = 360 + Angle;
        Angle = Mathf.Abs(360 - Angle);

        UIArrow.rotation = Quaternion.Euler(0, 180, Angle);

        float DegreeChange = (360 / UISpells.childCount);

        for (int i = 0; i < UISpells.childCount; i++)
        {
            Offset = 130.0f;

            if((i * DegreeChange <= Angle && Angle <= (i + 1) * DegreeChange))
            {
                Offset = 200.0f;
                _SpellIndex = i;
            }

            Vector2 Position = new Vector2(Mathf.Sin(Rads), Mathf.Cos(Rads)) * Offset;
            Vector2 PositionExt = new Vector2(Mathf.Sin(Rads), Mathf.Cos(Rads)) * (Offset - 30);

            UISpells.GetChild(i).GetComponent<RectTransform>().localPosition = Position;
            UISpellsText.GetChild(i).GetComponent<TextMeshProUGUI>().transform.position = PositionExt + new Vector2(UISpellsText.position.x, UISpellsText.position.y);

            Rads += (2 * Mathf.PI) / UISpells.childCount;
        }
    }

    public void ToggleActive(bool Active)
    {
        if (Active)
        {
            _Collision.height = _Spells[_SpellIndex].GetLength();
            _Collision.radius = _Spells[_SpellIndex].GetRadius();
        }
        else
        {
            _Collision.radius = 0;
            _Collision.height = 0;
        }
        _Collision.center = new Vector3(0, 0, _Spells[_SpellIndex].GetLength() / 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        _Spells[_SpellIndex].OnStartUse(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        _Spells[_SpellIndex].OnUse(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        _Spells[_SpellIndex].OnEndUse(other.gameObject);
    }

    private void Update()
    {
        _Spells[_SpellIndex].OnUse();
    }
}
