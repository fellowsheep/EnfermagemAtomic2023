using UnityEngine;

public class ScrollbarController : MonoBehaviour
{
    [SerializeField] private GameObject _arrowScrollUp = default;
    [SerializeField] private GameObject _arrowScrollDown = default;

    private void Start()
    {
        _arrowScrollUp.SetActive(false);
        _arrowScrollDown.SetActive(false);
    }

    private void OnEnable()
    {
        _arrowScrollUp.SetActive(true);
        _arrowScrollDown.SetActive(true);
    }
}
