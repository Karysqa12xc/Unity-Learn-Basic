using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
namespace Chapter.Mathematics
{
    public class VectorBase : MonoBehaviour
    {
        public GameObject g1, g2;
        // Start is called before the first frame update
        void Start()
        {
            //Math with vector
            var v1 = new Vector3(1f, 2f, 3f);
            var v2 = new Vector3(0f, 1f, 6f);
            Debug.LogFormat($"Add Tow Vector3 {AddTwoVector(v1, v2)}");
            Debug.LogFormat($"Sub Tow Vector3 {SubTwoVector(v1, v2)}");
            //magnitude of vector
            var vectorMagnitude = new Vector3(2f, 5f, 3f).magnitude;
            Debug.LogFormat($"Magnitude of Vector {vectorMagnitude}");

            //Cách tính khoảng cách giữa hai vector
            var point1 = new Vector3(5f, 1f, 0f);
            var point2 = new Vector3(7f, 0f, 2f);
            Debug.LogFormat("Distance between Point1 and Point2 is {0}",
            DistanceBetween2Point(point1, point2));
            //Cách tính khoảng cách giữa hai vector mà sử dụng hàm
            Debug.LogFormat("Distance when I use method of Vector: {0}", Vector3.Distance(point1, point2));
            //Chuẩn hoá Vector theo cách thủ công
            var bigVector = new Vector3(4, 7, 9);
            Debug.LogFormat("Normalized of Vector: {0}", NormalizedVector(bigVector));
            //Chuẩn hoá Vector bằng hàm có sẵn
            var unitVector = bigVector.normalized;
            Debug.LogFormat("Normalized of Vector with method: {0}", unitVector);
            //Tính tích vô hướng bằng hàm của Vector3
            DotProduceBetween2Vector(bigVector);

        }
        void Update()
        {
            MoveTowardsBetween2Vector(g1, g2);
        }
        Vector3 AddTwoVector(Vector3 v1, Vector3 v2)
        {
            return v1 + v2;
        }
        Vector3 SubTwoVector(Vector3 v1, Vector3 v2)
        {
            return v1 - v2;
        }
        float DistanceBetween2Point(Vector3 v1, Vector3 v2)
        {
            var distance = (v2 - v1);
            return distance.magnitude;
        }
        Vector3 NormalizedVector(Vector3 v1)
        {
            return v1 / v1.magnitude;
        }
        void DotProduceBetween2Vector(Vector3 v1)
        {

            //2 vector cùng hướng sẽ cho ra kết quả là > 1
            var SameDirection = Vector3.Dot(v1, v1 / v1.magnitude);
            Debug.LogFormat("Same Direction Vector: {0}", SameDirection);
            //2 vector khác hướng sẽ cho ra kết quả là -1
            var DifferentDirection = Vector3.Dot(Vector3.left, Vector3.right);
            Debug.LogFormat("Different Direction Vector: {0}", DifferentDirection);
            //2 vector vuông góc sẽ cho ra kết quả là 0
            var RightAngle = Vector3.Dot(Vector3.up, Vector3.left);
            Debug.LogFormat("Right Angle Vector: {0}", RightAngle);
        }
        void MoveTowardsBetween2Vector(GameObject g1, GameObject g2)
        {
            g1.transform.position = Vector3.MoveTowards(g1.transform.position, g2.transform.position, 0.5f * Time.deltaTime);
        }
    }
}


