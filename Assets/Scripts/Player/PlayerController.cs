using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Vida del jugador.
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] LayerMask eneemymask;
    private int ammo = 10;
    private int damage = 2;
    public Image healthBarFill;
    public GameObject gameOverPanel;
    [SerializeField] private float smoothSpeed = 5f;
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

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
          
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100, eneemymask) && ammo > 0)
            {
                hit.collider.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage);
            }
            ammo--;
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

    void UpdateHealthBar()
    {
        targetFill = (float)currentHealth / maxHealth;
        healthBarFill.color = targetFill <= 0.4f ? Color.red : Color.green;
    }
}