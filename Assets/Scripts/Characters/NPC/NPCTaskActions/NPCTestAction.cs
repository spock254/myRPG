using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTestAction : ITaskAction
{
    public void Action()
    {
        Debug.Log("Action 1");
    }
}

public class NPCTestAction2 : ITaskAction
{
    public void Action()
    {
        Debug.Log("Action 2");
    }
}

public class NPCTestAction3 : ITaskAction
{
    public void Action()
    {
        Debug.Log("Action 3");
    }
}

public class NPCDefaultAction : ITaskAction 
{
    public void Action()
    {
        Debug.Log("Default action");
    }
}