using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartingLoadingPanel : MonoBehaviour
{
    public Action CountdownIsOverEvent;
    [SerializeField] private Text _text;

    public IEnumerator  StartCountdown()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 3; i > 0; i--)
        {
            _text.text = i.ToString();
            DOTween.Sequence()
                .SetUpdate(UpdateType.Normal, true)
                .Append(_text.transform.DOScale(4, 0.4f))
                .Append(_text.transform.DOScale(1, 0.4f));
            yield return new WaitForSeconds(1f);
        }
        CountdownIsOverEvent?.Invoke();
        gameObject.SetActive(false);
    }
    
}
