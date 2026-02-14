using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setHealth(int health)
    {
        slider.value = health;
    }
}
