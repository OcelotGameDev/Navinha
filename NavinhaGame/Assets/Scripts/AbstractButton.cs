using UnityEngine.UI;

public abstract class AbstractButton<T> : Button where T : AbstractButton<T>
{
    protected static T _instance;

    public static bool Pressed => _instance != null && _instance.IsPressed();

    protected override void OnEnable()
    {
        _instance = (T)this;
        base.OnEnable();
    }
}
