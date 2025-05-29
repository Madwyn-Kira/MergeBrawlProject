using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider HpBarSlider;

    public void InitializeHPBar(float maxHP)
    {
        HpBarSlider.maxValue = maxHP;
        HpBarSlider.value = maxHP;
    }

    public void ChangeHPBarValue(float amount)
    {
        HpBarSlider.value = Mathf.Clamp(HpBarSlider.value + amount, 0f, HpBarSlider.maxValue);
    }
}
