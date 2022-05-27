using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Oxygen,
    Hydrogen,
    Iron,
    Carbon
}
public class Element
{
    public int quantity;
    public ElementType type;

    public Element(ElementType t, int num)
    {
        this.type = t;
        this.quantity = num;
    }
}

public class Combination
{
    public Element elementOne;
    public Element elementTwo;
    public Element elementThree;

    public Combination(Element one, Element two, Element three = null)
    {
        this.elementOne = one;
        this.elementTwo = two;
        this.elementThree = three;
    }
}
public class Molecule
{
    public string name;
    public int quantity;

    public Molecule(string n, int num)
    {
        this.name = n;
        this.quantity = num;
    }
    public Molecule(Molecule m)
    {
        this.name = m.name;
        this.quantity = m.quantity;
    }
}

public class Table : MonoBehaviour
{
    Dictionary<Combination, Molecule> library;
    // Start is called before the first frame update
    void Start()
    {
        library.Add(new Combination(new Element(ElementType.Oxygen, 1), new Element(ElementType.Hydrogen, 2)), new Molecule("H2O", 1));
        library.Add(new Combination(new Element(ElementType.Oxygen, 3), new Element(ElementType.Iron, 2)), new Molecule("Fe2O3", 1));
        library.Add(new Combination(new Element(ElementType.Oxygen, 1), new Element(ElementType.Carbon, 2)), new Molecule("C2O", 1));
        library.Add(new Combination(new Element(ElementType.Hydrogen, 3), new Element(ElementType.Iron, 1)), new Molecule("FeH3", 1));
        library.Add(new Combination(new Element(ElementType.Hydrogen, 4), new Element(ElementType.Carbon, 1)), new Molecule("CH4", 1));
        library.Add(new Combination(new Element(ElementType.Hydrogen, 2), new Element(ElementType.Oxygen, 2), new Element(ElementType.Iron, 1)), new Molecule("Fe(OH)2", 1));
        library.Add(new Combination(new Element(ElementType.Hydrogen, 1), new Element(ElementType.Oxygen, 1)), new Molecule("OH", 1));
        library.Add(new Combination(new Element(ElementType.Iron, 1), new Element(ElementType.Oxygen, 1)), new Molecule("FeO", 1));
    }
    Molecule CheckCombination(Combination c)
	{
        if (library.ContainsKey(c))
        {
            return new Molecule(library[c]);
        }
        return null;
    }
    public Molecule CheckCombinations(Combination c)
	{
        Molecule m = null;

        m = CheckCombination(c);
        if(m == null)
		{
            if (c.elementThree != null)
			{
                Combination newC = new Combination(c.elementOne, c.elementThree, c.elementTwo);
                m = CheckCombination(newC);
                if (m != null)
                {
                    return m;
                }
                newC = new Combination(c.elementTwo, c.elementOne, c.elementThree);
                m = CheckCombination(newC);
                if (m != null)
                {
                    return m;
                }
                newC = new Combination(c.elementTwo, c.elementThree, c.elementOne);
                m = CheckCombination(newC);
                if (m != null)
                {
                    return m;
                }
                newC = new Combination(c.elementThree, c.elementOne, c.elementTwo);
                m = CheckCombination(newC);
                if (m != null)
                {
                    return m;
                }
                newC = new Combination(c.elementThree, c.elementTwo, c.elementOne);
                m = CheckCombination(newC);
            }
            else
			{
                Combination newC = new Combination(c.elementTwo, c.elementOne);
                m = CheckCombination(newC);
            }
        }
        return m;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
