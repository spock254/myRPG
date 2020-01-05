using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assert
{
    public static void ASSERT_INT_LESS_THEN_ZERO(int input) 
    {
        if (input < 0)
        {
            Debug.LogError("input is less then 0");
            Application.Quit();
        }
    }

    public static void ASSERT_INTS_LESS_THEN_ZERO(int input1, int input2)
    {
        if (input1 < 0 || input2 < 0)
        {
            Debug.LogError("input1 or input2 is less then 0");
            Application.Quit();
        }
    }

    public static void ASSERT_REF_IS_NULL(System.Object obj) 
    {
        if (obj == null)
        {
            Debug.LogError("obj is null");
            Application.Quit();
        }
    }
}
