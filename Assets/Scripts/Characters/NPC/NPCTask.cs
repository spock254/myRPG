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
    public List<ITaskAction> taskActions;
    public UnityEvent OnTask = new UnityEvent();
    public bool IsActionsFinished = false;

    public Task() { }
    public Task(NPCAStar.GridPosition position, 
                NPCAStar.GridPosition currentPosition,
                TaskDuration duration, 
                List<ITaskAction> taskActions) 
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

        if (taskActions == null)
        {
            Debug.LogError("taskActions is null");
            return;
        }

        this.position = position;
        this.currentPosition = currentPosition;
        this.duration = duration;
        this.taskActions = taskActions;
    }

    public Task(NPCAStar.GridPosition position,
            NPCAStar.GridPosition currentPosition,
            List<ITaskAction> taskActions)
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

        if (taskActions == null)
        {
            Debug.LogError("taskActions is null");
            return;
        }

        this.position = position;
        this.currentPosition = currentPosition;
        this.duration = new TaskDuration();
        this.taskActions = taskActions;
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

    public bool IsTaskOver() 
    {
        return this.duration.Hours == 0
            && this.duration.Days == 0;
    }

    public void ProcessActions() 
    {
        foreach (ITaskAction action in this.taskActions)
        {
            action.Action();
        }
    }
}

public struct TaskDuration
{
    float hours;
    float days;

    public float Hours 
    { 
        get { return hours; } 
        set { if (days >= 0) { this.hours = value; } } 
    }

    public float Days
    {
        get { return days; }
        set { if (days >= 0) { this.days = value; } }
    }

    public TaskDuration(float hours, float days) 
    {
        this.hours = hours;
        this.days = days;
    }

}

public interface ITaskAction 
{
    void Action();
}