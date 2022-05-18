using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinatorBehaviour : MonoBehaviour
{
    public GameObject atomOne;
    public GameObject atomTwo;
    public float dist;
    public List<GameObject> resultingMolecules;
    int numResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Reset Each Frame
        for(int i = 0; i < resultingMolecules.Count; i++)
        {
            Destroy(resultingMolecules[i].gameObject);
        }
        resultingMolecules.Clear();

        foreach (GameObject a in atomOne.GetComponent<AtomBehaviour>().atomDuplicates)
        {
            foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = true;
        }
        foreach (GameObject a in atomTwo.GetComponent<AtomBehaviour>().atomDuplicates)
        {
            foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = true;
        } 
        foreach (Renderer r in atomOne.GetComponentsInChildren<Renderer>()) r.enabled = true;
        foreach (Renderer r in atomTwo.GetComponentsInChildren<Renderer>()) r.enabled = true;


        //Calculate distance
        float result = Vector3.Distance(atomOne.transform.position,atomTwo.transform.position);
        if(result <= dist) //Add conditions for combinations
        {
            Debug.Log(result);
            Vector3 resultLocation = atomOne.transform.position + ((atomOne.transform.position + atomTwo.transform.position)/2);
            Debug.Log(atomOne.transform.position);
            Debug.Log(atomTwo.transform.position);
            Debug.Log(resultLocation);
            //Hardcoded Water Reaction
            if (atomOne.GetComponent<AtomBehaviour>().atomNum == 4 && atomTwo.GetComponent<AtomBehaviour>().atomNum == 2)
            {
                foreach (GameObject a in atomOne.GetComponent<AtomBehaviour>().atomDuplicates)
                {
                    foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = false;
                }
                foreach (GameObject a in atomTwo.GetComponent<AtomBehaviour>().atomDuplicates)
                {
                    foreach (Renderer r in a.GetComponentsInChildren<Renderer>()) r.enabled = false;
                } 
                foreach (Renderer r in atomOne.GetComponentsInChildren<Renderer>()) r.enabled = false;
                foreach (Renderer r in atomTwo.GetComponentsInChildren<Renderer>()) r.enabled = false;

                numResult = 2;
                 /* Distance around the circle */
                for (int i = 0; i < numResult; i++)
                {
                    /* Distance around the circle */
                    var radians = 2 * Mathf.PI / numResult * i;

                    /* Get the vector direction */
                    var vertical = Mathf.Sin(radians);
                    var horizontal = Mathf.Cos(radians);

                    var spawnDir = new Vector3(horizontal, 0, vertical);

                    /* Get the spawn position */
                    var spawnPos = resultLocation + spawnDir * 0.01f; // Radius is just the distance away from the point

                    /* Now spawn */
                    
                    GameObject go = Instantiate(GameObject.FindGameObjectWithTag("H20"), spawnPos, Quaternion.identity);
                    resultingMolecules.Add(go);
                }
            }
        }
        
    }
}
