using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public float duration = 1.0f;
    public float speed = 1.0f;
    public Color color = Color.white;

    private TextMeshPro textMesh;
    private float timer = 0f;

    private Transform lookTarget;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        lookTarget = Camera.main.transform;
    }

    public void Set(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        textMesh.alpha = 1f - (timer / duration);
        transform.LookAt(lookTarget);

        transform.position += Vector3.up * speed * Time.deltaTime;

        if(timer > duration)
        {
            Destroy(gameObject);
        }
    }
}
