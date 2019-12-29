using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    public Sprite carUp;
    public Sprite carDown;
    public Sprite carFront;
    public Sprite carBack;

    public void DirectionAnim(ref NPCAStar.GridPosition currentGridPosition, 
							  ref MyPathNode nextNode, 
							  ref Vector2 input)  
    {
		if (nextNode.X > currentGridPosition.x)
		{
			input.x = 1;
			this.GetComponent<SpriteRenderer>().sprite = carFront;
		}
		if (nextNode.Y > currentGridPosition.y)
		{
			input.y = 1;
			this.GetComponent<SpriteRenderer>().sprite = carUp;
		}
		if (nextNode.Y < currentGridPosition.y)
		{
			input.y = -1;
			this.GetComponent<SpriteRenderer>().sprite = carDown;
		}
		if (nextNode.X < currentGridPosition.x)
		{
			input.x = -1;
			this.GetComponent<SpriteRenderer>().sprite = carBack;
		}


	}
}
