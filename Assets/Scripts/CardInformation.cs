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
    public float alpha = 0.95f;
    public GameObject cursorLocation;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cursorLocation = GameObject.Find("GazeCursor");
    }

    void Update()
    {
        tmpPositionCursor = (alpha * (cursorLocation.transform.position.normalized)) + (1.0f - alpha) * tmpPositionCursor;

        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Card_Show" && (animator.GetCurrentAnimatorStateInfo(0).length*2 <
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime))
        {
            animator.Stop();
            transform.position = tmpPositionCursor;
        }
    }

    public void PlayShow()
    {
        GetComponent<Animator>().Play("Card_Show", 0, 0.0f);
    }

    public void PlayHide()
    {
        GetComponent<Animator>().Play("Card_Hide", 0, 0.0f);
    }
}
