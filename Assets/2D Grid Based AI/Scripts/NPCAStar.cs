using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NPCAStar : MonoBehaviour {

	public GameManager Game;

	public MyPathNode nextNode;

	public GridPosition currentGridPosition = new GridPosition();
	public GridPosition startGridPosition = new GridPosition();
	public GridPosition endGridPosition = new GridPosition();

	private Orientation gridOrientation = Orientation.Vertical;
	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	private bool isMoving = true;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;
	private Color myColor;

	public NPCStats stats;
	public NPCAnimationController animationController;
	NPCTask tasks = new NPCTask();

	public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, 
						  TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
	{
		protected override Double Heuristic(PathNode inStart, PathNode inEnd)
		{

			int distance = 2;
			int formula = distance;
			int dx = Math.Abs (inStart.X - inEnd.X);
			int dy = Math.Abs(inStart.Y - inEnd.Y);

			if(formula == 0)
				return Math.Sqrt(dx * dx + dy * dy); //Euclidean distance

			else if(formula == 1)
				return (dx * dx + dy * dy); //Euclidean distance squared

			else if(formula == 2)
				return Math.Min(dx, dy); //Diagonal distance

			else if(formula == 3)
				return (dx*dy)+(dx + dy); //Manhatten distance

		

			else 
				return Math.Abs (inStart.X - inEnd.X) + Math.Abs (inStart.Y - inEnd.Y);

		}
		
		protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
		{
			return Heuristic(inStart, inEnd);
		}

		public MySolver(TPathNode[,] inGrid)
			: base(inGrid)
		{
		}
	} 

	void Start () 
	{
		//test
		tasks.AddTask(new Task(new GridPosition(10, 10), 
								currentGridPosition, 
								new TaskDuration(0, 0)));
		tasks.AddTask(new Task(new GridPosition(2, 14),
								currentGridPosition,
								new TaskDuration(0, 0)));
		tasks.AddTask(new Task(new GridPosition(2, 4),
								currentGridPosition,
								new TaskDuration(14, 12)));


		myColor = getRandomColor();

		startGridPosition = new GridPosition(5,5);
		//test
		endGridPosition = tasks.PeekTask().position;
		//endGridPosition = startGridPosition;
		//endGridPosition = new gridPosition(Game.gridWidth-1,UnityEngine.Random.Range(0,Game.gridHeight-1));
		initializePosition ();

		MySolver<MyPathNode, System.Object> aStar = new MySolver<MyPathNode, System.Object>(Game.gridMap.grid);
		IEnumerable<MyPathNode> path = aStar.Search(new Vector2(startGridPosition.x, startGridPosition.y), new Vector2(endGridPosition.x, endGridPosition.y), null);

		foreach(GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
		{
			g.GetComponent<Renderer>().material.color = Color.white;
		}


		UpdatePath();

		this.GetComponent<Renderer>().material.color = myColor;
	}


	public void findUpdatedPath(int currentX,int currentY)
	{
		MySolver<MyPathNode, System.Object> aStar = new MySolver<MyPathNode, System.Object>(Game.gridMap.grid);
		IEnumerable<MyPathNode> path = aStar.Search(new Vector2(currentX, currentY), new Vector2(endGridPosition.x, endGridPosition.y), null);

		int x = 0;

		if (path != null) {
		
			foreach (MyPathNode node in path)
			{
				if (x==1)
				{
					nextNode = node;
					break;
				}

				x++;
			}

			foreach(GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
			{
				if(g.GetComponent<Renderer>().material.color != Color.red && g.GetComponent<Renderer>().material.color == myColor) 
				{ 
					g.GetComponent<Renderer>().material.color = Color.white;
				}
			}

			foreach (MyPathNode node in path)
			{
				GameObject.Find(node.X + "," + node.Y).GetComponent<Renderer>().material.color = myColor;
			}
		}
	}

	Color getRandomColor()
	{
		Color tmpCol = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
		return tmpCol;

	}
	
	

	void Update () 
	{
		//test

		if (!isMoving) 
		{
			StartCoroutine(Move());
		}
	}
	
	public class GridPosition
	{
		public int x =0;
		public int y=0;

		public GridPosition()
		{
		}

		public GridPosition(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};
	
	
	private enum Orientation 
	{
		Horizontal,
		Vertical
	};

	
	public IEnumerator Move() 
	{
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if (tasks.PeekTask().ComparePositions(currentGridPosition))
		{
			Debug.Log("IN");
			tasks.RemoveLastTask();
			endGridPosition = tasks.PeekTask().position;
		}

		if (gridOrientation == Orientation.Horizontal) 
		{
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridMap.gridSize,
			                          startPosition.y);
			currentGridPosition.x += System.Math.Sign(input.x);
		} 
		else 
		{
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridMap.gridSize,
			                          startPosition.y + System.Math.Sign(input.y) * Game.gridMap.gridSize);
			
			currentGridPosition.x += System.Math.Sign(input.x);
			currentGridPosition.y += System.Math.Sign(input.y);
		}
		
		if (allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) 
		{
			factor = 0.9071f;
		} 
		else 
		{
			factor = 1f;
		}

	
		while (t < 1f) 
		{
			t += Time.deltaTime * (stats.MoveSpeed / Game.gridMap.gridSize) * factor;
			transform.position = Vector2.Lerp(startPosition, endPosition, t);
			yield return null;
		}
		
		isMoving = false;
		getNextMovement ();
		
		yield return 0;

	}
	
	void UpdatePath()
	{
		findUpdatedPath(currentGridPosition.x, currentGridPosition.y);
	}
	
	void getNextMovement()
	{
		UpdatePath();
		
		input.x = 0;
		input.y = 0;

		if (currentGridPosition != null && nextNode != null)
		{
			animationController.DirectionAnim(ref currentGridPosition, 
											  ref nextNode, 
											  ref input);

			StartCoroutine(Move());
		}
	}
	
	public Vector2 GetGridPosition(int x, int y)
	{
		float contingencyMargin = Game.gridMap.gridSize * 0.10f;
		float posX = Game.gridMap.gridBox.transform.position.x + (Game.gridMap.gridSize*x) - contingencyMargin;
		float posY = Game.gridMap.gridBox.transform.position.y + (Game.gridMap.gridSize*y) + contingencyMargin;
		
		return new Vector2(posX,posY);	
	}
	
	
	public void initializePosition()
	{
		this.gameObject.transform.position = GetGridPosition (startGridPosition.x, startGridPosition.y);
		currentGridPosition.x = startGridPosition.x;
		currentGridPosition.y = startGridPosition.y;
		isMoving = false;
		GameObject.Find(startGridPosition.x + "," + startGridPosition.y).GetComponent<Renderer>().material.color = Color.black; 

	}
	


}
