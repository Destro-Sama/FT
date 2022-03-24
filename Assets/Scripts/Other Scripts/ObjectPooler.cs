using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //The System.Serializable header makes this entire class viewable in the unity editor
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    //This creates a class reference as an object inside its own class
    public static ObjectPooler Instance;

    //creates a list of type pool
    public List<Pool> pools;
    //creates a dictionary with key type string, and item type of a queue of GameObjects
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    //awake is a function called by unity when the object is first initialised
    private void Awake()
    {
        Instance = this;
    }

    //start is a function called by unity directly after awake
    private void Start()
    {
        //setting the dictioanry to an empty dictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            //creating a new empty queue of GameObjects
            Queue<GameObject> objectPool = new Queue<GameObject>();

            //for i, starting at 0, increasing by 1
            for (int i = 0; i < pool.size; i++)
            {
                //Instantiate creates a copy of the object in the real space, on the screen
                GameObject obj = Instantiate(pool.prefab);
                //SetActive(false) makes the object non interactable, and invinisble. But still there
                obj.SetActive(false);
                //Adding the object to the queue
                objectPool.Enqueue(obj);
                //SetParent sets the parent of the object, which allows us to make local references
                obj.gameObject.transform.SetParent(this.gameObject.transform);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    //GameObject is the return type of the function
    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        //checking if the item is in the dictioanary
        if (!poolDictionary.ContainsKey(tag))
            return null;

        //removing the object from the queue, and taking its reference
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        //SetActive(true) makes the object interactable and visible.
        objectToSpawn.SetActive(true);

        //Adding the object back to the bottom of the queue
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
