using Fusion;
using UnityEngine;

public class Ammo : NetworkBehaviour
{
    public int AmmoInMagazine = 10;
    public void DisableGrabLayer()
    {
        SetLayerRecursively(gameObject, 9);
    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
