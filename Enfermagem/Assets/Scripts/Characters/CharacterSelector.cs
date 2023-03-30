using System;
using System.Collections;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public Action<int> OnCharacterSelected;

    [SerializeField] private GameObject[] _people = default;

    private int _randomPersonIndex;

    public GameObject SelectCharacter()
    {
        _randomPersonIndex = UnityEngine.Random.Range(0, 3);
        OnCharacterSelected?.Invoke(_randomPersonIndex);

        return _people[_randomPersonIndex];
    }
}
