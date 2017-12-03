using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour{

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	private Rigidbody ribody;

	void Start()
	{
		
	}
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 120.0f ;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 4.0f  ;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	[Command]
	void CmdFire ()
	{
		//Create the bullet from the prefab
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		//add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*20.0f;

		//Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		//Destroy the bullet after 2 seconds
		Destroy(bullet,2); 
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

}
