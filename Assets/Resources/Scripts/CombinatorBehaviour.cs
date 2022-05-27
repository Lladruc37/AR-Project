using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinatorBehaviour : MonoBehaviour
{
    public GameObject atomOne;
    public GameObject atomTwo;
    public float dist;
    private GameObject resultingMolecule;
    Table library;
    // Start is called before the first frame update
    void Start()
    {
        library = GetComponent<Table>();
    }

    // Update is called once per frame
    void Update()
    {
        //Reset Each Frame
        Destroy(resultingMolecule);

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
            Debug.Log(result);
            Vector3 distance = atomTwo.transform.position - atomOne.transform.position;
            Vector3 resultLocation = atomOne.transform.position + distance;
            Debug.Log(atomOne.transform.position);
            Debug.Log(atomTwo.transform.position);
            Debug.Log(resultLocation);
			//Hardcoded Water Reaction

            //EXPLODES HERE
			//Molecule m = library.CheckCombinations(new Combination(Element(ElementType.Hydrogen,2),Element(ElementType.Oxygen,1));
            //if(m != null)
            //{
            //
            //}

            if (atomOne.GetComponentInChildren<AtomBehaviour>().atomNum == 2 && atomTwo.GetComponentInChildren<AtomBehaviour>().atomNum == 1)
            {
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
        }
        
    }
}
