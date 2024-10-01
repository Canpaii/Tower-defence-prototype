using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyReference : MonoBehaviour
{
    public static CurrencyReference instance;
    public Currency currency;

    void Awake()
    {
        instance = this;
    }
}
