using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonArsenal
{
    public class PolygonSoundSpawn : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            Destroy(gameObject, 2f);
        }
    }
}
