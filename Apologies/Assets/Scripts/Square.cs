using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int squareID;
    public char safeZone = ' ';
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnMouseDown()
    {
        Debug.Log("Yes");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log(hit.collider.gameObject.GetComponent<Square>().squareID);
        }
    }
}
