using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionBetweenLevels : MonoBehaviour
{
    private bool _transitionIsOpen;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (_transitionIsOpen)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void SetTransitionState(bool state)
    {
        _transitionIsOpen = state;
    }
}
