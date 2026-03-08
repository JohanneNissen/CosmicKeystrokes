using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public float parralax = 2f;
 
    void Update()
    {

        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;

        offset.y = offset.y + .001f *parralax;
        mat.mainTextureOffset = offset;


    }
}
