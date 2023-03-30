using UnityEngine;

public class RandomPassingOut : MonoBehaviour
{
    [SerializeField] private Transform _passingOutTransform = default;

    private Vector3 _passingOutNewPosition;

    public void SetCharacterPosition(GameObject character)
    {
        character.transform.rotation = _passingOutTransform.rotation;

        _passingOutNewPosition = new Vector3(_passingOutTransform.position.x, character.transform.position.y, _passingOutTransform.position.z);
        character.transform.position = _passingOutNewPosition;
    }

    public void PassOut(GameObject character)
    {
        character.GetComponent<Animator>().SetBool("passingOut", true);
    }
}
