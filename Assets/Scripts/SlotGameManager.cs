using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotGameManager : MonoBehaviour
{
    [Header("Reels")]
    public Reel[] reels;              

    [Header("UI References")]
    public Button spinButton;
    public GameObject winPopup;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI balanceText;

    [Header("Game Config")]
    public int startingBalance = 100;
    public int spinCost = 10;

    private int _balance;
    private bool _isSpinning;

    private void Start()
    {
        _balance = startingBalance;
        UpdateBalanceUI();
        winPopup.SetActive(false);
        spinButton.onClick.AddListener(OnSpinPressed);
    }

    private void OnSpinPressed()
    {
        if (_isSpinning || _balance < spinCost) return;

        _balance -= spinCost;
        UpdateBalanceUI();
        winPopup.SetActive(false);

        StartCoroutine(DoSpin());
    }

    private IEnumerator DoSpin()
    {
        _isSpinning = true;
        spinButton.interactable = false;

        for (int i = 0; i < reels.Length; i++)
            reels[i].StartSpin(i * 0.2f);

        float totalWait = 2f + (reels.Length - 1) * 0.2f + 0.1f;
        yield return new WaitForSeconds(totalWait);

        EvaluateResult();

        _isSpinning = false;
        spinButton.interactable = true;
    }

    private void EvaluateResult()
    {
        int r0 = reels[0].GetResult();
        int r1 = reels[1].GetResult();
        int r2 = reels[2].GetResult();

        if (r0 == r1 && r1 == r2)
        {
            int payout = 10 * spinCost;
            _balance += payout;
            UpdateBalanceUI();
            ShowWin($"JACKPOT! +{payout} coins!");
        }
        else if (r0 == r1 || r1 == r2 || r0 == r2)
        {
            int payout = spinCost;
            _balance += payout;
            UpdateBalanceUI();
            ShowWin($"✨ 2 Matches! +{payout} coins!");
        }
    }

    private void ShowWin(string message)
    {
        winPopup.SetActive(true);
        winText.text = message;
    }

    private void UpdateBalanceUI()
    {
        balanceText.text = $"Balance: {_balance}";
    }

    public void CloseWinPopup() => winPopup.SetActive(false);
}