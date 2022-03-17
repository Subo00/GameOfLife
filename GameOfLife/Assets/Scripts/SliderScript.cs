using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    private void Start() 
    {
        _slider.onValueChanged.AddListener((v)=> 
        {
            _sliderText.text = v.ToString("0.00");
        });
    }
}
