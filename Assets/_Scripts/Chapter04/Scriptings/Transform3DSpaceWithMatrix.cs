using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chapter.Mathematics
{
    public class Transform3DSpaceWithMatrix : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var translationMatrix = new Matrix4x4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(5, 0, 0, 1)
            );
            //Thao tác với ma trận
            HandleWithMatrix();
            //Cách khởi tạo và nhân ma trận
            MatrixMultiply(translationMatrix);
            //Di chuyển ma trận bằng hàm có sẵn 
            TranslateMatrixUsingMethod();
            //Xoay ma trận quanh điểm gốc toạ độ
            RotateAPointAround();
            //Nối các ma trận
            CombineMatrix();
            //Nối các ma trận bằng hàm
            CombineMatrixWithMethod();
        }

        // Update is called once per frame
        void Update()
        {

        }
        void HandleWithMatrix()
        {
            var matrix = new Matrix4x4();
            var m00 = matrix[0, 0];
            matrix[0, 1] = 2f;
            Debug.Log(matrix);
        }
        void MatrixMultiply(Matrix4x4 matrix4X4)
        {
            Debug.Log(matrix4X4);
            var input = new Vector3(0, 1, 2);
            var result = matrix4X4.MultiplyPoint(input);
            Debug.Log(result);
        }
        void TranslateMatrixUsingMethod(){
            var input = new Vector3(0, 1, 2);
            var translationMatrix = Matrix4x4.Translate(new Vector3(5, 1, -2));
            var result = translationMatrix.MultiplyPoint(input);
            Debug.Log("Translate Matrix Using Method\n");
            Debug.Log(result);
        }
        void RotateAPointAround()
        {
            var rotate90DegreeAroundX = Quaternion.Euler(90, 0 , 0);

            var rotationMatrix = Matrix4x4.Rotate(rotate90DegreeAroundX);

            var input = new Vector3(0, 0, 1);

            var result = rotationMatrix.MultiplyPoint(input);
            Debug.Log("Rotation A Point Around\n");
            Debug.Log(result);
        }
        void CombineMatrix(){
            var translation = Matrix4x4.Translate(new Vector3(5, 0, 0));
            var rotation = Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0));
            var scale = Matrix4x4.Scale(new Vector3(1, 5, 1));
            
            var combined = translation * rotation * scale;

            var input = new Vector3(1, 1, 1);
            var result = combined.MultiplyPoint(input);
            Debug.Log(result);
        }
        void CombineMatrixWithMethod(){
            var transformMatrix = Matrix4x4.TRS(
                new Vector3(5, 0 ,0),
                Quaternion.Euler(90, 0 , 0),
                new Vector3(1, 5, 1)
            );
            Debug.Log(transformMatrix);
        }
    }
}

