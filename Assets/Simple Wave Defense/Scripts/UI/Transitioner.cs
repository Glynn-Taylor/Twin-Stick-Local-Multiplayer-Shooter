using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Transitioner: MonoBehaviour
{
    [Header("Animation")]
    [SerializeField]
    public LoadingAnimation[] StartAnimations;
    [SerializeField]
    public LoadingAnimation[] EndAnimations;


    [Serializable]
    public class LoadingAnimation
    {
        public MaskableGraphic Graphic;
        public LoadingAnimationType Type;
        public float StartTime;
        public float Duration;
        public float TargetValue;
        public Color TargetColor;

        public enum LoadingAnimationType
        {
             Color, Fill
        }
    }

    public void TransitionIn()
    {
        for (int i = 0; i < StartAnimations.Length; i++)
        {
            Animate(StartAnimations[i]);
        }
    }
    public void TransitionOut()
    {
        for (int i = 0; i < EndAnimations.Length; i++)
        {
            Animate(EndAnimations[i]);
        }
    }
	
    public void Animate(LoadingAnimation lAnimation)
    {
        StartCoroutine("PerformLoadingAnimation", lAnimation);
    }
    IEnumerator PerformLoadingAnimation(LoadingAnimation lAnimation)
    {
        yield return new WaitForSeconds(lAnimation.StartTime);
	    switch (lAnimation.Type)
        {
            case LoadingAnimation.LoadingAnimationType.Color:
                StartCoroutine("PerformColorLerp", lAnimation);
                break;
            case LoadingAnimation.LoadingAnimationType.Fill:
                StartCoroutine("PerformFill",lAnimation);
                break;
        }
    }

    IEnumerator PerformFill(LoadingAnimation lAnimation)
    {
        float elapsedTime = 0;
        Image img = (Image)lAnimation.Graphic;

        float startingFill = img.fillAmount;

        while (elapsedTime < lAnimation.Duration)
        {
            img.fillAmount = Mathf.Lerp(startingFill, lAnimation.TargetValue, (elapsedTime / lAnimation.Duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator PerformColorLerp(LoadingAnimation lAnimation)
    {
        float elapsedTime = 0;

        Color startingColor = new Color(lAnimation.Graphic.color.r,lAnimation.Graphic.color.g,lAnimation.Graphic.color.b,lAnimation.Graphic.color.a);


        while (elapsedTime < lAnimation.Duration)
        {
            lAnimation.Graphic.color = Color.Lerp(startingColor, lAnimation.TargetColor, (elapsedTime / lAnimation.Duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
