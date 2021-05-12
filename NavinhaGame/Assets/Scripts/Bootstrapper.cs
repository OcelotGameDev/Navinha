using UnityEngine;

public static class Bootstrapper 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Setup()
    {
        SetupPoolingSystem();
    }

    private static void SetupPoolingSystem()
    {
        if (Object.FindObjectOfType<PoolingSystem>() == null)
        {
            var gameObject = new GameObject("[POOLING SYSTEM]", typeof(PoolingSystem));
        }
    }

    private static void SetupGameStateMachine()
    {
        if (Object.FindObjectOfType<GameManager>() == null)
        {
            var gameManager = new GameObject("[GAME MANAGER]", typeof(GameManager));
        }
    }
}
