using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{

    private float m_currentHealth = 1f;
    private float m_maxHealth = 100f;

    // Relacionado a UI
    [SerializeField] private RawImage healthBar;
    private float imageSize;


    // Visualizadores para debug
    public float currentHealthVis;
    public float maxHealthVis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_currentHealth = m_maxHealth;

        if (healthBar != null)
            imageSize = healthBar.rectTransform.sizeDelta.x;

        maxHealthVis = m_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealthVis = m_currentHealth;
    }

    public void TakeDamage(float damage)
    {
        m_currentHealth -= damage;
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.rectTransform.sizeDelta = new Vector2(imageSize * healthPercent, healthBar.rectTransform.sizeDelta.y);
    }

    public bool isDead
    {
        get => m_currentHealth <= 0f;
    }
    public float healthPercent
    {
        get => m_currentHealth / m_maxHealth;
    }
}
