using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public List<GameObject> FruitList { get { return fruitList; } set { fruitList = value; } }

    [Tooltip("All fruit prefabs")]
    [SerializeField] List<GameObject> fruitPrefabs;
    [Tooltip("Fruit list for object pooler")]
    [SerializeField] List<GameObject> fruitList;
    [Tooltip("Amount of fruits for each prefab at start")]
    [SerializeField] int fruitSize;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        AddFruitToPool(fruitSize);
    }

    // Method in charge of generating the fruits and adding it to the object pooler
    void AddFruitToPool(int amount, bool random = false)
    {
        if (!random)
        {
            for (int i = 0; i < fruitPrefabs.Count; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    GameObject fruit = Instantiate(fruitPrefabs[i], gameObject.transform);

                    fruit.SetActive(false);
                    fruit.GetComponent<Fruit>().Id = i;
                    fruitList.Add(fruit);
                }
            }
        }
        else
        {
            int rn = Random.Range(0, fruitPrefabs.Count);

            GameObject fruit = Instantiate(fruitPrefabs[rn], gameObject.transform);
            fruit.SetActive(false);
            fruit.GetComponent<Fruit>().Id = rn;
            fruitList.Add(fruit);
        }
    }

    // Method in charge of delivering or returning a fruit from the grouper of objects.
    public GameObject GetFruitToPool(Sprite fruit)
    {
        for (int i = 0; i < fruitList.Count; i++)
        {
            if (!fruitList[i].activeSelf && fruitList[i].GetComponentInChildren<SpriteRenderer>().sprite == fruit)
            {
                return fruitList[i];
            }
        }

        AddFruitToPool(1, true);

        return fruitList[fruitList.Count - 1];
    }
}