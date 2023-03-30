using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour
{
    public UnityEvent OnInteractionComplete;
    
    public virtual void Interact()
    {
        OnInteractionComplete.Invoke();
    }
}
