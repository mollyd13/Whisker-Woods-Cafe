using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Slider coffeeSlider;
    [SerializeField] Slider milkSlider;

    public void increaseScore(string type) {
        if (type == "coffee"){
            coffeeSlider.value += .10f;
        }
        else {
            milkSlider.value += .10f;
        }
    }

    public Tuple<float,float> getSliderVal() {
        return new Tuple<float, float>(coffeeSlider.value, milkSlider.value);
    }
}
