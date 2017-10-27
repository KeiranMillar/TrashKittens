using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseDrag : MonoBehaviour {
    bool currClickStatus, prevClickstatus;
    float distance = 10;
    public float mult;
    Vector2 beginTouch;
    Vector2 exitTouch;
    Vector2 forceToApply;

    Vector3 lastPos;
    Vector3 thisPos;

    Rigidbody2D rb;

    bool click;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        beginTouch = new Vector2(0.0f, 0.0f);
        exitTouch = new Vector2(0.0f, 0.0f);
        forceToApply = new Vector2(0.0f, 0.0f);
        mult = 50.0f;
    }

    void OnMouseEnter()
    {
        beginTouch.x = Input.mousePosition.x;
        beginTouch.y = Input.mousePosition.y;
    }

    void OnMouseDrag()
    {
        lastPos = thisPos;
        thisPos = transform.position;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    void OnMouseUp()
    {
        thisPos = transform.position;
        Vector3 velocity = thisPos - lastPos;
        exitTouch.x = Input.mousePosition.x;
        exitTouch.y = Input.mousePosition.y;

        Debug.Log(forceToApply);
        forceToApply = exitTouch - beginTouch;
        float xForce, yForce;
        // if (forceToApply.x >= 150.0f) forceToApply.x = 150.0f;
        // if (forceToApply.y >= 150.0f) forceToApply.y = 150.0f;
        rb.velocity = velocity*3f;
        //rb.AddForce(forceToApply * mult * Time.deltaTime, ForceMode2D.Impulse);
    }

   void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Camera.main.ViewportToWorldPoint( beginTouch), Camera.main.ViewportToWorldPoint(exitTouch));
    }
    //bool isClicked()
    //{
    //    checkIfTouched();
    //    bool clickedOrNot = false;
    //    clickedOrNot &= OnMouseDrag();
    //    return clickedOrNot;
    //}

    //bool checkIfTouched()
    //{
    //    return false;
    //}

    //void Update()
    //{
    //    prevClickstatus = currClickStatus;
    //    currClickStatus = isClicked();

    //    if (prevClickstatus && currClickStatus) calculateForce();
    //    else {
    //        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    //        rb.AddForce();
    //    }
    //    /*
    //     * ghave a variale to tell you whether the player were clicking in the last frame as well or not. 
    //     * based on position of previous click and current, make a vector, and then applyforce to your rigitdbody
    //     */


    //}

    //void calculateForce() {
    //}


}
