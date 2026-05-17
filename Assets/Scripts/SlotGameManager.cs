using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class SlotGameManager : MonoBehaviour
{
    [Header("Reels")]
    public Reel[] reels;

    [Header("UI")]
    public Button spinButton;
    public GameObject winPopup;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI balanceText;

    [Header("Lever")]
    public Image neutralLeverImage;
    public Image pulledLeverImage;
    public float leverReturnDelay = 1f;

    [Header("Game")]
    public int startingBalance = 100;
    public int spinCost = 10;

    [Header("Audio")]
    public AudioClip spinClip;
    public AudioClip jackpotClip;
    public AudioClip matchClip;

    private int balance;
    private bool isSpinning;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        balance = startingBalance;
        UpdateBalanceUI();

        if (winPopup != null)
            winPopup.SetActive(false);

        if (neutralLeverImage != null) neutralLeverImage.enabled = true;
        if (pulledLeverImage != null) pulledLeverImage.enabled = false;

        if (spinButton != null)
            spinButton.onClick.AddListener(TriggerSpin);
    }

    public void TriggerSpin()
    {
        if (isSpinning || balance < spinCost)
            return;

        StartCoroutine(SpinSequence());
    }

    private IEnumerator SpinSequence()
    {
        isSpinning = true;
        balance -= spinCost;
        UpdateBalanceUI();

        if (winPopup != null)
            winPopup.SetActive(false);

        if (audioSource != null && spinClip != null)
            audioSource.PlayOneShot(spinClip);

        if (neutralLeverImage != null) neutralLeverImage.enabled = false;
        if (pulledLeverImage != null) pulledLeverImage.enabled = true;

        for (int i = 0; i < reels.Length; i++)
            reels[i].StartSpin(i * 0.2f);

        yield return new WaitForSeconds(2.6f);

        EvaluateResult();

        yield return new WaitForSeconds(leverReturnDelay);

        if (pulledLeverImage != null) pulledLeverImage.enabled = false;
        if (neutralLeverImage != null) neutralLeverImage.enabled = true;

        isSpinning = false;
    }

    private void EvaluateResult()
    {
        int r0 = reels[0].GetResult();
        int r1 = reels[1].GetResult();
        int r2 = reels[2].GetResult();

        if (r0 == r1 && r1 == r2)
        {
            int payout = GetPayoutMultiplier(r0) * spinCost;
            balance += payout;
            UpdateBalanceUI();
            ShowWin("JACKPOT!\n+" + payout + " coins!");

            if (audioSource != null && jackpotClip != null)
                audioSource.PlayOneShot(jackpotClip);
        }
        else if (r0 == r1 || r1 == r2 || r0 == r2)
        {
            int payout = spinCost;
            balance += payout;
            UpdateBalanceUI();
            ShowWin("2 MATCHES!\n+" + payout + " coins!");

            if (audioSource != null && matchClip != null)
                audioSource.PlayOneShot(matchClip);
        }
    }

    private int GetPayoutMultiplier(int symbolIndex)
    {
        switch (symbolIndex)
        {
            case 0: return 10;
            case 1: return 2;
            case 2: return 3;
            case 3: return 5;
            default: return 1;
        }
    }

    private void ShowWin(string message)
    {
        if (winPopup != null)
            winPopup.SetActive(true);

        if (winText != null)
            winText.text = message;
    }

    private void UpdateBalanceUI()
    {
        if (balanceText != null)
            balanceText.text = "Balance: " + balance;
    }

    public void CloseWinPopup()
    {
        if (winPopup != null)
            winPopup.SetActive(false);
    }
}