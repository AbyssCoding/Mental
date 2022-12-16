using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineAll : MonoBehaviour
{
    [SerializeField] private Color outlineColor = Color.black;
    [SerializeField, Range(0f, 10f)]
    private float outlineWidth = 2f;
    // Start is called before the first frame update
    void Start()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = outlineWidth;
        List<Transform> children = new();

        foreach (Transform child in transform)
        {
            
            children.Add(child);
        }

        Debug.Log("Count: " + children.Count);

        foreach(Transform child in children)
        {
            outline = child.gameObject.AddComponent<Outline>();

            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
