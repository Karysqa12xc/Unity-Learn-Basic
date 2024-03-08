# Unity Learn Basic
## Working with Unity
* **Short cut:**
    - Ctrl + N: Tạo một GameObject Empty
    - Alt + Shift + N: Tạo một GameObject Empty trong một GameObject khác
    - Ctrl + = : Trỏ vào đối tượng đầu tiền trong một đối list các đối tượng con của một đối tượng khác
    - Ctrl + - : Trỏ vào đối tượng cuối cùng trong một đối list các đối tượng con của một đối tượng khác
## Scripting 
* **Short:**
    - Object Pool: Tạo các prefabs với hiệu năng cao hơn so với destroy
    - Singleton: Một design pattern được sử dụng để code không bị lặp và được khởi tạo một lần duy nhất( tác hại là code sẽ dễ bị phụ thuộc nên cần phải lưu ý )
## Input:
* **Short:**
    - Input System: Được cài đặt bằng Package Manager, đây là hệ thống quản lý điều kiểu object do unity tạo ra.
    - Có thể sử dụng các thuộc tính của Cursor để tác động lên con trỏ chuột trong trò chơi.
    - Sử dụng Event Systems và Camera được gắn Component Physics Raycaster và các Interface(IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler,IPointerDownHandler,
    IPointerClickHandler) để chọn đối tượng trong trò chơi bằng con trỏ chuột.
## Mathematics(Vector, Rotating, Matrix, Angle)
* **Short:**
    - Tính khoảng cách giữa hai Vector: ![alt](Images/TinhKhoangCachGiuaHaiVector.png)
    - Chuẩn hoá Vector: ![alt](Images/ChuanHoaVector.png)
    - Tính tích vô hướng giữa hai Vector: ![alt](Images/TinhTichVoHuong.png)
    - Quaternion có 4 giá trị(x, y, z, w)
    - Cách thực hiện phép quay: ![alt](Images/QuayMotVatTheTrongGame.png)
    - Sử dụng hàm Slerp để hoà trộn hai góc quay với nhau: ![alt](Images/SuDungHamSlerpDeTronGocQuay.png)
    - Kết hợp Quaternion: ![alt](Images/KetHopHaiQuaternion.png)
    - Thao tác cơ bản với ma trận 4x4: ![alt](Images/ThaoTacCoBanVoiMaTran.png)
    - Cách tạo ma trận 4x4 bằng Vector: ![alt](Images/KhoiTaoMotMaTranBangVector.png)
    - Cách nhân vector với ma trận 4x4: ![alt](Images/NhanMaTranVoiVector.png)
    - Cách nối thủ công các ma trận với nhau: ![alt](Images/NoiMaTran.png)
    - Cách nối các ma trận với nhau nhưng sử dụng hàm: ![alt](Images/NoiCacMaTranNhungSuDungHam.png)
    - Cách chuyển đổi góc có giá trị là độ sang radian: ![alt](Images/ChuyenDoiTuDoSangRadian.png)
    - Cách chuyển đổi góc có giá trị là radian sang độ: ![alt](Images/ChuyenDoiTuRadianSangDo.png)
    - Để tính góc giữa hai vector ta sử dụng hàm Acos của Mathf đối với tích vô hướng của hai vector: ![alt](Images/CachTinhGocGiuaHaiVector.png)
## 2D Graphics
* **Short:**
    - Để sử dụng hình ảnh trong Unity2D ta chỉ cần kéo hình ảnh đó vào thư mục trong cửa sổ Project của Unity và chuyển Texture Type thành Sprite (2D and UI).
    - Để sử dụng Sprite trong Scene thì GameObject đó phải có Component tên là "Sprite Renderer".
    - Dùng Custom Physics Shape trong Sprite Editor để xác định hình dạng Collider của Polygon Collider 2d.
    - Dùng Component Composite Collider để gộp chung các Collider con với nhau:![alt](Images/HieuQuaKhiSuDungCompositeCollider.png)
    - Sử dụng Sprite Atlas để sử dụng một gói các Sprite cho một texture duy nhất để tránh lạng phí bộ nhớ.
    - Sử dụng Component "Constant Force 2D" để tác động một lực vào vật thể được gắn Component này.
    - Dùng Effector để mô phỏng lực tự nhiên và một số chức năng mở rộng(Area Effector, Buoyancy Effector, Platform Effector, Surface Effector)
    - Sử dụng Component "Sprite Group" để hiển thị đồng thời các ảnh của GameObject con trong GameObject cha.
    - Để tạo Scene 2.5D ta chỉnh Projection của Camera thành Perspective và tạo một Background ở xa Camera, lúc này nếu di chuyển Camera sang trái hoặc phải sẽ tạo cảm giác 3d.


