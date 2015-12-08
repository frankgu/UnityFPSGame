using UnityEngine;
using System.Collections;

using Globals;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HeadLookController))]
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Weapon))]

public class PlayerController : MonoBehaviour
{

    #region Public Fields & Properties
	public float idleTimer;

	public float runSpeed = 4.6f;
	public float runStrafeSpeed = 3.07f;
	public float walkSpeed = 1.22f;
	public float walkStrafeSpeed = 1.22f;
	public float maxRotationSpeed = 540f;

	[HideInInspector]
	public float targetYRotation;
	[HideInInspector]
	public bool walk;
	[HideInInspector]
	public bool inAir;
	[HideInInspector]
	public bool aim;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public Vector3 moveDir;

    #endregion

    #region Private Fields & Properties
	private Transform _playerTransform;
	private CharacterController _controller;
	private CharacterMotor _motor;
	private float stepTimer;
	private float stepCool;
    #endregion

    #region Getters & Setters

    #endregion

    #region System Methods
    // Use this for initialization
    private void Start()
    {
		idleTimer = 0f;
		_playerTransform = transform;
		walk = true;
		aim = false;
		_controller = GetComponent<CharacterController> ();
		_motor = GetComponent<CharacterMotor> ();
		_controller.center = new Vector3 (0f, 1f, 0f);
		stepCool = 0.55f;
    }

    // Update is called once per frame
    private void Update()
    {
		GetUserInput ();
		if (!_motor.canControl) 
		{
			_motor.canControl = true;
		}

		moveDir = new Vector3(Input.GetAxis(PlayerInput.Horizontal),0f,Input.GetAxis(PlayerInput.Vertical));
		if (moveDir.sqrMagnitude > 1f)
			moveDir = moveDir.normalized;
		_motor.inputMoveDirection = _playerTransform.TransformDirection (moveDir);
		_motor.inputJump = Input.GetButtonDown (PlayerInput.Jump);

		_motor.movement.maxForwardSpeed = (walk) ? walkSpeed : runSpeed;
		_motor.movement.maxBackwardsSpeed = _motor.movement.maxForwardSpeed;
		_motor.movement.maxSidewaysSpeed = (walk) ? walkStrafeSpeed : runStrafeSpeed;

		if (moveDir != Vector3.zero) 
		{
			MoveSound();
			idleTimer = 0f;
		}
		inAir = !_motor.grounded;
		grounded = !inAir;

		float currentAngle = _playerTransform.localRotation.eulerAngles.y;
		float delta = Mathf.Repeat ((targetYRotation - currentAngle), 360f);

		if (delta > 180f)
			delta -= 360f;

		float newYRot = Mathf.MoveTowards (currentAngle, 
		                                   currentAngle + delta, 
		                                   Time.deltaTime * maxRotationSpeed);
		Vector3 newLocalRot = new Vector3 (_playerTransform.localRotation.eulerAngles.x, 
		                                   newYRot, _playerTransform.localRotation.eulerAngles.z);
		_playerTransform.localRotation = Quaternion.Euler (newLocalRot);

		if(Input.GetButton(PlayerInput.Run))
		{
			stepCool = 0.35f;
			MoveSound();
		} else {
			stepCool = 0.55f;
		}
		if (stepTimer > 0)
			stepTimer -= Time.deltaTime;
		if (stepTimer < 0)
			stepTimer = 0;
    }
    #endregion

    #region Custom Methods
    private void GetUserInput()
	{
		aim = Input.GetButton (PlayerInput.Fire2);
		idleTimer += Time.deltaTime;
		walk = (!Input.GetButton(PlayerInput.Run)
		        ||moveDir == Vector3.zero
		        ||Input.GetAxis(PlayerInput.Vertical)<0f);

	}

	private void MoveSound()
	{
		if(stepTimer == 0)
		{
			GetComponent<AudioSource>().Play();
			stepTimer = stepCool;
		}
	}
    #endregion
}
