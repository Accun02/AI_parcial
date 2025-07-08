using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f;

    public int maxHealth = 100;
    public int currentHealth;

    public Image healthBarFill;
    public GameObject gameOverPanel;

    private float targetFill = 1f;

    void Start()
    {
        currentHealth = maxHealth;
        targetFill = 1f;
        gameOverPanel.SetActive(false);
        UpdateHealthBar();
    }

    void Update()
    {
        if (healthBarFill.fillAmount != targetFill)
        {
            healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, targetFill, Time.deltaTime * smoothSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Debug.Log("Jugador muerto");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void UpdateHealthBar()
    {
        targetFill = (float)currentHealth / maxHealth;
        healthBarFill.color = targetFill <= 0.4f ? Color.red : Color.green;
    }
}
