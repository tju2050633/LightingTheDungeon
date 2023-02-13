using System.Collections.Generic;
using UnityEngine;

namespace LightingTheDungeon
{
    public class LaserGun : MonoBehaviour
    {
        //Laser 本体
        private LineRenderer laser;
        //存储 Laser 经过的路径的列表
        public List<Vector3> laserPoint = new List<Vector3>();
        //设置反射次数
        public int times = 9;
        //设置折射率
        public float refraction = 1.5f;
        //设置起始角度
        public Vector3 startAngel = new Vector3(1, 1, 0);
        //设置光线宽度
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
            //清空旧的LaserPoint
            laserPoint.Clear();
            //从Laser Gun的位置出发
            Vector3 startPoint = transform.position;
            //发射方向
            Vector3 direction = startAngel;
            //第一个出发点
            laserPoint.Add(startPoint);

            for (int i = 0; i < times; i++)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(startPoint, direction);
                //添加射线击中点到路径中
                laserPoint.Add(hitInfo.point);
                if (hitInfo.collider.tag == "Mirror")
                {
                    //新的发射方向
                    direction = Vector2.Reflect(hitInfo.point - (Vector2)startPoint, hitInfo.normal);
                    //将下一次发射起点设定为击中点
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
        //折射光线
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