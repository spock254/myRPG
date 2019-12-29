using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCTask
{
    Queue<Task> tasks = new Queue<Task>();

    public void AddTask(Task task) 
    {
        tasks.Enqueue(task);
    }

    public Task RemoveLastTask() 
    {
        return tasks.Dequeue();
    }

    public Task PeekTask() 
    {
        return tasks.Peek();
    }

    public bool isEmpty() 
    {
        return tasks.Count == 0;
    }
}

public class Task
{
    public NPCAStar.GridPosition position;
    public bool newPosition = false;

    public Task() { }
    public Task(NPCAStar.GridPosition position, bool newPosition) 
    {
        this.position = position;
        this.newPosition = newPosition;
    }

    public bool ComparePositions(NPCAStar.GridPosition nextPosition) 
    {
        return this.position.x == nextPosition.x 
            && this.position.y == nextPosition.y;
    }
}
