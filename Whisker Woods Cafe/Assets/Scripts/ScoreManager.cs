using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Slider coffeeSlider;
    [SerializeField] Slider milkSlider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void increaseScore(string type) {
        if (type == "coffee"){
            coffeeSlider.value += .25f;
        }
        else {
            milkSlider.value += .25f;
        }
    }
}
