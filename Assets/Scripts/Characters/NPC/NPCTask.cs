using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SettlersEngine;

[System.Serializable]
public class NPCTask
{
    Queue<Task> tasks = new Queue<Task>();

    public void AddTask(Task task) 
    {
        if (task == null)
        {
            Debug.LogError("task is null");
            return;
        }

        tasks.Enqueue(task);
    }

    public Task RemoveLastTask() 
    {
        if (isEmpty())
        {
            Debug.LogError("tasl is empty");
            return null;
        }

        return tasks.Dequeue();
    }

    public Task PeekTask() 
    {
        if (isEmpty())
        {
            Debug.LogError("tasl is empty");
            return null;
        }

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
    public NPCAStar.GridPosition currentPosition;
    public TaskDuration duration;
    public UnityEvent OnTask = new UnityEvent();

    public Task() { }
    public Task(NPCAStar.GridPosition position, 
                NPCAStar.GridPosition currentPosition,
                TaskDuration duration) 
    {
        if (position == null)
        {
            Debug.LogError("position is null");
            return;
        }

        if (currentPosition == null)
        {
            Debug.LogError("currentPosition is null");
            return;
        }

        this.position = position;
        this.currentPosition = currentPosition;
        this.duration = duration;
    }

    public Task(NPCAStar.GridPosition position,
            NPCAStar.GridPosition currentPosition)
    {
        if (position == null)
        {
            Debug.LogError("position is null");
            return;
        }

        if (currentPosition == null)
        {
            Debug.LogError("currentPosition is null");
            return;
        }

        this.position = position;
        this.currentPosition = currentPosition;
        this.duration = new TaskDuration();
    }

    public void UpdateCurrentPosition(NPCAStar.GridPosition currentPosition) 
    {
        this.currentPosition = currentPosition;
    }

    public bool ComparePositions(NPCAStar.GridPosition nextPosition) 
    {
        if (nextPosition == null)
        {
            Debug.LogError("newxp position is null");
            return false;
        }
        return this.position.x == nextPosition.x 
            && this.position.y == nextPosition.y;
    }

    public void StartTask() 
    {
        if (ComparePositions(currentPosition) == true)
        {
            //start dicris duration TODO
            OnTask.Invoke();
        }
    }

    // Coroutine task execution
    //public IEnumerator StartTaskExec() 
    //{
    //    Debug.Log("TASK EXECUTION");
    //    yield return new WaitForSeconds(5.1f);
    //}

    public bool IsTaskOver() 
    {
        return this.duration.Hours == 0
            && this.duration.Days == 0;
    }
}

public struct TaskDuration
{
    int hours;
    int days;

    public int Hours 
    { 
        get { return hours; } 
        set { if (days >= 0) { this.hours = value; } } 
    }

    public int Days
    {
        get { return days; }
        set { if (days >= 0) { this.days = value; } }
    }

    public TaskDuration(int hours, int days) 
    {
        this.hours = hours;
        this.days = days;
    }

}
