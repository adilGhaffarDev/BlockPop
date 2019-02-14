using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour 
{
	int myCol = 0;
	int myRow = 0;
	int myHealth = 0;
	SpriteRenderer _spriteRenderer;
	TextMesh health;
	ParticleSystem destroyParticle;
	// Use this for initialization
	void Start () 
	{
		

	}

	public void init(int mr, int mc, int mh, Color c, Color d)
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();

		health = transform.GetChild(0).GetComponent<TextMesh>();
//		Debug.Log(health.name);
		health.transform.localScale = new Vector3(0.04f,0.04f,0.04f);
		GameObject blo = (GameObject) Resources.Load("Prefabs/DestParticle");
		_spriteRenderer.color = c;
		health.color = d;
		destroyParticle = blo.GetComponent<ParticleSystem>();
		setRow(mr);
		setColumn(mc);
		setHealth(mh);
	}

	public void setColumn(int co)
	{
		myCol = co;
	}

	public int getColumn()
	{
		return myCol;
	}

	public void setRow(int ro)
	{
		myRow = ro;
	}

	public int getRow()
	{
		return myRow;
	}

	public void setHealth(int hl)
	{
		myHealth = hl;
		health.text = hl.ToString();
	}

	public int getHealth()
	{
		return myHealth;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//	hitMarker = (GameObject) Instantiate (hit, transform.position + Vector3.forward * 3f, Quaternion.identity);
		if(other.tag == "Bullet")
		{
			if(myHealth>1)
			{
				myHealth--;
				setHealth(myHealth);
				iTween.PunchScale(gameObject,Vector3.one*2,0.2f);
				SoundManager.Instance.PlayClickSound();
				_spriteRenderer.color = GenerateScript.Instance.ColourValues[myHealth];
			//	health.color = GenerateScript.Instance.ColourValues[15-myHealth];
			}
			else
			{
				ParticleSystem hitMarker = (ParticleSystem) Instantiate (destroyParticle, transform.position,transform.rotation);
				iTween.PunchScale(gameObject,Vector3.one*5,0.2f);
				SoundManager.Instance.PlayClickSound();

				Destroy(hitMarker.gameObject,3);
				Destroy(gameObject,0.1f);

			}
		}
	}

}
