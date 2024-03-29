using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.ThreeDGraphics
{
    public class ChangeMaterialColor : MonoBehaviour
    {
        [SerializeField] Color fromColor = Color.white;
        [SerializeField] Color toColor = Color.green;

        [SerializeField] float speed = 1f;

        new Renderer renderer;
        // Start is called before the first frame update
        void Start()
        {
            renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            float t = Mathf.Sin(Time.time * speed);

            t += 1;
            t /= 2;
            var newColor = Color.Lerp(fromColor, toColor, t);

            renderer.material.color = newColor;
        }
    }

}
