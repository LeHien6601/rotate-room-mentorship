
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class NPCPlayerScript : MonoBehaviour
{
	bool inScript = false; 
	int num = 0;
	[SerializeField] Vector2 pos;
	NPCPlayer npcplayer;
	void Start()
	{
		npcplayer = GetComponent<NPCPlayer>();
	}
	void Update()
	{
		if(!inScript)
		{
			string name = "Script";
			name += num;
			// Debug.Log(name);
			
			npcplayer.Reset();
			gameObject.transform.position = pos;
			StartCoroutine(name);
			num = (num+1)%5;
		}
	}
	IEnumerator Script0()
	{
		inScript = true;
		Debug.Log("script0");
		yield return new WaitForSeconds(1f);
		npcplayer.rotateLeft();
		yield return new WaitForSeconds(.5f);
		npcplayer.rotateLeft();
		yield return new WaitForSeconds(2);
		inScript = false;
	}
	IEnumerator Script1()
	{
		inScript = true;
		Debug.Log("script1");
		yield return new WaitForSeconds(1f);
		npcplayer.rotateLeft();
		yield return new WaitForSeconds(.5f);
		npcplayer.rotateRight();
		
		yield return new WaitForSeconds(2);
		inScript = false;
	}
	IEnumerator Script2()
	{
		inScript = true;
		Debug.Log("script2");
		yield return new WaitForSeconds(1f);
		npcplayer.rotateRight();
		
		yield return new WaitForSeconds(2);
		inScript = false;
	}
	IEnumerator Script3()
	{
		inScript = true;
		Debug.Log("script3");
		yield return new WaitForSeconds(1f);
		npcplayer.rotateLeft();
		
		yield return new WaitForSeconds(2);
		inScript = false;
	}
	IEnumerator Script4()
	{
		inScript = true;
		Debug.Log("script4");
		yield return new WaitForSeconds(1f);
		npcplayer.rotateRight();
		yield return new WaitForSeconds(.5f);
		npcplayer.rotateRight();		
		yield return new WaitForSeconds(2);
		inScript = false;
	}
}