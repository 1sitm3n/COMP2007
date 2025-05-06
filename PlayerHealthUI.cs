using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider slider;
    public PlayerStats player;

    void Update()
    {
        if (player != null)
        {
            slider.maxValue = player.maxHealth;
            slider.value = player.currentHealth;
        }
    }
}
