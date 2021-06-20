using System.Collections;
using UnityEngine;

public class SetOuteScreenColor : MonoBehaviour
{
    [SerializeField] private GameObject _panelToReset;

    private IEnumerator Start()
    {
        _panelToReset.SetActive(false);
        yield return null;
        _panelToReset.SetActive(true);
    }
}
