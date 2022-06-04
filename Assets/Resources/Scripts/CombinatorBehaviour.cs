using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinatorBehaviour : MonoBehaviour
{
    public GameObject atomOne;
    public GameObject atomTwo;
    public float dist;
    private GameObject resultingMolecule;
    bool areTogether = false;
    bool combinationWorks = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Reset Each Frame
        Destroy(resultingMolecule);

        atomOne.GetComponentInChildren<AtomBehaviour>().combined = false;
        atomTwo.GetComponentInChildren<AtomBehaviour>().combined = false;
        foreach (GameObject a in atomOne.GetComponentInChildren<AtomBehaviour>().atomDuplicates)
        {
            foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = true;
        }
        foreach (GameObject a in atomTwo.GetComponentInChildren<AtomBehaviour>().atomDuplicates)
        {
            foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = true;
        } 
        foreach (Renderer r in atomOne.GetComponentsInChildren<Renderer>()) r.enabled = true;
        foreach (Renderer r in atomTwo.GetComponentsInChildren<Renderer>()) r.enabled = true;


        //Calculate distance
        float result = Vector3.Distance(atomOne.transform.position,atomTwo.transform.position);
        if(result <= dist) //Add conditions for combinations
        {
            areTogether = true;
            Debug.Log(result);
            Vector3 distance = atomTwo.transform.position - atomOne.transform.position;
            Vector3 resultLocation = atomOne.transform.position + distance;
            Debug.Log(atomOne.transform.position);
            Debug.Log(atomTwo.transform.position);
            Debug.Log(resultLocation);

			//Hardcoded Water Reaction

            if (atomOne.GetComponentInChildren<AtomBehaviour>().atomNum == 2 && atomTwo.GetComponentInChildren<AtomBehaviour>().atomNum == 1)
            {
                combinationWorks = true;
                atomOne.GetComponentInChildren<AtomBehaviour>().combined = true;
                atomTwo.GetComponentInChildren<AtomBehaviour>().combined = true;
                foreach (GameObject a in atomOne.GetComponentInChildren<AtomBehaviour>().atomDuplicates)
                {
                    foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = false;
                }
                foreach (GameObject a in atomTwo.GetComponentInChildren<AtomBehaviour>().atomDuplicates)
                {
                    foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = false;
                } 
                foreach (Renderer r in atomOne.GetComponentsInChildren<Renderer>()) r.enabled = false;
                foreach (Renderer r in atomTwo.GetComponentsInChildren<Renderer>()) r.enabled = false;

                resultingMolecule = Instantiate(GameObject.FindGameObjectWithTag("H20"), resultLocation, Quaternion.Euler(new Vector3(-90, 0, -60)));
            }
            else
            {
                combinationWorks = false;
            }
        }
        else
        {
            areTogether = false;
        }
        
    }

    void OnGUI()
    {
        if (areTogether)
        {
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
            myButtonStyle.fontSize = 24;

            if (combinationWorks)
            {
                GUI.TextArea(new Rect(300, 850, 900, 40), "Congratulations! You created H20.", myButtonStyle);
            }
            else
            {
                GUI.TextArea(new Rect(300, 850, 900, 40), "Unstable combination. The atoms should have a neutral electric charge", myButtonStyle);
            }
        }
    }
}
