using UnityEditor.U2D.Animation;
using UnityEngine;

public static class Bootstrapper 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Setup()
    {
        SetupPoolingSystem();
        //SetupGameStateMachine();
    }

    private static void SetupPoolingSystem()
    {
        var gameObject = new GameObject("[POOLING SYSTEM]", typeof(PoolingSystem));
    }

    private static void SetupGameStateMachine()
    {
        var gameManager = new GameObject("[GAME MANAGER]", typeof(GameManager));
    }
}
