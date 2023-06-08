using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class StartYRotation : MonoBehaviour
    {
        private void Start()
        {
            Vector3 currentEluers = transform.eulerAngles;
            float randAngle = Random.Range(0, 180);
            if(Random.Range(0, 2) == 1)
                randAngle *= -1;
            currentEluers.y = randAngle;
            transform.eulerAngles = currentEluers;
            Destroy(this);
        }
    }
}