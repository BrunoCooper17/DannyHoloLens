using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInformation : MonoBehaviour {

    public enum ID_CARD
    {
        MEXICO,
        DUBLIN,
        SINGAPORE,
        NEW_YORK,
        KENTUKY,
        SAN_FRANCISCO,
        TVL
    };

    public ID_CARD Id_Card;

    public void PlayShow()
    {
        GetComponent<Animator>().Play("Card_Show", 0, 0.0f);
    }

    public void PlayHide()
    {
        GetComponent<Animator>().Play("Card_Hide", 0, 0.0f);
    }
}
