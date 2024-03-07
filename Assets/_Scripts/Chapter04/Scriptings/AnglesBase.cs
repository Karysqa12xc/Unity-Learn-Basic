using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Mathematics
{
    public class AnglesBase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Chuyển đổi từ độ sang radian
            ConvertDegreeToRadian();
            //Chuyển đổi từ radian sang độ
            ConvertRadianToDegree();
            //Cách tích góc giữa hai vector
            CalculatedBetweenTwoVector();
        }

        void ConvertDegreeToRadian(){
            var radians  = 90 * Mathf.Deg2Rad;
            Debug.Log(radians);
        }
        void ConvertRadianToDegree(){
            var degree = 2 * Mathf.PI * Mathf.Rad2Deg;
            Debug.Log(degree);
        }
        void CalculatedBetweenTwoVector(){
            var angle = Mathf.Acos(Vector3.Dot(Vector3.up, Vector3.left));
            Debug.Log(angle * Mathf.Rad2Deg);
        }
    }
}

