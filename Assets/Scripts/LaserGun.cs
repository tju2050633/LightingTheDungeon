using System.Collections.Generic;
using UnityEngine;

namespace LightingTheDungeon
{
    public class LaserGun : MonoBehaviour
    {
        //Laser ����
        private LineRenderer laser;
        //�洢 Laser ������·�����б�
        public List<Vector3> laserPoint = new List<Vector3>();
        //���÷������
        public int times = 9;
        //����������
        public float refraction = 1.5f;
        //������ʼ�Ƕ�
        public Vector3 startAngel = new Vector3(1, 1, 0);
        //���ù��߿��
        public float lightWidth = 1;

        private void Start()
        {
            laser = transform.Find("Line").GetComponent<LineRenderer>();
            //laser.endWidth = lightWidth;
            //laser.startWidth = lightWidth;
        }
        private void Update()
        {
            CasetLaser();
            laser.positionCount = laserPoint.Count;
            laser.SetPositions(laserPoint.ToArray());
        }

        void CasetLaser()
        {
            //��վɵ�LaserPoint
            laserPoint.Clear();
            //��Laser Gun��λ�ó���
            Vector3 startPoint = transform.position;
            //���䷽��
            Vector3 direction = startAngel;
            //��һ��������
            laserPoint.Add(startPoint);

            for (int i = 0; i < times; i++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(startPoint, direction);
                //������߻��е㵽·����
                laserPoint.Add(hitInfo.point);
                if (hitInfo.collider.tag == "Mirror")
                {
                    //�µķ��䷽��
                    direction = Vector2.Reflect(hitInfo.point - (Vector2)startPoint, hitInfo.normal);
                    //����һ�η�������趨Ϊ���е�
                    startPoint = (Vector3)hitInfo.point + direction.normalized;
                }

                else if (hitInfo.collider.tag == "Glass")
                {
                    direction = Refract(hitInfo.point - (Vector2)startPoint, hitInfo.normal, refraction);
                    startPoint = (Vector3)hitInfo.point + direction.normalized;
                }

                else if (hitInfo.collider.tag == "MoonStone")
                {
                    MoonStone moonStone = hitInfo.collider.gameObject.GetComponent<MoonStone>();
                    moonStone.boolean = true; ;
                    moonStone.FixedUpdate();
                    return;
                }

                else if (hitInfo.collider.tag == "Werewolf")
                {
                    AudioManager.Instance.PlaySound("WolfDie");
                    Destroy(hitInfo.collider.gameObject);
                }

                else if (hitInfo.collider.tag == "Vampire")
                {
                    AudioManager.Instance.PlaySound("VampireDie");
                    Destroy(hitInfo.collider.gameObject);
                }

                else
                    return;
            }
        }
        //�������
        public Vector2 Refract(Vector2 incidentRays, Vector2 normal, float refraction)
        {
            incidentRays = incidentRays.normalized;
            normal = normal.normalized;

            float A = Vector3.Dot(incidentRays, normal);
            float B = 1.0f - refraction * refraction * (1.0f - A * A);
            Vector3 reflectRay = refraction * incidentRays - (refraction * A + Mathf.Sqrt(B)) * normal;
            if (B > 0)
                return reflectRay;
            else
                return Vector2.zero;
        }
    }
}