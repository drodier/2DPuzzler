using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private bool opened = false;

    // Update is called once per frame
    void Update()
    {
        if(!opened && GetComponent<ActivatableContent>().GetStatus())
            StartCoroutine(OpenAnimation());
    }

    IEnumerator OpenAnimation()
    {
        if(!opened)
        {
            opened = true;

            while(tilemap.color.a > 0)
            {
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, tilemap.color.a - 0.1f);
                yield return new WaitForSecondsRealtime(0.2f);
            }

            yield return null;

            Destroy(this.gameObject);
        }

        yield return null;
    }
}
