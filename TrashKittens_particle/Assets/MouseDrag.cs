using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseDrag : MonoBehaviour
{

    float distance = 10;

    void OnMouseDrag()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        transform.position = objPosition;
    }

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    //       OnMouseDrag();
    //}
}

