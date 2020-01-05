using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SettlersEngine;
using System;

[System.Serializable]
public class NPCTask
{
    PriorityQueue<Task> tasks;

    private bool isDefault = false;

    public NPCTask(Task defaultTask) 
    {
        isDefault = true;
        tasks = new PriorityQueue<Task>();

        if (defaultTask == null)
        {
            Debug.LogError("defaultTask is null");
        }

        AddTask(defaultTask);
    }

    public NPCTask() 
    {
        tasks = new PriorityQueue<Task>();
    }

    /* must be the last task */
    public void AddDefaultTask(Task defaultTask) 
    {
        if (defaultTask == null)
        {
            Debug.LogError("defaultTask is null");
            return;
        }

        isDefault = true;

        tasks.Enqueue(defaultTask);
    }

    public void AddTask(Task task) 
    {
        if (task == null)
        {
            Debug.LogError("task is null");
            return;
        }

        //if (isDefault == true)
        //{
        //    tasks.Enqueue(RemoveLastTask());
        //}

        tasks.Enqueue(task);
    }

    public Task RemoveLastTask() 
    {
        if (IsEmpty())
        {
            Debug.LogError("task is empty");
            return null;
        }

        return tasks.Dequeue();
    }

    public Task PeekTask() 
    {
        if (IsEmpty())
        {
            Debug.LogError("tasl is empty");
            return null;
        }

        return tasks.Peek();
    }

    public bool IsEmpty() 
    {
        return tasks.Count() == 0;
    }

    public bool HasDefaultTask() 
    {
        return isDefault;
    }

    public bool IsDefaultTask() 
    {
        return tasks.Count() == 1 
            && HasDefaultTask() == true;
    }
}

public class Task : IComparable<Task>
{
    public NPCAStar.GridPosition position;
    public NPCAStar.GridPosition currentPosition;

    public TaskDuration duration;

    public List<ITaskAction> taskActions;

    public UnityEvent OnTask = new UnityEvent();

    public bool IsActionsFinished = false;

    private int taskIndex = -1;

    public Task() { }
    public Task(NPCAStar.GridPosition position, 
                NPCAStar.GridPosition currentPosition,
                TaskDuration duration, 
                List<ITaskAction> taskActions,
                int taskIndex) 
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
        this.taskIndex = taskIndex;
    }
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
        this.taskIndex = -1;
    }


    public Task(NPCAStar.GridPosition position,
            NPCAStar.GridPosition currentPosition,
            List<ITaskAction> taskActions,
            int taskIndex)
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
        this.taskIndex = taskIndex;
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
        this.taskIndex = -1;
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

    public int CompareTo(Task other)
    {
        return this.taskIndex - other.taskIndex;
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