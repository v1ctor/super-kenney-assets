using UnityEngine;

public class ParallaxController : MonoBehaviour
{

    public GameObject[] layers;
    public float ratio;

    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var diff = position - transform.position;
        position = transform.position;
        float r = 0;
        foreach (GameObject layer in layers) {
            r += ratio / layers.Length;
            var offset = diff * r * Time.deltaTime;
            layer.transform.position += offset;
        }
    }
}
