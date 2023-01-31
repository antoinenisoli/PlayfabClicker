using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour
{
    [SerializeField] Text countText;
    [SerializeField] Image buttonImage;

    public void Feedback() //called by button
    {
        buttonImage.color = Random.ColorHSV();

        transform.DOComplete();
        transform.DOShakePosition(0.15f, 15f, 40);

        countText.transform.DOComplete();
        countText.transform.DOPunchScale(Vector3.one * 0.5f, 0.15f);
    }
}
