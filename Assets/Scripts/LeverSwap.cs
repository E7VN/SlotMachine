using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LeverSwap : MonoBehaviour
{
    public Image leverImage;
    public Sprite leverNeutral;
    public Sprite leverPulled;

    public void PullLever()
    {
        StopAllCoroutines();
        StartCoroutine(PullRoutine());
    }

    private IEnumerator PullRoutine()
    {
        leverImage.sprite = leverPulled;
        yield return new WaitForSeconds(1f);
        leverImage.sprite = leverNeutral;
    }
}