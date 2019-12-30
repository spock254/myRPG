using UnityEngine;


public class GameManager : MonoBehaviour 
{
	public Grid gridMap = new Grid();

	//public static string distanceType;
	//public static int distance = 2;

	void Awake () 
	{
		gridMap.CreateGridNodes();
		gridMap.CreateGrid();
	}

}
