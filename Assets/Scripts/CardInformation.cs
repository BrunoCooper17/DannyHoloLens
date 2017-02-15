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

    public Animator animator;

    public Vector3 tmpPositionCursor;
    public float alpha = 0.05f;
    public GameObject cursorLocation;

    public bool bcheck = false;

    private float acum=0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cursorLocation = GameObject.Find("GazeCursor");
    }

    void Update()
    {
        tmpPositionCursor = (alpha * (cursorLocation.transform.position.normalized)) + (1.0f - alpha) * tmpPositionCursor;

        acum -= Time.deltaTime;

        if (bcheck && acum < 0.0f)
        {
            animator.enabled = false;
            transform.position = tmpPositionCursor;
        }
    }

    public void PlayShow()
    {
        bcheck = true;
        animator.Play("Card_Show", 0, 0.0f);
        acum = 0.5f;
    }

    public void PlayHide()
    {
        bcheck = false;

        animator.enabled = true;
        animator.Play("Card_Hide", 0, 0.0f);
    }

    void hideCard()
    {
        animator.Play("Card_Hide", 0, 0.0f);
    }
}
