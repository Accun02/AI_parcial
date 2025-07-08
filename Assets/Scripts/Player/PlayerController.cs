using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Vida del jugador.
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] LayerMask eneemymask;

    [SerializeField] private AudioSource audioSource;

    public int ammo = 10;

    private int damage = 2;
    public Image healthBarFill;
    public GameObject gameOverPanel;
    [SerializeField] private float smoothSpeed = 5f;
    private float targetFill = 1f;
    public AudioClip shootSound;
    public TextMeshProUGUI ammoText;



    //variables del jugador
    public Animator anim;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero; //direccion del mov
    float rotationX = 0;

    public bool canMove = true;

    [SerializeField] private AudioSource SFX; //audio
    [SerializeField] private AudioClip playerWalking;


    CharacterController characterController; //componente character controller
    void Start() //obtiene el componenete y oculta el cursor en el primer frame
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentHealth = maxHealth;
        targetFill = 1f;
        gameOverPanel.SetActive(false);
        UpdateHealthBar();
        audioSource = GetComponent<AudioSource>();

        UpdateAmmoUI();
    }

    public void Movement()
    {


        //reproduce el sonido de caminar del jugador cuando la condicion de caminar es verdadera



        #region Handles Movment //para que sea mas comoda la lectura en el inspector
        // movimiento del jugador
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        anim.SetFloat("VelX", moveDirection.x);
        anim.SetFloat("VelY", moveDirection.y);

        // aumenta la velocidad si presiono Left Shift
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY); //calcula nueva direccion de mov
        SFX.Play();


        #endregion //termina la region de mov del personaje


        characterController.Move(moveDirection * Time.deltaTime);  //rotacion del personaje
    }

    public void Rotate()
    {
        //si el persoanje se mueve, me permite rotar, mirar de arriba a abajo
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); //girar izq a der
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100, eneemymask))
            {
                hit.collider.gameObject.GetComponentInParent<BaseClassEnemy>().TakeDamage(damage);
            }
            audioSource.clip = shootSound;
            audioSource.Play();
            ammo--;
            UpdateAmmoUI();
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

    public void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + ammo;
        }
    }

    public void RechargeAmmo(int amount)
    {
        ammo = Mathf.Clamp(ammo + amount, 0, 100);
    }
}