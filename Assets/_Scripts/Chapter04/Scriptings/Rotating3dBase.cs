using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Mathematics
{
    public class Rotating3dBase : MonoBehaviour
    {
        public GameObject Capsule;
        // Start is called before the first frame update
        void Start()
        {
            //Xoay vật thể trong không gian 3 chiều
            RotateObjectIn3DSpace();
            //Hoà trộn giữa hai rotations
            CombineQuaternions();
        }

        // Update is called once per frame
        void Update()
        {
            BlendBetweenTwoRotations(Capsule);
        }
        void RotateObjectIn3DSpace()
        {
            var rotation = Quaternion.Euler(90, 0, 0);
            var input = new Vector3(0, 0, 1);
            var result = rotation * input;
            Debug.Log(result);
        }
        void BlendBetweenTwoRotations(GameObject gameObject){
            var identity = Quaternion.identity;
            var rotationsX = Quaternion.Euler(90, 0, 0);
            var halfwayRotated = Quaternion.Slerp(identity , rotationsX, 0.5f);
            gameObject.transform.rotation = halfwayRotated;
        }
        void CombineQuaternions()
        {
            var combinedRotation = Quaternion.Euler(90, 0, 0)
                                * Quaternion.Euler(0, 90, 0);
            Debug.Log(combinedRotation);
        }

    }

}
