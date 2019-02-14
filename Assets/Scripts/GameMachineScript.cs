using UnityEngine;
using System.Collections;

public class GameMachineScript : MonoBehaviour 
{
	GenerateScript GS;
	GameObject blocksHolder;

	float duration = GameConstants.MOVE_TIME; //set the duration in the inspector
	float elapsedTime = 0;

	GameObject BaseBlock;

	Vector2 sizeOfBlock;
	Vector3 targetPos;
	float speed = 5;
	// Use this for initialization
	void Start () 
	{
		//Generator
		GS = GameObject.FindWithTag("Generator").GetComponent<GenerateScript>();
		blocksHolder = GameObject.FindWithTag("BlocksHolder");// GS.getBlocksHolder();
		BaseBlock = GameObject.FindWithTag("BaseBlock");


		Vector3 rt = BaseBlock.GetComponent<Renderer>().bounds.size;

		sizeOfBlock = new Vector2(rt.x,rt.y);

		targetPos = new Vector3( blocksHolder.transform.position.x, blocksHolder.transform.position.y-rt.y , blocksHolder.transform.position.z);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (Input.GetKeyDown(KeyCode.Space)) {
//			elapsedTime = 0f;
//		}
		duration -= Time.deltaTime;
		if (duration <= 0) 
		{
		//	blocksHolder.transform.Translate(Vector3.down*Time.deltaTime);
			blocksHolder.transform.position = Vector3.MoveTowards(blocksHolder.transform.position, targetPos, speed * Time.deltaTime);
			//elapsedTime += Time.deltaTime;
		//	duration = GameConstants.MOVE_TIME;
			if( blocksHolder.transform.position == targetPos)
			{
				GS.generateLine();
				targetPos = new Vector3( blocksHolder.transform.position.x, blocksHolder.transform.position.y-sizeOfBlock.y , blocksHolder.transform.position.z);
				duration = GameConstants.MOVE_TIME;
			}
		}
		else
		{
			//duration = GameConstants.MOVE_TIME;
		}
	
	}


}
