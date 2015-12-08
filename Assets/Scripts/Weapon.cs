using UnityEngine;
using System.Collections;

using Globals;
public class Weapon : MonoBehaviour {

	#region Public Fields & Properties
	public float fireDelay;
	public float Damage = 50f;

	public Transform gunRig;
	public Transform muzzle;

	public GameObject bulletHole;

	public Camera cam;

	public GameObject FireWork;
	public GameObject Somke;

    #endregion

    #region Private Fields & Properties
	private float _fireCounter;
	private Ray _ray;
	private PlayerController _playerController;
    #endregion

    #region Getters & Setters

    #endregion

    #region System Methods
    // Use this for initialization
    private void Start()
    {
		_playerController = GetComponent<PlayerController> ();

    }

    // Update is called once per frame
    private void LateUpdate()
    {
		_ray = cam.ScreenPointToRay (new Vector3 (Screen.width * 0.5f, Screen.height * 0.5f, 0f));		

		// recalculate the gun rig orientation to fit the aiming point	
		gunRig.forward = _ray.direction;
	
		if (Input.GetButton (PlayerInput.Fire1) && _fireCounter > fireDelay) {

			muzzle.GetComponent<AudioSource>().Play();
			_fireCounter = 0f;

			RaycastHit hit;

			if(Physics.Raycast(_ray, out hit, 100f))
			{
				GameObject go = hit.collider.gameObject;
				if( go.tag == "FireWork")
				{
					Instantiate(FireWork, go.transform.position, Quaternion.AngleAxis(90,Vector3.left));
				} 
				else if( go.tag == "Smoke" )
				{					
					Instantiate(Somke, go.transform.position,Quaternion.AngleAxis(90,Vector3.left));
				}
				// calculte the health deduction, get the health component of the object
				HasHealth h = go.GetComponent<HasHealth>();
				if( h != null ){
					h.ReceiveDamage(Damage);
				}

				Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			}

		}
		
		_fireCounter += Time.deltaTime;


    }
    #endregion

    #region Custom Methods
    #endregion
}
