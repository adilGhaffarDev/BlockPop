using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateScript : MonoBehaviour 
{

	List<float> genPositions = new List<float>();
	List<Transform> baseBlocks = new List<Transform>();
	List<Transform> allBlocks = new List<Transform>();

	float screenHeight;

	GameObject BaseBlock;
	GameObject BlocksHolder;

	float toGenProb;
	Vector2 sizeOfBlock;

	int rowCount = 0;

	public Color[] ColourValues = new Color[] { 
		new Color(220f/255f, 190f/255f, 45.0f/255f),
		new Color(207f/255f, 135f/255f, 56.0f/255f),
		new Color(204f/255f, 111f/255f , 75f/255f),
		new Color(200f/255f, 100f/255f  , 110f/255f),
		new Color(200f/255f, 90.0f/255f, 145f/255f),
		new Color(180f/255f , 80f/255f , 140f/255f),
		new Color(150f/255f , 100f/255f , 180f/255f),
		new Color(130f/255f , 120f/255f , 200f/255f),
		new Color(120f/255f , 133f/255f, 218f/255f),
		new Color(122f/255f , 156f/255f, 230f/255f),
		new Color(120f/255f , 170f/255f, 230f/255f),
		new Color(130f/255f , 190f/255f, 230f/255f),
		new Color(110f/255f , 180f/255f, 220f/255f),
		new Color(130f/255f , 205f/255f, 230f/255f),
		new Color(96f/255f , 150f/255f, 210f/255f),
		new Color(100f/255f , 120f/255f, 230f/255f),
	};


	private static GenerateScript _instance;

	public static GenerateScript Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GenerateScript>();

				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}

			return _instance;
		}
	}

	void Awake() 
	{
		Debug.Log("Awake Called");
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(gameObject);
		}
	}

	void Start () 
	{
//		Debug.Log("W "+Screen.width);
//		Debug.Log("H "+Screen.height);
		Vector3 tempScre = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
		screenHeight = tempScre.y;
//		Debug.Log("Hnew "+tempScre.y);

		toGenProb = GameConstants.TO_GEN_PROB / 10;
		BaseBlock = GameObject.FindWithTag("BaseBlock");
		BlocksHolder = GameObject.FindWithTag("BlocksHolder");
	
		Vector3 rt = BaseBlock.GetComponent<Renderer>().bounds.size;

		sizeOfBlock = new Vector2(rt.x,rt.y);

		float posXF = (transform.position.x - ((sizeOfBlock.x * GameConstants.TOTAL_BLOCKS_INLINE/2)+(GameConstants.DISTANCE_BW_BLOCKS* GameConstants.TOTAL_BLOCKS_INLINE/2))) + (sizeOfBlock.x/2);
		genPositions.Add(posXF);
		for (int i = 1; i<GameConstants.TOTAL_BLOCKS_INLINE ; i++)
		{
		//	if (child.tag == "Tag")
			{
				genPositions.Add(genPositions[i-1] + sizeOfBlock.x + (GameConstants.DISTANCE_BW_BLOCKS));
			}
		}
		for(int i = 1 ; i <= GameConstants.TOTAL_BLOCKS ; i++)
		{
			GameObject blo = (GameObject) Resources.Load("Prefabs/Block"+i);
			if(blo != null)
			{
			//	Debug.Log("Block : "+blo.name);
				baseBlocks.Add(blo.transform);
			}
		}

		
	//	Debug.Log("sizeOfBlock : "+rt.y);

		for(int i = 1 ; i <= GameConstants.START_BLOCK_LENGTH ; i++)
		{
			generateLine();
		}


	
	}
	
//	// Update is called once per frame
//	void Update () 
//	{
//	
//	}

	public void generateLine()
	{
		bool gen = false;
		if(allBlocks.Count > 0)
		{
			float yPos = allBlocks[allBlocks.Count-1].position.y + sizeOfBlock.y + GameConstants.DISTANCE_BW_BLOCKS;

			for(int i = 0 ; i < genPositions.Count ; i++)
			{

				if(toGenOrNot())
				{
					int n = blockNoToGen();
					int co = getColor();
					GameObject clone = (GameObject)Instantiate (baseBlocks[n].gameObject, new Vector3(genPositions[i] , yPos , 0),Quaternion.identity);
					allBlocks.Add(clone.transform);
					clone.transform.SetParent(BlocksHolder.transform);
					int coL = co+1;
					if(coL > 15)
					{
						coL = coL-2;
					}
					else if(coL < 0)
					{
						coL = coL+2;
					}
					int bh = blockHealth();
					clone.GetComponent<BlockScript>().init(rowCount,i,bh,ColourValues[bh],Color.white);
					gen = true;
				}
			}
		}
		else
		{
			for(int i = 0 ; i < genPositions.Count ; i++)
			{
				if(toGenOrNot())
				{
					int n = blockNoToGen();
					int co = getColor();

					GameObject clone = (GameObject)Instantiate (baseBlocks[n].gameObject, new Vector3(genPositions[i] , screenHeight + sizeOfBlock.y/2, 0),Quaternion.identity);
					allBlocks.Add(clone.transform);
					clone.transform.SetParent(BlocksHolder.transform);
					int coL = co+1;
					if(coL > 15)
					{
						coL = coL-2;
					}
					else if(coL < 0)
					{
						coL = coL+2;
					}
					int bh = blockHealth();
					clone.GetComponent<BlockScript>().init(rowCount,i,bh,ColourValues[bh],Color.white);
					gen = true;
				}
			}
		}

		if(gen)
		{
			rowCount++;
		}
	}

	//probablities
	bool toGenOrNot()
	{
		bool t = (Random.value <= toGenProb);
	//	Debug.Log("prob : "+t);
		return t;
	}

	int blockNoToGen()
	{
		int r = Random.Range(0,12);
	//	Debug.Log(r);
		return r;
	}

	int getColor()
	{
		int r = Random.Range(0,15);
		//	Debug.Log(r);
		return r;
	}

	int blockHealth()
	{
		float t = Random.value;
		int r = 1;
		if(t<0.85)
		{
			r = Random.Range(1,3);
		}
		else if(t>=0.85 && t<=0.95)
		{
			r = Random.Range(4,7);
		}
		else if(t>=0.95 && t<=1.0)
		{
			r = Random.Range(8,10);
		}
		//	Debug.Log(r);
		return r;
	}

	public GameObject getBlocksHolder()
	{
		return BlocksHolder;
	}
}
