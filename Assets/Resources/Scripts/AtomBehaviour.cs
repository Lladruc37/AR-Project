using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> atomDuplicates;
    public short atomNum = 1;
    public float radius = 0.03f;
    public bool addAtom = false;
    public bool removeAtom = false;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (addAtom || removeAtom)
        {
            for(int i = 0; i < atomDuplicates.Count; i++)
            {
                Destroy(atomDuplicates[i].gameObject);
            }
            atomDuplicates.Clear();

            if (addAtom)
            {
                atomNum++;
                addAtom = false;
            }
            else if (removeAtom)
            {
                atomNum--;
                removeAtom = false;
            }

            Debug.Log("Atom Number: " + atomNum);

            /* Distance around the circle */
            for (int i = 0; i < (atomNum - 1); i++)
            {
                /* Distance around the circle */
                var radians = 2 * Mathf.PI / atomNum * i;

                /* Get the vector direction */
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);

                var spawnDir = new Vector3(horizontal, 0, vertical);

                /* Get the spawn position */
                var spawnPos = transform.position + spawnDir * radius; // Radius is just the distance away from the point

                /* Now spawn */
                atomDuplicates.Add(Instantiate(gameObject, spawnPos, Quaternion.identity));
            }

            Debug.Log("Atom Number Dublicates: " + atomDuplicates.Count);
        }
    }
}
