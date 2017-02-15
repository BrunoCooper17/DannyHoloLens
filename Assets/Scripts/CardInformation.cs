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

    private float acum = 100000;
    private float checkAcum = 0;
    public float mag;

    public Transform ParentRef;

    private void Start()
    {
        animator = GetComponent<Animator>();
        cursorLocation = GameObject.Find("GazeCursor");
    }

    void Update()
    {
        /*
        //if (name == "Card_01 (1)")
        //    Debug.Log("aSDASDASDqweqdwa " + name + " " + (tmpPositionCursor - (Camera.main.transform.forward)).magnitude);

        mag = Mathf.Abs(Vector3.Distance(tmpPositionCursor, Camera.main.transform.forward));
        if (mag < 1.7f)
        {/**/
            tmpPositionCursor = (alpha * (cursorLocation.transform.position.normalized)) + (1.0f - alpha) * tmpPositionCursor;
            transform.position = tmpPositionCursor;
            /*checkAcum = 1.0f;
        }
        else
        {
            checkAcum -= Time.deltaTime;
            if( checkAcum < 0.0f)
            {
                //Debug.Log(name + " Checking");
                checkAcum = 1.0f;
                tmpPositionCursor = cursorLocation.transform.position.normalized;
            }
        }/**/
        

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
    }

    public void PlayHide()
    {
        bcheck = false;
        acum = 1000000;

        gameObject.transform.SetParent(ParentRef);

        animator.enabled = true;
        animator.Play("Card_Hide", 0, 0.0f);
    }

    void hideCard()
    {
        animator.Play("Card_Hide", 0, 0.0f);
    }
}
