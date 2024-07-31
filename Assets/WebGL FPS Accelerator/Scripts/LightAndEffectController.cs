using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    public GameObject prefab; // Привяжите ваш префаб сюда
    public Button destroyButton; // Привяжите вашу кнопку для удаления сюда
    public Toggle lightToggle; // Привяжите ваш toggle сюда
    public Light directionalLight; // Привяжите ваш directional light сюда
    public Slider sizeSlider; // Привяжите ваш слайдер сюда
    public Transform parent; // Привяжите ваш объект родителя сюда

    private float spacing = 21f;
    private int size_x = 7;
    private int size_z = 7;
    private List<GameObject> prefabList = new List<GameObject>();

    void Start()
    {
        destroyButton.onClick.AddListener(DestroyAllEffects);
        lightToggle.onValueChanged.AddListener(delegate { ToggleLight(lightToggle); });
        sizeSlider.onValueChanged.AddListener(delegate { SliderEvent(); });
        SliderEvent(); // Генерация объектов при запуске
    }

    void ToggleLight(Toggle toggle)
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = toggle.isOn;
        }
    }

    void SliderEvent()
    {
        size_x = (int)sizeSlider.value;
        size_z = (int)sizeSlider.value;
        Generate();
    }

    void Generate()
    {
        DestroyAllEffects(); // Удаляем предыдущие объекты перед генерацией новых
        Vector3 pos = Vector3.zero;

        for (int x = 0; x < size_x; x++)
        {
            pos.x = x * spacing;

            for (int z = 0; z < size_z; z++)
            {
                pos.z = z * spacing;

                GameObject go = Instantiate(prefab);
                go.transform.position = pos;
                go.transform.parent = parent;

                prefabList.Add(go);
            }
        }
    }

    void DestroyAllEffects()
    {
        foreach (var item in prefabList)
        {
            DestroyImmediate(item);
        }
        prefabList.Clear();
    }
}