using TMPro;
using UnityEngine;

public class InteractableChoice : Interactable
{

    private Vector3 _chosen_effect = new Vector3(0.186f, 0.124f, 0.010f);
    private Vector3 _startScale = new Vector3(0.15f, 0.1f, 0.008f);

    public OnChoiceSelectedEvent OnChoiceSelected;

    [SerializeField] protected ChoiceData _choiceData = default;
    [SerializeField] private string _name = default;

    public override void Interact()
    {
        OnChoiceSelected?.Invoke(this);
    }

    public void Initialize()
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().text = _name;
    }

    public ChoiceData Data { get => _choiceData; }
    public string Name { get => _name; }

    private void OnEnable()
    {
        Initialize();
    }

    public void ChosenEffect()
    {
        transform.localScale = _chosen_effect;
    }

    public void RestartScale()
    {
        transform.localScale = _startScale;
    }
}
