using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Text sliderValueText;
    private Slider _slider;

    void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
    }

    public void OnValueChanged()
    {
        sliderValueText.text = _slider.value.ToString();
    }
}
