 using UnityEngine;

public class ScrollUV : MonoBehaviour
{

    public float parralax = 2f;
 
    void Update()
    {

        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;

        offset.y = transform.position.z / transform.localScale.z / parralax;
        mat.mainTextureOffset = offset;


    }
}
