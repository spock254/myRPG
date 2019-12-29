using UnityEngine;
using System.Collections;

public class TurnToWall : MonoBehaviour {

	public GameManager Game;
	
	//[SerializeField]
	//NPCAStar npc;

	bool isWall;

	//private void OnMouseDown()
	//{
	//	string[] splitter = this.gameObject.name.Split(',');

	//	if (isWall == false) 
	//	{
	//		npc.endGridPosition = new NPCAStar.GridPosition(int.Parse(splitter[0]), int.Parse(splitter[1]));
	//		Debug.Log("RESED" + npc.endGridPosition.x.ToString() + " : " + npc.endGridPosition.y.ToString());
	//	}
	//}

	//void OnMouseDown()
	//{
	//	string[] splitter = this.gameObject.name.Split(',');

	//	if (!isWall)
	//	{
	//		Game.AddWall(int.Parse(splitter[0]), int.Parse(splitter[1]));
	//		isWall = true;
	//		this.GetComponent<Renderer>().material.color = Color.red;
	//	}
	//	else
	//	{
	//		Game.RemoveWall(int.Parse(splitter[0]), int.Parse(splitter[1]));
	//		isWall = false;
	//		this.GetComponent<Renderer>().material.color = Color.white;
	//	}
	//}
}
