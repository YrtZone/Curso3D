using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    //adicionando componentes
    [Header("Components")]
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask colisionLayer;


    // adicionando variaveis
    [Header("Variables")]
    public float velocity = 5f;
    private bool isGround;
    private float yForce; // força do pulo (força y)


  
    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

void Update()
{
	Move();
	Jump();
}


public void Move()

{
	Debug.Log("Executando movimento do personagem ...");
	float horizontal = 0f;
	float vertical = 0f;
    if(Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) horizontal -=1;
    if(Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) horizontal +=1;

    if(Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vertical -=1;
    if(Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vertical +=1;
	
	// Normalizar a velocidade de movimento em diagonal 
	Vector3 movimento = new Vector3(horizontal, 0, vertical);
	movimento = Vector3.ClampMagnitude(movimento, 1f);
	
	movimento = myCamera.TransformDirection(movimento);
	movimento.y = 0;
	
	controller.Move(movimento * Time.deltaTime * velocity);
	if(movimento != Vector3.zero)
	{
		transform.rotation = Quaternion.Slerp(
			transform.rotation, Quaternion.LookRotation(movimento),
			Time.deltaTime * 10f
		);
	}
	animator.SetBool("Move", movimento != Vector3.zero);
	isGround = Physics.CheckSphere(foot.position, 0.3f, colisionLayer);
	animator.SetBool("isGround", isGround);
	}
	public void Jump()
	{	
		Debug.Log("Chão" + isGround);

		if(Keyboard.current.spaceKey.wasPressedThisFrame && isGround)
        {
             yForce = 5f;
            animator.SetTrigger("Jump");
        }
		if (yForce > -9.81f)
		{
			yForce += -9.81f * Time.deltaTime;
		}
		controller.Move(new Vector3(0, yForce, 0) * Time.deltaTime);

	}
}