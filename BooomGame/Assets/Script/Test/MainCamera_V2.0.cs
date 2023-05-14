namespace RPGTest.Camera
{
    using UnityEngine;

    // Tips:
    // 1.删除了 follow 合并到 lookAt 自动跟随和旋转
    // 2.优化了说明
    // 3.修改了方法名和方法调用位置
    // 4.优化了旋转角度计算方法
    // 5.请将相机最近距离调小防止抖动 建议最近距离小于人物头部半径
    // 6.如果出现相机抖动请查看物体缩放比例 X Y Z 缩放尽量保持一致

    public class MainCamera : MonoBehaviour
    {
        /// <summary>
        /// 相机对准的位置
        /// </summary>
        [Header("相机跟随")] public Transform lookAtPos;
        /// <summary>
        /// 相机臂长度
        /// </summary>
        [Header("相机臂"), Range(0, 10)] public float armLeghth = 5;
        /// <summary>
        /// 相机臂垂直角度
        /// </summary>
        [Range(-90, 90)] public float armVerticalRadius = 45;
        /// <summary>
        /// 相机臂水平角度
        /// </summary>
        [Range(-180, 180)] public float armHorizontalRadius = -90;
        /// <summary>
        /// 水平移动反转
        /// </summary>
        [Header("相机反转")] public bool horizontalInvert = true;
        /// <summary>
        /// 垂直移动反转
        /// </summary>
        public bool verticalInvert = true;
        /// <summary>
        /// 远近移动反转
        /// </summary>
        public bool distanceInvert = true;
        /// <summary>
        /// 相机旋转速度
        /// </summary>
        [Header("相机移动速度")] public float horizontalSpeed = 1f;
        /// <summary>
        /// 相机上下速度
        /// </summary>
        public float verticalSpeed = 1f;
        /// <summary>
        /// 相机远近速度
        /// </summary>
        public float distanceSpeed = 1f;

        private void LateUpdate()
        {
            SetCameraPos();
        }

        /// <summary>
        /// 设置相机位置
        /// 通过计算圆上点的位置确定相机位置
        /// </summary>
        void SetCameraPos()
        {
            // 获取旋转角度
            SetCameraRot();

            // 先计算 z/y 平面相机臂的位置 计算相机偏移

            // 圆心为跟随的目标
            Vector3 center = lookAtPos.position;
            // 限制垂直角度 因为 Cos 90°= 0
            armVerticalRadius = Mathf.Clamp(armVerticalRadius, -89.9f, 89.9f);
            // 圆心 z 轴负方向为 0°
            float x0 = center.z;
            float y0 = center.y;
            // 下个圆的半径
            float x1 = x0 + armLeghth * Mathf.Cos(armVerticalRadius * Mathf.Deg2Rad);
            // 相机 y
            float y1 = y0 + armLeghth * Mathf.Sin(armVerticalRadius * Mathf.Deg2Rad);

            // 再计算 x/z 平面相机臂的位置 计算相机旋转范围

            // 圆心 x 轴正方向为 0°
            float x2 = center.x;
            float y2 = center.z;
            // 相机 x
            float x3 = x2 + Mathf.Abs(x1 - x0) * Mathf.Cos(armHorizontalRadius * Mathf.Deg2Rad);
            // 相机 z
            float y3 = y2 + Mathf.Abs(x1 - x0) * Mathf.Sin(armHorizontalRadius * Mathf.Deg2Rad);

            // 设置相机位置
            transform.position = new Vector3(x3, y1, y3);
            transform.LookAt(lookAtPos);

            // 地面检测
            GroundTest();
        }

        /// <summary>
        /// 设置相机旋转
        /// </summary>
        void SetCameraRot()
        {
            // 鼠标锁定后才能旋转 不需要可以删除该行代码
            if (Cursor.lockState != CursorLockMode.Locked) return;

            // 左右旋转
            float horizontalRotaion = horizontalInvert ? -Input.GetAxis("Mouse X") : Input.GetAxis("Mouse X");
            armHorizontalRadius += horizontalRotaion * horizontalSpeed;
            // 旋转控制在 ±180° 以内
            if (armHorizontalRadius > 180 || armHorizontalRadius < -180) armHorizontalRadius = -armHorizontalRadius;
            armHorizontalRadius = Mathf.Clamp(armHorizontalRadius, -180, 180);

            // 上下旋转
            float verticalRotation = verticalInvert ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");
            armVerticalRadius += verticalRotation * verticalSpeed;

            // 相机远近
            float distance = distanceInvert ? -Input.GetAxis("Mouse ScrollWheel") : Input.GetAxis("Mouse ScrollWheel");
            armLeghth += distance * distanceSpeed;
            // 不能穿过 LookAt 目标
            armLeghth = Mathf.Max(Camera.main.nearClipPlane, armLeghth);
        }

        /// <summary>
        /// 墙壁检测 防止穿透
        /// 平面射线版本
        /// </summary>
        void GroundTest()
        {
            // 求出相机宽高
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // 求出最近的位置
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // 辅助线 相机当前位置
            Debug.DrawLine(transform.position, _center, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // 辅助线 lookAt 射线位置
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // 辅助线 射线范围
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // 从人物往摄像机射线
            if (Physics.BoxCast(lookAtPos.position, new Vector3(_cameraWidth * .5f, _cameraHeight * .5f), -transform.forward, out RaycastHit _hit, Quaternion.LookRotation(-transform.forward), Vector3.Distance(lookAtPos.position, _center), LayerMask.GetMask("Ground")))
            {
                // 辅助线 碰撞点
                Debug.DrawLine(lookAtPos.position, _hit.point, Color.white, 0.1f);

                // 碰撞点居中
                Vector3 _hitCenter = lookAtPos.position + Vector3.Project(_hit.point - lookAtPos.position, transform.position - lookAtPos.position);

                // 辅助线 碰撞点居中位置
                Debug.DrawLine(lookAtPos.position, _hitCenter, Color.green, 0.1f);

                // 相机前移
                transform.position = transform.position + transform.forward * Vector3.Distance(_center, _hitCenter);

                // 求出最近的新位置
                _center = transform.position + transform.forward * Camera.main.nearClipPlane;

                // 辅助线 相机当前位置
                Debug.DrawLine(transform.position, _center, Color.blue, 0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 0.1f);

            }
        }

        /// <summary>
        /// 墙壁检测 防止穿透
        /// 平面射线版本-简化版
        /// </summary>
        void GroundTest3()
        {
            // 求出相机宽高
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // 求出最近的位置
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // 从人物往摄像机射线
            if (Physics.BoxCast(lookAtPos.position, new Vector3(_cameraWidth * .5f, _cameraHeight * .5f), -transform.forward, out RaycastHit _hit, Quaternion.LookRotation(-transform.forward), Vector3.Distance(lookAtPos.position, _center), LayerMask.GetMask("Ground")))
            {
                // 碰撞点居中
                Vector3 _hitCenter = lookAtPos.position + Vector3.Project(_hit.point - lookAtPos.position, transform.position - lookAtPos.position);
                // 相机前移
                transform.position = transform.position + transform.forward * Vector3.Distance(_center, _hitCenter);
            }
        }

        /// <summary>
        /// 墙壁检测 防止穿透
        /// 水平射线版本
        /// </summary>
        void GroundTest2()
        {
            RaycastHit _hit;
            // 相机框宽高
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // 辅助线 地面
            Debug.DrawLine(new Vector3(6.237049f, 0.702f, -7.666428f) + Vector3.forward * 100f, new Vector3(6.237049f, 0.702f, -7.666428f) + Vector3.back * 100f, Color.white, 2f);

            // 相机框中心
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // 辅助线 相机-中心 红
            Debug.DrawLine(transform.position, _center, Color.red, 2f);
            // 辅助线 相机下边框 蓝
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 2f);
            // 辅助线 相机左边框 红
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 2f);
            // 辅助线 相机上边框 红
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 2f);
            // 辅助线 相机右边框 红
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 2f);

            // ------------------检测地面和天花板-------------------

            // 检测上/下 默认为上
            Vector3 _detectionDirection = transform.up;
            // 射线-box 相机框中心-相机下/上边框
            if (!Physics.BoxCast(_center, new Vector3(_cameraWidth * .5f, 0, 0), _detectionDirection, out _hit, Quaternion.identity, _cameraHeight * .5f, LayerMask.GetMask("Ground")))
            {
                Physics.BoxCast(_center, new Vector3(_cameraWidth * .5f, 0, 0), -_detectionDirection, out _hit, Quaternion.identity, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
                _detectionDirection = -_detectionDirection;
            }
            // 辅助线 如果有碰撞 相机最近位置的中心-碰撞点 黄
            if (_hit.collider) Debug.DrawLine(_center, _hit.point, Color.yellow, 2f);

            // 计算射线长度
            float _lookAtRayLength = Vector3.Distance(lookAtPos.position, _center);
            // 计算射线起点
            Vector3 _lookAtRayPos = lookAtPos.position + _detectionDirection * _cameraHeight * .5f; ;
            // 辅助线 相机边框中心-射线起点 绿
            Debug.DrawLine(_center + _detectionDirection * _cameraHeight * .5f, _lookAtRayPos, Color.green, 2f);

            // 射线-box 人物射线起点-相机边框
            if (Physics.BoxCast(_lookAtRayPos, new Vector3(_cameraWidth * .5f, 0, 0), -transform.forward, out _hit, Quaternion.identity, _lookAtRayLength, LayerMask.GetMask("Ground")))
            {
                // 辅助线 人物射线起点-碰撞点 黄
                Debug.DrawLine(_lookAtRayPos, _hit.point, Color.yellow, 2f);
                // 计算碰撞点向中心线的投影
                Vector3 _hitCenter = _lookAtRayPos + Vector3.Project(_hit.point - _lookAtRayPos, _center + _detectionDirection * _cameraHeight * .5f - _lookAtRayPos);
                // 辅助线 人物射线起点-碰撞中心点 红
                Debug.DrawLine(_lookAtRayPos, _hitCenter, Color.red, 2f);
                // 相机调整位置
                transform.position += transform.forward * Vector3.Distance(_hitCenter, _center + _detectionDirection * _cameraHeight * .5f);
            }

        }

        /// <summary>
        /// 墙壁检测 防止穿透
        /// 射线版本
        /// </summary>
        void GroundTest1()
        {
            // 设置变量
            Vector3 p1, p2, p3, p4;
            RaycastHit _hit;
            float _p3RayLength;

            // 求出相机宽高
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // ------------------检测地面和天花板-------------------

            // 从人物朝相机发射一条射线 求出 p1
            Physics.Raycast(new Ray(lookAtPos.position, -transform.forward), out _hit, armLeghth, LayerMask.GetMask("Ground"));
            p1 = _hit.collider ? _hit.point : transform.position;
            Debug.DrawLine(lookAtPos.position, p1, Color.red, 2f); // 红

            // p1 前移 求出 p2
            p2 = p1 + transform.forward * Camera.main.nearClipPlane;
            Debug.DrawLine(p1, p2, Color.green, 2f);  // 绿

            // p2 向上/下射线 求出 p3
            _p3RayLength = Vector3.Distance(p2, lookAtPos.position);
            // 先向下检测 没有检测到则向上检测
            Physics.Raycast(new Ray(p2, -transform.up), out _hit, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
            if (!_hit.collider)
            {
                Physics.Raycast(new Ray(p2, transform.up), out _hit, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
                p3 = p2 + transform.up * _cameraHeight * .5f;
            }
            else p3 = p2 - transform.up * _cameraHeight * .5f;
            Debug.DrawLine(p2, p3, Color.blue, 2f);  // 蓝

            // 人物向 p3 射线求出 p4
            Physics.Raycast(new Ray(p3 + transform.forward * _p3RayLength, -transform.forward), out _hit, _p3RayLength, LayerMask.GetMask("Ground"));
            p4 = _hit.collider ? _hit.point : p3;
            Debug.DrawRay(p3, transform.forward * _p3RayLength, Color.black, 2f);

            // 相机前移
            transform.position = p1 + transform.forward * Vector3.Distance(p3, p4);

            // ------------------检测左/右墙-------------------

            // 重新计算 p1
            Physics.Raycast(new Ray(lookAtPos.position, -transform.forward), out _hit, Vector3.Distance(lookAtPos.position, transform.position), LayerMask.GetMask("Ground"));
            p1 = _hit.collider ? _hit.point : transform.position;
            Debug.DrawLine(lookAtPos.position, p1, Color.red, 2f); // 红

            // 重新计算 p2
            p2 = p1 + transform.forward * Camera.main.nearClipPlane;
            Debug.DrawLine(p1, p2, Color.green, 2f);  // 绿

            // p2 向左/右射线 求出 p3
            _p3RayLength = Vector3.Distance(p2, lookAtPos.position);
            // 先向左检测 没有检测到则向右检测
            Physics.Raycast(new Ray(p2, -transform.right), out _hit, _cameraWidth * .5f, LayerMask.GetMask("Ground"));
            if (!_hit.collider)
            {
                Physics.Raycast(new Ray(p2, transform.right), out _hit, _cameraWidth * .5f, LayerMask.GetMask("Ground"));
                p3 = p2 + transform.right * _cameraWidth * .5f;
            }
            else p3 = p2 - transform.right * _cameraWidth * .5f;
            Debug.DrawLine(p2, p3, Color.blue, 2f);  // 蓝

            // 人物向 p3 射线求出 p4
            Physics.Raycast(new Ray(p3 + transform.forward * _p3RayLength, -transform.forward), out _hit, _p3RayLength, LayerMask.GetMask("Ground"));
            p4 = _hit.collider ? _hit.point : p3;
            Debug.DrawLine(p3, p4, Color.black, 2f);

            // 相机前移
            transform.position = p1 + transform.forward * Vector3.Distance(p3, p4);

            //TODO: 修复边缘穿模

        }
    }
}