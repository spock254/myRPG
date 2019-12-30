using System.Collections;
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
        tasks.AddTask(new Task(new NPCAStar.GridPosition(10, 10),
                                currentGridPosition,
                                new TaskDuration(3, 0)));
        tasks.AddTask(new Task(new NPCAStar.GridPosition(2, 14),
                                currentGridPosition,
                                new TaskDuration(0, 0)));
        tasks.AddTask(new Task(new NPCAStar.GridPosition(2, 4),
                                currentGridPosition,
                                new TaskDuration(2, 0)));

        base.InitNPCAStar();
        endGridPosition = tasks.PeekTask().position;

        //CreateNpc();
    t = tasks.PeekTask().duration.Hours;
    }
    void Update()
    {
        if (tasks.PeekTask().ComparePositions(currentGridPosition))
        {
            //Debug.Log("IN");
            // StartCoroutine(tasks.PeekTask().StartTaskExec());
            InvokeRepeating("Print", tasks.PeekTask().duration.Hours, 10);
            t -= Time.deltaTime;
            
            if (t > 0)
            {
                Debug.Log(t.ToString());
                return;
            }

            t = tasks.PeekTask().duration.Hours;
            tasks.RemoveLastTask();
            endGridPosition = tasks.PeekTask().position;
        }
        else if (isMoving == false)
        {
            StartCoroutine(Move());
        }
    }

    void Print() 
    {
        Debug.Log("Print");
    }

    void CreateNpc()
    {
        GameObject nb = (GameObject)GameObject.Instantiate(npc);
        nb.SetActive(true);
    }
}
