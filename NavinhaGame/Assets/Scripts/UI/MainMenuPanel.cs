public class MainMenuPanel : AbstractPanel<Menu>
{
    protected override void Awake()
    {
        base.Awake();
        _panel.SetActive(true);
    }

    protected override void HandleGameStateChanged(IState state)
    {
        _panel.SetActive(!(state is Options));
    }
}