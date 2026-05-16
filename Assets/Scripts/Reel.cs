using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Header("Reel Config")]
    public SlotSymbol[] symbolPool;    // Assign all 4 symbols in Inspector
    public float symbolHeight = 150f;
    public float spinSpeed = 1800f;    // pixels per second
    public float spinDuration = 2f;

    [Header("References")]
    public Transform symbolContainer;  // Parent that scrolls (Reel_X)
    public Image[] visibleSymbolImages; // The 3 visible image slots

    private int _landingIndex;         // RNG-decided result
    private bool _isSpinning;

    // Called externally to start spin
    public void StartSpin(float delay, int forcedResult = -1)
    {
        StartCoroutine(SpinRoutine(delay, forcedResult));
    }

    public int GetResult() => _landingIndex;

    private IEnumerator SpinRoutine(float delay, int forcedResult)
    {
        yield return new WaitForSeconds(delay);

        _isSpinning = true;
        _landingIndex = forcedResult >= 0 ? forcedResult
                        : Random.Range(0, symbolPool.Length);

        float elapsed = 0f;
        float blurAlpha = 1f;

        // Scroll symbols downward for spinDuration
        while (elapsed < spinDuration)
        {
            float move = spinSpeed * Time.deltaTime;
            symbolContainer.localPosition -= new Vector3(0, move, 0);

            // Wrap symbol container position to fake infinite loop
            if (Mathf.Abs(symbolContainer.localPosition.y) > symbolHeight * symbolPool.Length)
                symbolContainer.localPosition = Vector3.zero;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to final result
        symbolContainer.localPosition = Vector3.zero;
        SetDisplayedSymbol(_landingIndex);
        _isSpinning = false;
    }

    private void SetDisplayedSymbol(int index)
    {
        // Set the center visible image to the result
        if (visibleSymbolImages.Length > 0)
            visibleSymbolImages[0].sprite = symbolPool[index].sprite;
    }
}