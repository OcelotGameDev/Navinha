public class OptionsPanel : AbstractPanel<Options>
{
    protected override void HandleGameStateChanged(IState state)
    {
        _panel.SetActive(state is Options);
    }
}
