using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleColor : MonoBehaviour
{
    private Toggle toggle;

    [SerializeField] private Image buttonBackground;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color unactiveColor;

    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void _SwitchColor()
    {
        print(gameObject.name + " :" + toggle.isOn);
        buttonBackground.color = toggle.isOn ? activeColor : unactiveColor;
    }

}
