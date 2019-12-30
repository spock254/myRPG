﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : NPCAStar
{
    NPCTask tasks = new NPCTask();
    public GameObject npc;
    //public NPCStats stats;
    float t = 0f;
    void Start()
    {
        List<ITaskAction> taskActions1 = new List<ITaskAction>();
        taskActions1.Add(new NPCTestAction());
        taskActions1.Add(new NPCTestAction2());

        List<ITaskAction> taskActions2 = new List<ITaskAction>();
        taskActions2.Add(new NPCTestAction3());
        taskActions2.Add(new NPCTestAction3());

        List<ITaskAction> taskActions3 = new List<ITaskAction>();
        taskActions3.Add(new NPCTestAction2());
        taskActions3.Add(new NPCTestAction2());
        taskActions3.Add(new NPCTestAction2());
        taskActions3.Add(new NPCTestAction3());
        taskActions3.Add(new NPCTestAction2());
        taskActions3.Add(new NPCTestAction());


        tasks.AddTask(new Task(new NPCAStar.GridPosition(10, 10),
                                currentGridPosition,
                                new TaskDuration(3, 0), 
                                taskActions1));

        tasks.AddTask(new Task(new NPCAStar.GridPosition(2, 14),
                                currentGridPosition,
                                new TaskDuration(5, 0), 
                                taskActions2));

        tasks.AddTask(new Task(new NPCAStar.GridPosition(2, 4),
                                currentGridPosition,
                                new TaskDuration(2, 0), 
                                taskActions3));

        base.InitNPCAStar();
        endGridPosition = tasks.PeekTask().position;

        //CreateNpc();

    }
    void Update()
    {
        //TODO optim
        if (tasks.isEmpty() == false)
        {
            Task task = tasks.PeekTask();

            if (task.ComparePositions(currentGridPosition))
            {
                if (task.IsActionsFinished == false) 
                {
                    task.ProcessActions();

                    task.IsActionsFinished = true;
                }

                task.duration.Hours -= Time.deltaTime;
            
                if (task.duration.Hours > 0)
                {
                    //Debug.Log(t.ToString());
                    return;
                }

                //t = tasks.PeekTask().duration.Hours;
                tasks.RemoveLastTask();

                if (tasks.isEmpty() == false)
                {
                    endGridPosition = tasks.PeekTask().position;
                }
            }
            else if (isMoving == false)
            {
                StartCoroutine(Move());
            }
        }
    }


    void CreateNpc()
    {
        GameObject nb = (GameObject)GameObject.Instantiate(npc);
        nb.SetActive(true);
    }
}
