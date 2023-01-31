using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimatedText : MonoBehaviour
{
    private void Awake()
    {
        Vector3 baseScale = transform.localScale;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(baseScale * 1.2f, 0.3f));
        sequence.SetLoops(-1, LoopType.Yoyo);
    }
}
