using UnityEngine;
using Vuforia;
using System.Collections;
public class ModelSelect : MonoBehaviour
{
    private Transform pickedObject = null;
    private bool mAddModel = false;
    private bool mRemoveModel = false;
    private bool selectedObject = false;

    private Vector3 lastPlanePoint;
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
            selectedObject = true;
        }

        if (mAddModel)
        {
            AddModel();
            mAddModel = false;
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
                    lastPlanePoint = planePoint;
                }
                else
                {
                    pickedObject = null;
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
                lastPlanePoint = planePoint;
            }
            else
            {
                pickedObject = null;
            }
        }
    }
    void OnGUI()
    {
        if (selectedObject)
        {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 24;

            GUI.TextArea(new Rect(50,110,200,40), "Selected: ", myButtonStyle); GUI.TextArea(new Rect(250, 110, 200, 40), pickedObject.gameObject.name, myButtonStyle);
            if (GUI.Button(new Rect(50, 150, 240, 40), "Add Model", myButtonStyle))
            {
                mAddModel = true;
            }
            if (GUI.Button(new Rect(50, 190, 240, 40), "Remove Model", myButtonStyle))
            {
                mRemoveModel = true;
            }
        }
    }
    private void AddModel()
    {
        
    }
}