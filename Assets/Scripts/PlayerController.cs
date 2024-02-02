
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : CharacterManager {

	public static PlayerController Instance
    {
        get
        {
            return s_Instance;
        }
    }


	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float gravity = -12;
	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	float currentSpeed;
	float velocityY;

	Animator animator;
	Transform cameraT;
	CharacterController controller;

	public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;
	
	private static PlayerController s_Instance;

	public float dashSpeed = 10f;
	public float dashDuration = 0.5f;
	private bool isDashing = false;

	public Camera mainCamera;
    public float fovWhileRunning = 70f; 
	float fovWalking = 60f;
	float smoothnessFactor  = 5f;

	void Start () {
		animator = GetComponent<Animator> ();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();
		s_Instance = this;
		Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true); //new
		Time.timeScale = 1;
	}

	void Update () {
		// Input
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey (KeyCode.LeftShift);

		float fovRunning = fovWhileRunning;
    	
    	// Interpolasi antara FOV saat berjalan dan FOV saat berlari
    	float targetFOV = running ? fovRunning : fovWalking;
    
   		 // Haluskan perpindahan FOV menggunakan Mathf.Lerp
   		mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * smoothnessFactor);


		Move(inputDir, running);

		if (Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}

		// Animator
		float animationSpeedPercent = ((running) ? currentSpeed/runSpeed : currentSpeed/walkSpeed * .5f);
		animator.SetFloat ("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

		// Dash
		if (Input.GetKeyDown(KeyCode.LeftControl))
    	{
        	Dash();
    	}
	}

	//? Code Reference : Sebastian Lague
	void Move(Vector2 inputDir, bool running){
		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp (currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

		controller.Move(velocity * Time.deltaTime);
		currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

		if (controller.isGrounded){
			velocityY = 0;
		}

		
	}

	void Jump(){
		if (controller.isGrounded){
			float jumpVelocity = Mathf.Sqrt(-2*gravity * jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	void Dash()
	{
    	if (!isDashing)
    	{
        	StartCoroutine(PerformDash());
			animator.SetBool("roll", true);
    	}
	}

	IEnumerator PerformDash()
	{
    isDashing = true;
    float startTime = Time.time;

    while (Time.time < startTime + dashDuration)
    {
        Vector3 dashDirection = transform.forward;
        controller.Move(dashDirection * dashSpeed * Time.deltaTime);
        yield return null;
    }

    	isDashing = false;
		animator.SetBool("roll", false);
	}

	//? Code Reference : Sebastian Lague
	float GetModifiedSmoothTime(float smoothTime){
		if (controller.isGrounded){
			return smoothTime;
		}

		if (airControlPercent == 0){
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}

	private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            animator.SetBool("hitted", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            animator.SetBool("hitted", false);
        }
    }
}
