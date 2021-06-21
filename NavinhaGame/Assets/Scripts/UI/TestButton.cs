using UnityEngine;
using UnityEngine.UI;

public class TestButton : Button
{
    private void Update()
    {
        if (IsPressed())
        {
            Debug.Log("Pressed");
        }
        else
        {
            Debug.Log("NOT");
        }
    }
}