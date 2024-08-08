using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WFA
{
    public class mapGenerator : MonoBehaviour
    {
        public float spacing = 21f;

        public GameObject prefabSpawn;
        
        [HideInInspector]
        public int size_x = 7;
        
        [HideInInspector]
        public int size_z = 7;

        private List<GameObject> prefabList = new List<GameObject>();

        [HideInInspector]
        public Slider Slider;

        [HideInInspector]
        public Transform parent;

        public TextMeshProUGUI prefabCountText; // Add this line to declare a Text component

        private bool useColor = true;

        public void Start()
        {
            sliderEvent();
        }

        public void sliderEvent()
        {
            size_x = (int)Slider.value;
            size_z = (int)Slider.value;

            generate();
            UpdatePrefabCountText(); // Add this line to update the text when slider changes
        }

        public void generate()
        {
            foreach (var item in prefabList)
            {
                DestroyImmediate(item);
            }

            prefabList.Clear();

            Vector3 pos = Vector3.zero;
            Quaternion rotation = Quaternion.Euler(0, -180, 0); 


            for (int x = 0; x < size_x; x++)
            {
                pos.x = x * spacing;

                for (int z = 0; z < size_z; z++)
                {
                    pos.z = z * spacing;

                    GameObject go = Instantiate(prefabSpawn);
                    go.transform.position = pos;
                    go.transform.rotation = rotation;
                    go.transform.parent = parent;

                    prefabList.Add(go);
                }
            }

            if (useColor)
            {
                for (int i = 0; i < prefabList.Count; i++)
                {
                    Color Color1 = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

                    List<Renderer> renderers = prefabList[i].GetComponentsInChildren<Renderer>().ToList();
                    renderers[0].material.color = Color1;
                    renderers[1].material.color = Color1;
                }
            }
        }

        private void UpdatePrefabCountText()
        {
            prefabCountText.text = $"Prefabs: {prefabList.Count * 12000}";
        }
    }
}
