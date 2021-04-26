using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulBar : MonoBehaviour
{
    public Slider slider;
    
    public void SetMaxSoul(int soul)
    {
        slider.maxValue = soul;
        slider.value = soul;
    }

    public void SetSoul(int soul)
    {
        slider.value = soul;
    }
}
