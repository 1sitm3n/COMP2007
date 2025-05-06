using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    public EnemyStats enemy;

    void Update()
    {
        if (enemy != null)
        {
            slider.value = enemy.currentHealth;
            slider.maxValue = enemy.maxHealth;
        }

        // Always face the camera
        transform.LookAt(Camera.main.transform);
    }
}
