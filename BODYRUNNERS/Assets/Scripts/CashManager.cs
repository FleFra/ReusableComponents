using System;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static Action<int> addCash;
    int currentCash;

    [SerializeField] TMP_Text cashTxt;

    private void Start()
    {
        currentCash = 0; 
        addCash += AddCash;   
    }

    void AddCash(int amount)
    {
        currentCash += amount;
        cashTxt.text = $"Cash: {currentCash} \r\n";
    }
}
