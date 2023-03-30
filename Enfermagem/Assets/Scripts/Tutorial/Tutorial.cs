using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Animator _animator = default;

    private bool _canCrouch = false;
    private bool _bomb = false;

    public void ChangeAnimation()
    {
        _canCrouch = true;

        if (_canCrouch == true)
        {
           _animator.SetBool("Crouch", true);
           StartCoroutine(Bomb());
        } 
    }

    private IEnumerator Bomb()
    {
        yield return new WaitForSeconds(3);

        _bomb = true;

        if (_bomb == true)
        {
            _animator.SetBool("Crouch", false);
            _animator.SetBool("Bomb", true);
        }
    }
}
