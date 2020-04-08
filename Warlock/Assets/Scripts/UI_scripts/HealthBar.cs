using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider _slider;

    public void setHealth(float amount){
        _slider.value = amount;
    }

    public void setMaxHealth(float amount){
        _slider.maxValue = amount;
        _slider.value = amount;
    }

}
