using UnityEngine;


public class GameManager : MonoBehaviour 
{
	public GameObject npc;

	public Grid gridMap = new Grid();

	//public static string distanceType;
	//public static int distance = 2;

	void Start () 
	{

		gridMap.CreateGridNodes();
		//instantiate grid gameobjects to display on the scene
		gridMap.CreateGrid();

		//instantiate enemy object
		CreateEnemy ();
	}

	void CreateEnemy()
	{
		GameObject nb = (GameObject)GameObject.Instantiate(npc);
		nb.SetActive (true);
	}


}
