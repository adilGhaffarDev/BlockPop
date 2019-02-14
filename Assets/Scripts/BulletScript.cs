using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{

	GameObject hit, hitMarker;
	float strength = 0;
//	GameObject myShip;

	//StartFunction
	void Start(){
		hit = (GameObject) Resources.Load ("Prefabs/Particles/Hit");
	}

	//Fixed UpdateFunction
	void FixedUpdate () {
		transform.Translate (0f, 15*Time.deltaTime, 0f);
	}

	//Called upon trigger activation. Spawns hit particle / destroys laser
	void OnTriggerEnter2D(Collider2D other)
	{
	//	hitMarker = (GameObject) Instantiate (hit, transform.position + Vector3.forward * 3f, Quaternion.identity);
		if(other.tag == "Block")
		{
			Destroy (this.gameObject);
		}
	}

	public void setStrength(float st)
	{
		strength = st;
	}

	public float getStrength()
	{
		return strength;
	}

//	public void setShip(GameObject ship)
//	{
//	//	myShip = ship;
//	}
//
//	public GameObject getShip()
//	{
//		return myShip;
//	}
}
