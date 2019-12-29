using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Grid
{
    public MyPathNode[,] grid;
    public GameObject gridBox;

    public int gridWidth;
    public int gridHeight;

    public float gridSize;

	public void CreateGridNodes() 
	{
		//Generate a grid - nodes according to the specified size
		grid = new MyPathNode[gridWidth, gridHeight];

		for (int x = 0; x < gridWidth; x++)
		{
			for (int y = 0; y < gridHeight; y++)
			{
				grid[x, y] = new MyPathNode()
				{
					IsWall = false,
					X = x,
					Y = y,
				};
			}
		}
	}

	public void CreateGrid()
	{
		for (int i = 0; i < gridHeight; i++)
		{
			for (int j = 0; j < gridWidth; j++)
			{
				GameObject nobj = (GameObject)GameObject.Instantiate(gridBox);
				nobj.transform.position = new Vector2(gridBox.transform.position.x + (gridSize * j), gridBox.transform.position.y + (0.87f * i));
				nobj.name = j + "," + i;

				nobj.gameObject.transform.parent = gridBox.transform.parent;
				nobj.SetActive(true);

			}
		}
	}

	public void AddWall(int x, int y)
	{
		grid[x, y].IsWall = true;
	}

	public void RemoveWall(int x, int y)
	{
		grid[x, y].IsWall = false;
	}
}
