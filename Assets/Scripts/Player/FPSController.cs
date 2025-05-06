using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] //necesita que el game object tenga un character controller
public class FPScontroller : MonoBehaviour //controla el mov y rotacion del personaje en primera persona
{
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
    }

    void Update() 
    {
        if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") != 0)
        {

            //reproduce el sonido de caminar del jugador cuando la condicion de caminar es verdadera
            SFX.Play();
         
        }
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

       
       
            #endregion //termina la region de mov del personaje


            #region Handles Rotation //rotacion del personaje
            characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            //si el persoanje se mueve, me permite rotar, mirar de arriba a abajo
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); //girar izq a der
        }

        #endregion
    }
}
