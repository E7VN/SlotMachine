using UnityEngine;
using UnityEngine.UI;

public class LeverControl : MonoBehaviour
{
    public SlotGameManager gameManager;

    public void OnLeverClicked()
    {
        if (gameManager != null)
            gameManager.TriggerSpin();
    }
}