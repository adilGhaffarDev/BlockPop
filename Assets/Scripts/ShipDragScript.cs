using UnityEngine;
using System.Collections;

public class ShipDragScript: MonoBehaviour
{


	private Vector3 startPosition;
	private Transform rectTransform;

	GameObject bullet;

	public float shootingRate = 0.2f;
	private float shootCooldown;

	//	public GameObject immidiateParentGO;


	void Start () 
	{
		bullet = (GameObject) Resources.Load("Prefabs/bullet");
		rectTransform = gameObject.GetComponent<Transform> ();
		shootCooldown = shootingRate;
		//	withMaskParent.GetComponent<HorizontalLayoutGroup>().
		//		parentScript = parentToNotify.GetComponent<BaseMenu>();

		//pursePosition = rectTransform.localPosition;
	}


	void Update()
	{
		// ...

		// 6 - Make sure we are not outside the camera bounds
		var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).x;

		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
		).x;

		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
		).y;

		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
		).y;

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
			transform.position.z
		);
			
		shootCooldown -= Time.deltaTime;
		if (shootCooldown <= 0)
		{
			Fire();
			shootCooldown = shootingRate;
			Sprite temp = Resources.Load ("sound_on",typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().color = Color.cyan;//Resources.Load ("DressingScene/Body/body"+id.ToString()+".png",typeof(Sprite)) as Sprite;

		}
		else
		{
			Sprite temp = Resources.Load ("sound_on",typeof(Sprite)) as Sprite;
			GetComponent<SpriteRenderer>().color = Color.red;//Resources.Load ("DressingScene/Body/body"+id.ToString()+".png",typeof(Sprite)) as Sprite;

		}
		// End of the update method
	}


	public void OnMouseDown()
	{
		startPosition = rectTransform.position;
	}



	public void OnMouseDrag() 
	{

	//	if(!done)
//		{
		//	PlayGameObjectSound();
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 10; // select distance = 10 units from the camera
			//Debug.Log(Camera.main.ScreenToWorldPoint(mousePos));
		transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,transform.position.y,mousePos.z));
	//	}
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(gameObject.tag == "hair")//myIndex == 0)
		{

		}





	}

	void Fire()
	{
	//	if(CanAttack)
		{
			GameObject clone = (GameObject)Instantiate (bullet, transform.position, transform.rotation);

		}
	}



//	IEnumerator moveBackPlaced()
//	{
//		done = true;
//		iTween.ScaleTo(gameObject,Vector3.zero,.5f);
//		yield return new WaitForSeconds(0.5f);
//
//		StartCoroutine("goBack");
//	}
//
//	public void goBackDone()
//	{
//		done = true;
//		StartCoroutine("goBack");
//
//	}
//
//	IEnumerator goBack()
//	{
//
//
//		StopGameObjectSound();
//		transform.parent = withMaskParent.transform;
//
//		gameObject.transform.localPosition = Vector3.zero;
//		//iTween.MoveTo(gameObject,iTween.Hash("position", btnGO.transform.position, "easeType", iTween.EaseType.easeOutCirc, "loopType", iTween.LoopType.none, "time", 0.001f));
//		yield return new WaitForSeconds(0.5f);
//		//transform.parent = withMaskParent.transform;
//		transform.localScale = Vector3.one;
//		if(btnGO !=null && dragGO!=null)
//		{
//			dragGO.SetActive(false);
//			btnGO.SetActive(true);
//		}
//
//		if(!done)
//		{
//			iTween.Resume(gameObject,"punch");	
//
//		}
//		else
//		{
//			iTween.ScaleTo(gameObject,Vector3.one,.5f);
//			done = false;
//		}
//
//		if(particleEffect!=null)
//		{
//			particleEffect.Stop();
//		}
//
//	}
//	public void PlayGameObjectSound()
//	{
//		if(GetComponent<AudioSource>() != null && !GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Play();
//	}
//	public void StopGameObjectSound()
//	{
//		if(GetComponent<AudioSource>() != null && GetComponent<AudioSource>().isPlaying)
//			GetComponent<AudioSource>().Stop();
//	}
}
