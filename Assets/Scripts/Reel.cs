using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("Symbols")]
    public Sprite[] symbolSprites;

    [Header("Spin")]
    public float spinDuration = 2f;

    [Header("References")]
    public Image visibleSymbolImage;

    private int _landingIndex;

    public void StartSpin(float delay)
    {
        StartCoroutine(SpinRoutine(delay));
    }

    public int GetResult() => _landingIndex;

    private IEnumerator SpinRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        _landingIndex = Random.Range(0, symbolSprites.Length);

        float elapsed = 0f;
        int fakeFrame = 0;
        float frameTimer = 0f;
        float frameInterval = 0.08f;

        while (elapsed < spinDuration)
        {
            frameTimer += Time.deltaTime;
            elapsed += Time.deltaTime;

            if (frameTimer >= frameInterval)
            {
                fakeFrame = (fakeFrame + 1) % symbolSprites.Length;
                visibleSymbolImage.sprite = symbolSprites[fakeFrame];
                frameTimer = 0f;

                if (elapsed > spinDuration * 0.7f)
                    frameInterval = Mathf.Lerp(0.08f, 0.25f, (elapsed - spinDuration * 0.7f) / (spinDuration * 0.3f));
            }

            yield return null;
        }

        visibleSymbolImage.sprite = symbolSprites[_landingIndex];
    }
}