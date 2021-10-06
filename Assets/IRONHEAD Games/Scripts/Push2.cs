using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Push2 : MonoBehaviour
{
    public float MinLocalY = 0.25f;
    public float MaxLocalY = 0.55f;

    public bool isBeingTouched = false;
    public bool isClicked = false;
    public int P1 = 0;
    public Material greenMat;


    public float smooth = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        // Start with button up top / popped up
        transform.localPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);

    }



    private void Update()
    {
        Vector3 buttonDownPosition = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        Vector3 buttonUpPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
        if (!isClicked)
        {
            if (!isBeingTouched && (transform.localPosition.y > MaxLocalY || transform.localPosition.y < MaxLocalY))
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, buttonUpPosition, Time.deltaTime * smooth);
            }

            if (transform.localPosition.y < MinLocalY)
            {
                isClicked = true;
                transform.localPosition = buttonDownPosition;
                OnButtonDown();
            }
        }

    }


    void OnButtonDown()
    {
        GetComponent<MeshRenderer>().material = greenMat;
        GetComponent<Collider>().isTrigger = true;
        if (P1 == 1)
        {
            cargarescena1();
        }
        if (P1 == 2)
        {
            cargarescena2();
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (isClicked)
        {

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag != "BackButton")
        {
            isBeingTouched = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.tag != "BackButton")
        {
            isBeingTouched = false;

        }
    }

    void cargarescena1()
    {
        SceneManager.LoadScene(1);
    }

    void cargarescena2()
    {
        SceneManager.LoadScene(2);
    }

}
