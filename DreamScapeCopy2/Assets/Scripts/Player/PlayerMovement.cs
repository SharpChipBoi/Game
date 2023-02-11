using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerMovement>(); //находит ачивмент менеджер и  пихает его в instance чьлбы мы могли использовать его класс
            }
            return PlayerMovement.instance;
        }
    }

    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    public Transform groundCheck;
    public Transform cam;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 3f;

    public float sprintSpeed;
    //public bool isSprinting = false;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Vector3 moveDir;

    public bool active;

    public bool isGrounded;

    public Animator animator;

    private GameObject inventory;

    private bool isShowing;
    public GameObject inventoryUi;
    public AudioSource step;
    public float audioCooldown;
    bool playAudio;
    private ConversationController ui;

    private void Start()
    {
        ui = ConversationController.instance;
        inventoryUi.SetActive(false); //убираем инвентарь
        if (GlobalControl.Instance.TransitionTarget != null)
            gameObject.transform.position = GlobalControl.Instance.TransitionTarget.position;
        if (GlobalControl.Instance.IsSceneBeingLoaded)//если мы выгружаем персонажа то передаем стаые значения
        {
            PlayerState.Instance.localPlayerData = GlobalControl.Instance.LocalCopyOfData;

            transform.position = new Vector3(
                            GlobalControl.Instance.LocalCopyOfData.PositionX,
                            GlobalControl.Instance.LocalCopyOfData.PositionY,
                            GlobalControl.Instance.LocalCopyOfData.PositionZ + 0.1f); //заменяем позицию персонажа на сахроненую

            GlobalControl.Instance.IsSceneBeingLoaded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //isShowing = !isShowing;
            inventoryUi.SetActive(!inventoryUi.activeSelf); //показываем/убираем инвентарь
        }

        if (ui.inDialogue)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        MovePlayer(vertical, horizontal);
        

    }
    void MovePlayer(float vertical, float horizontal)//функция позволяющая персонажу ходить
    {
        if (!active)
        {
            playAudio = false;
            return;
        }
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)//если если скорость больше 0.1 то мы подключаем анимацию и сглаживание вращения
        {
            //playAudio = true;
            animator.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //StartCoroutine(PlayAudio());
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (Input.GetKey(KeyCode.LeftShift) && direction.magnitude >= 0.1f) //если персонаж бежит, меняем анимацию на бег
            {
                animator.SetBool("isRunning", true);
                controller.Move(moveDir * sprintSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false); //если персонаж перестал бежать, меняем анимауию на походку
                animator.SetBool("isWalking", true);
                controller.Move(moveDir * speed * Time.deltaTime);
            }
        }
        else //если персонаж стоит, отключаем всю анимацию
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
       
        if (Input.GetButtonDown("Jump") && isGrounded) // если нажат пробел, меняем гравитацию 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
