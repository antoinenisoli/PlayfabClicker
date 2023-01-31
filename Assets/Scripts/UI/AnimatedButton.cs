using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimatedButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float growAmount = 1.15f;
    [SerializeField] float duration = 0.15f;
    Vector3 baseScale;
    Button button;

    private void Start()
    {
        baseScale = transform.localScale;
        button = GetComponentInChildren<Button>();
    }

    public void ShakeMe()
    {
        transform.DOShakeScale(0.1f, 0.5f, 40);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button && !button.interactable)
            return;

        transform.DOScale(baseScale * growAmount, duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (button && !button.interactable)
            return;

        transform.DOScale(baseScale, duration);
    }
}
