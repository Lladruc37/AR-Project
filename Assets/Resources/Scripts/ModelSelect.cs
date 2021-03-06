using UnityEngine;
using Vuforia;
using System.Collections;
public class ModelSelect : MonoBehaviour
{
    private Transform pickedObject = null;
    private bool mAddModel = false;
    private bool mRemoveModel = false;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Plane targetPlane = new Plane(transform.up, transform.position);

        if (pickedObject != null)
        {
            if (mAddModel)
            {
                pickedObject.GetComponent<AtomBehaviour>().addAtom = true;
                mAddModel = false;
            }

            if (mRemoveModel)
            {
                pickedObject.GetComponent<AtomBehaviour>().removeAtom = true;
                mRemoveModel = false;
            }
        }


        foreach (Touch touch in Input.touches)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            float dist = 0.0f;
            targetPlane.Raycast(ray, out dist);
            Vector3 planePoint = ray.GetPoint(dist);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touched!");
                //Struct used to get info back from a raycast
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    pickedObject = hit.transform;
                }
            }
        }

        //Mouse Clicking Support
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist = 0.0f;
            targetPlane.Raycast(ray, out dist);
            Vector3 planePoint = ray.GetPoint(dist);

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, 1000))
            {
                pickedObject = hit.transform;
            }
        }
    }
    void OnGUI()
    {
        if (pickedObject != null)
        {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 24;

            GUI.TextArea(new Rect(50, 110, 200, 40), "Selected: ", myButtonStyle); GUI.TextArea(new Rect(250, 110, 200, 40), pickedObject.gameObject.name, myButtonStyle);
            GUI.TextArea(new Rect(50, 150, 250, 40), "Number of atoms: ", myButtonStyle); GUI.TextArea(new Rect(300, 150, 200, 40), pickedObject.gameObject.GetComponent<AtomBehaviour>().atomNum.ToString(), myButtonStyle);
            int atomTotalCharge = pickedObject.gameObject.GetComponent<AtomBehaviour>().atomCharge * pickedObject.gameObject.GetComponent<AtomBehaviour>().atomNum;
            GUI.TextArea(new Rect(50, 190, 250, 40), "Current Charge: ", myButtonStyle); GUI.TextArea(new Rect(300, 190, 200, 40), atomTotalCharge.ToString(), myButtonStyle);
            if (GUI.Button(new Rect(50, 230, 240, 40), "Add Atom", myButtonStyle))
            {
                mAddModel = true;
            }
            if (pickedObject.gameObject.GetComponent<AtomBehaviour>().atomNum > 1)
            {
                if (GUI.Button(new Rect(50, 270, 240, 40), "Remove Atom", myButtonStyle))
                {
                    mRemoveModel = true;
                }
            }
        }
    }
}