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
    public Vector3 tmpRot;
    public float alpha = 0.15f;
    public GameObject cursorLocation;

    public bool bcheck = false;

    private float acum = 100000;
    private float checkAcum = 0;
    public float mag;

    public Vector3 rotation;

    public Transform ParentRef;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cursorLocation = GameObject.Find("GazeCursor");
    }

    void Update()
    {
        alpha = 0.15f;

        Vector3 tmpCursorPos = cursorLocation.transform.position;
        tmpCursorPos.Normalize();
        tmpPositionCursor = (alpha * (tmpCursorPos.normalized)) + (1.0f - alpha) * tmpPositionCursor;
        transform.position = tmpPositionCursor + Camera.main.transform.up * 0.05f;

        Quaternion rot = Quaternion.LookRotation(-Camera.main.transform.up, -Camera.main.transform.forward);
        tmpRot = (alpha * (rot.eulerAngles)) + (1.0f - alpha) * tmpRot;

        if (!animator.enabled)
        {
            transform.eulerAngles = tmpRot;
        }

        acum -= Time.deltaTime;

        if (bcheck && acum < 0.0f)
        {
            animator.enabled = false;
            //transform.position = tmpPositionCursor;
            bcheck = false;
            ParentRef = gameObject.transform.parent;
            gameObject.transform.SetParent(gameObject.transform.parent.parent);
        }
    }

    public void PlayShow()
    {
        acum = 0.6f;
        bcheck = true;
        animator.Play("Card_Show", 0, 0.0f);

        rotation = transform.eulerAngles;
    }

    public void PlayHide()
    {
        bcheck = false;
        acum = 1000000;

        gameObject.transform.SetParent(ParentRef);

        animator.enabled = true;
        animator.Play("Card_Hide", 0, 0.0f);

        transform.eulerAngles = rotation;
    }

    void hideCard()
    {
        animator.Play("Card_Hide", 0, 0.0f);
    }
}
