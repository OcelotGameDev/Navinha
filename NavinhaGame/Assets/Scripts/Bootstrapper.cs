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
        var gameObject = new GameObject("[Pooling System]", typeof(PoolingSystem));
    }
}
