namespace RPGTest.Camera
{
    using UnityEngine;

    // Tips:
    // 1.ɾ���� follow �ϲ��� lookAt �Զ��������ת
    // 2.�Ż���˵��
    // 3.�޸��˷������ͷ�������λ��
    // 4.�Ż�����ת�Ƕȼ��㷽��
    // 5.�뽫�����������С��ֹ���� �����������С������ͷ���뾶
    // 6.����������������鿴�������ű��� X Y Z ���ž�������һ��

    public class MainCamera : MonoBehaviour
    {
        /// <summary>
        /// �����׼��λ��
        /// </summary>
        [Header("�������")] public Transform lookAtPos;
        /// <summary>
        /// ����۳���
        /// </summary>
        [Header("�����"), Range(0, 10)] public float armLeghth = 5;
        /// <summary>
        /// ����۴�ֱ�Ƕ�
        /// </summary>
        [Range(-90, 90)] public float armVerticalRadius = 45;
        /// <summary>
        /// �����ˮƽ�Ƕ�
        /// </summary>
        [Range(-180, 180)] public float armHorizontalRadius = -90;
        /// <summary>
        /// ˮƽ�ƶ���ת
        /// </summary>
        [Header("�����ת")] public bool horizontalInvert = true;
        /// <summary>
        /// ��ֱ�ƶ���ת
        /// </summary>
        public bool verticalInvert = true;
        /// <summary>
        /// Զ���ƶ���ת
        /// </summary>
        public bool distanceInvert = true;
        /// <summary>
        /// �����ת�ٶ�
        /// </summary>
        [Header("����ƶ��ٶ�")] public float horizontalSpeed = 1f;
        /// <summary>
        /// ��������ٶ�
        /// </summary>
        public float verticalSpeed = 1f;
        /// <summary>
        /// ���Զ���ٶ�
        /// </summary>
        public float distanceSpeed = 1f;

        private void LateUpdate()
        {
            SetCameraPos();
        }

        /// <summary>
        /// �������λ��
        /// ͨ������Բ�ϵ��λ��ȷ�����λ��
        /// </summary>
        void SetCameraPos()
        {
            // ��ȡ��ת�Ƕ�
            SetCameraRot();

            // �ȼ��� z/y ƽ������۵�λ�� �������ƫ��

            // Բ��Ϊ�����Ŀ��
            Vector3 center = lookAtPos.position;
            // ���ƴ�ֱ�Ƕ� ��Ϊ Cos 90��= 0
            armVerticalRadius = Mathf.Clamp(armVerticalRadius, -89.9f, 89.9f);
            // Բ�� z �Ḻ����Ϊ 0��
            float x0 = center.z;
            float y0 = center.y;
            // �¸�Բ�İ뾶
            float x1 = x0 + armLeghth * Mathf.Cos(armVerticalRadius * Mathf.Deg2Rad);
            // ��� y
            float y1 = y0 + armLeghth * Mathf.Sin(armVerticalRadius * Mathf.Deg2Rad);

            // �ټ��� x/z ƽ������۵�λ�� ���������ת��Χ

            // Բ�� x ��������Ϊ 0��
            float x2 = center.x;
            float y2 = center.z;
            // ��� x
            float x3 = x2 + Mathf.Abs(x1 - x0) * Mathf.Cos(armHorizontalRadius * Mathf.Deg2Rad);
            // ��� z
            float y3 = y2 + Mathf.Abs(x1 - x0) * Mathf.Sin(armHorizontalRadius * Mathf.Deg2Rad);

            // �������λ��
            transform.position = new Vector3(x3, y1, y3);
            transform.LookAt(lookAtPos);

            // ������
            GroundTest();
        }

        /// <summary>
        /// ���������ת
        /// </summary>
        void SetCameraRot()
        {
            // ��������������ת ����Ҫ����ɾ�����д���
            if (Cursor.lockState != CursorLockMode.Locked) return;

            // ������ת
            float horizontalRotaion = horizontalInvert ? -Input.GetAxis("Mouse X") : Input.GetAxis("Mouse X");
            armHorizontalRadius += horizontalRotaion * horizontalSpeed;
            // ��ת������ ��180�� ����
            if (armHorizontalRadius > 180 || armHorizontalRadius < -180) armHorizontalRadius = -armHorizontalRadius;
            armHorizontalRadius = Mathf.Clamp(armHorizontalRadius, -180, 180);

            // ������ת
            float verticalRotation = verticalInvert ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");
            armVerticalRadius += verticalRotation * verticalSpeed;

            // ���Զ��
            float distance = distanceInvert ? -Input.GetAxis("Mouse ScrollWheel") : Input.GetAxis("Mouse ScrollWheel");
            armLeghth += distance * distanceSpeed;
            // ���ܴ��� LookAt Ŀ��
            armLeghth = Mathf.Max(Camera.main.nearClipPlane, armLeghth);
        }

        /// <summary>
        /// ǽ�ڼ�� ��ֹ��͸
        /// ƽ�����߰汾
        /// </summary>
        void GroundTest()
        {
            // ���������
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // ��������λ��
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // ������ �����ǰλ��
            Debug.DrawLine(transform.position, _center, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // ������ lookAt ����λ��
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // ������ ���߷�Χ
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 0.1f);
            Debug.DrawLine(lookAtPos.position + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 0.1f);

            // �����������������
            if (Physics.BoxCast(lookAtPos.position, new Vector3(_cameraWidth * .5f, _cameraHeight * .5f), -transform.forward, out RaycastHit _hit, Quaternion.LookRotation(-transform.forward), Vector3.Distance(lookAtPos.position, _center), LayerMask.GetMask("Ground")))
            {
                // ������ ��ײ��
                Debug.DrawLine(lookAtPos.position, _hit.point, Color.white, 0.1f);

                // ��ײ�����
                Vector3 _hitCenter = lookAtPos.position + Vector3.Project(_hit.point - lookAtPos.position, transform.position - lookAtPos.position);

                // ������ ��ײ�����λ��
                Debug.DrawLine(lookAtPos.position, _hitCenter, Color.green, 0.1f);

                // ���ǰ��
                transform.position = transform.position + transform.forward * Vector3.Distance(_center, _hitCenter);

                // ����������λ��
                _center = transform.position + transform.forward * Camera.main.nearClipPlane;

                // ������ �����ǰλ��
                Debug.DrawLine(transform.position, _center, Color.blue, 0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.blue, 0.1f);
                Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 0.1f);

            }
        }

        /// <summary>
        /// ǽ�ڼ�� ��ֹ��͸
        /// ƽ�����߰汾-�򻯰�
        /// </summary>
        void GroundTest3()
        {
            // ���������
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // ��������λ��
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // �����������������
            if (Physics.BoxCast(lookAtPos.position, new Vector3(_cameraWidth * .5f, _cameraHeight * .5f), -transform.forward, out RaycastHit _hit, Quaternion.LookRotation(-transform.forward), Vector3.Distance(lookAtPos.position, _center), LayerMask.GetMask("Ground")))
            {
                // ��ײ�����
                Vector3 _hitCenter = lookAtPos.position + Vector3.Project(_hit.point - lookAtPos.position, transform.position - lookAtPos.position);
                // ���ǰ��
                transform.position = transform.position + transform.forward * Vector3.Distance(_center, _hitCenter);
            }
        }

        /// <summary>
        /// ǽ�ڼ�� ��ֹ��͸
        /// ˮƽ���߰汾
        /// </summary>
        void GroundTest2()
        {
            RaycastHit _hit;
            // �������
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // ������ ����
            Debug.DrawLine(new Vector3(6.237049f, 0.702f, -7.666428f) + Vector3.forward * 100f, new Vector3(6.237049f, 0.702f, -7.666428f) + Vector3.back * 100f, Color.white, 2f);

            // ���������
            Vector3 _center = transform.position + transform.forward * Camera.main.nearClipPlane;

            // ������ ���-���� ��
            Debug.DrawLine(transform.position, _center, Color.red, 2f);
            // ������ ����±߿� ��
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.blue, 2f);
            // ������ �����߿� ��
            Debug.DrawLine(_center - transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 2f);
            // ������ ����ϱ߿� ��
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center - transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, Color.red, 2f);
            // ������ ����ұ߿� ��
            Debug.DrawLine(_center + transform.right * _cameraWidth * .5f + transform.up * _cameraHeight * .5f, _center + transform.right * _cameraWidth * .5f - transform.up * _cameraHeight * .5f, Color.red, 2f);

            // ------------------��������컨��-------------------

            // �����/�� Ĭ��Ϊ��
            Vector3 _detectionDirection = transform.up;
            // ����-box ���������-�����/�ϱ߿�
            if (!Physics.BoxCast(_center, new Vector3(_cameraWidth * .5f, 0, 0), _detectionDirection, out _hit, Quaternion.identity, _cameraHeight * .5f, LayerMask.GetMask("Ground")))
            {
                Physics.BoxCast(_center, new Vector3(_cameraWidth * .5f, 0, 0), -_detectionDirection, out _hit, Quaternion.identity, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
                _detectionDirection = -_detectionDirection;
            }
            // ������ �������ײ ������λ�õ�����-��ײ�� ��
            if (_hit.collider) Debug.DrawLine(_center, _hit.point, Color.yellow, 2f);

            // �������߳���
            float _lookAtRayLength = Vector3.Distance(lookAtPos.position, _center);
            // �����������
            Vector3 _lookAtRayPos = lookAtPos.position + _detectionDirection * _cameraHeight * .5f; ;
            // ������ ����߿�����-������� ��
            Debug.DrawLine(_center + _detectionDirection * _cameraHeight * .5f, _lookAtRayPos, Color.green, 2f);

            // ����-box �����������-����߿�
            if (Physics.BoxCast(_lookAtRayPos, new Vector3(_cameraWidth * .5f, 0, 0), -transform.forward, out _hit, Quaternion.identity, _lookAtRayLength, LayerMask.GetMask("Ground")))
            {
                // ������ �����������-��ײ�� ��
                Debug.DrawLine(_lookAtRayPos, _hit.point, Color.yellow, 2f);
                // ������ײ���������ߵ�ͶӰ
                Vector3 _hitCenter = _lookAtRayPos + Vector3.Project(_hit.point - _lookAtRayPos, _center + _detectionDirection * _cameraHeight * .5f - _lookAtRayPos);
                // ������ �����������-��ײ���ĵ� ��
                Debug.DrawLine(_lookAtRayPos, _hitCenter, Color.red, 2f);
                // �������λ��
                transform.position += transform.forward * Vector3.Distance(_hitCenter, _center + _detectionDirection * _cameraHeight * .5f);
            }

        }

        /// <summary>
        /// ǽ�ڼ�� ��ֹ��͸
        /// ���߰汾
        /// </summary>
        void GroundTest1()
        {
            // ���ñ���
            Vector3 p1, p2, p3, p4;
            RaycastHit _hit;
            float _p3RayLength;

            // ���������
            float _cameraHeight = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2) * Camera.main.nearClipPlane * 2;
            float _cameraWidth = Camera.main.aspect * _cameraHeight;

            // ------------------��������컨��-------------------

            // �����ﳯ�������һ������ ��� p1
            Physics.Raycast(new Ray(lookAtPos.position, -transform.forward), out _hit, armLeghth, LayerMask.GetMask("Ground"));
            p1 = _hit.collider ? _hit.point : transform.position;
            Debug.DrawLine(lookAtPos.position, p1, Color.red, 2f); // ��

            // p1 ǰ�� ��� p2
            p2 = p1 + transform.forward * Camera.main.nearClipPlane;
            Debug.DrawLine(p1, p2, Color.green, 2f);  // ��

            // p2 ����/������ ��� p3
            _p3RayLength = Vector3.Distance(p2, lookAtPos.position);
            // �����¼�� û�м�⵽�����ϼ��
            Physics.Raycast(new Ray(p2, -transform.up), out _hit, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
            if (!_hit.collider)
            {
                Physics.Raycast(new Ray(p2, transform.up), out _hit, _cameraHeight * .5f, LayerMask.GetMask("Ground"));
                p3 = p2 + transform.up * _cameraHeight * .5f;
            }
            else p3 = p2 - transform.up * _cameraHeight * .5f;
            Debug.DrawLine(p2, p3, Color.blue, 2f);  // ��

            // ������ p3 ������� p4
            Physics.Raycast(new Ray(p3 + transform.forward * _p3RayLength, -transform.forward), out _hit, _p3RayLength, LayerMask.GetMask("Ground"));
            p4 = _hit.collider ? _hit.point : p3;
            Debug.DrawRay(p3, transform.forward * _p3RayLength, Color.black, 2f);

            // ���ǰ��
            transform.position = p1 + transform.forward * Vector3.Distance(p3, p4);

            // ------------------�����/��ǽ-------------------

            // ���¼��� p1
            Physics.Raycast(new Ray(lookAtPos.position, -transform.forward), out _hit, Vector3.Distance(lookAtPos.position, transform.position), LayerMask.GetMask("Ground"));
            p1 = _hit.collider ? _hit.point : transform.position;
            Debug.DrawLine(lookAtPos.position, p1, Color.red, 2f); // ��

            // ���¼��� p2
            p2 = p1 + transform.forward * Camera.main.nearClipPlane;
            Debug.DrawLine(p1, p2, Color.green, 2f);  // ��

            // p2 ����/������ ��� p3
            _p3RayLength = Vector3.Distance(p2, lookAtPos.position);
            // �������� û�м�⵽�����Ҽ��
            Physics.Raycast(new Ray(p2, -transform.right), out _hit, _cameraWidth * .5f, LayerMask.GetMask("Ground"));
            if (!_hit.collider)
            {
                Physics.Raycast(new Ray(p2, transform.right), out _hit, _cameraWidth * .5f, LayerMask.GetMask("Ground"));
                p3 = p2 + transform.right * _cameraWidth * .5f;
            }
            else p3 = p2 - transform.right * _cameraWidth * .5f;
            Debug.DrawLine(p2, p3, Color.blue, 2f);  // ��

            // ������ p3 ������� p4
            Physics.Raycast(new Ray(p3 + transform.forward * _p3RayLength, -transform.forward), out _hit, _p3RayLength, LayerMask.GetMask("Ground"));
            p4 = _hit.collider ? _hit.point : p3;
            Debug.DrawLine(p3, p4, Color.black, 2f);

            // ���ǰ��
            transform.position = p1 + transform.forward * Vector3.Distance(p3, p4);

            //TODO: �޸���Ե��ģ

        }
    }
}