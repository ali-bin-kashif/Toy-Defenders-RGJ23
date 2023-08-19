using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient barColor;

    public Image barFill;

    public GameObject healthCanvas;

    private void Start()
    {
        healthCanvas.SetActive(false);
    }
    public void SetMaxHealth(float health)
    {
        //Debug.Log(healthBar.maxValue);
        healthBar.maxValue = health;
        healthBar.value = health;

        barFill.color = barColor.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;

        barFill.color = barColor.Evaluate(healthBar.normalizedValue);
    }

    public IEnumerator ShowHealthBar()
    {
        healthCanvas.SetActive(true);

        yield return new WaitForSeconds(2f);

        healthCanvas.SetActive(false);

    }
}
