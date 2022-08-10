using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabParabola : PrefabObject
{


    private void Start()
    {
        StartCoroutine(Parabola());
    }

    IEnumerator Parabola()
    {
        yield return null;

        dir = target.transform.position - gameObject.transform.position;
        float tmp = dir.magnitude;
        float tmptime = 0f;
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, tmp * Time.deltaTime);
            tmptime += Time.deltaTime; 
            if(tmptime <= 0.5f)
            {
                transform.position += Vector3.up * Time.deltaTime * 60;
                
            }
            else if ( tmptime >= 0.5f)
            {
                transform.position -= Vector3.up * Time.deltaTime * 60;
            }
            else
            {
                Destroy(gameObject);
            }
            //this.transform.Translate(transform.forward + (Time.deltaTime * dir));


            yield return null;
        }

    }

    //public Transform Target;
    //public float firingAngle = 45.0f;
    //public float gravity = 9.8f;

    //public Transform Projectile;      
    //private Transform myTransform;

    //void Awake()
    //{
    //    myTransform = transform;      
    //}

    //void Start()
    //{
    //    myTransform = gameObject.transform;
    //    Projectile = gameObject.transform;
    //    StartCoroutine(SimulateProjectile());
    //}


    //IEnumerator SimulateProjectile()
    //{
    //    // Short delay added before Projectile is thrown
    //    yield return new WaitForSeconds(1.5f);

    //    // Move projectile to the position of throwing object + add some offset if needed.
    //    Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
    //    Target = target.transform;
    //    // Calculate distance to target
    //    float target_Distance = Vector3.Distance(Projectile.position, Target.position);

    //    // Calculate the velocity needed to throw the object to the target at specified angle.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    // Extract the X  Y componenent of the velocity
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad) ;
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad) ;

    //    // Calculate flight time.
    //    float flightDuration = target_Distance / Vx;

    //    // Rotate projectile to face the target.
    //    Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

    //    float elapse_time = 0;

    //    while (elapse_time < flightDuration)
    //    {
    //        Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }
    //}  

    //public float m_Speed = 10;
    //public float m_HeightArc = 1;
    //private Vector3 m_StartPosition;


    //void Start()
    //{
    //    m_StartPosition = transform.position;
    //}

    //void Update()
    //{



    //    float x0 = m_StartPosition.x;
    //    float x1 = destination.x;
    //    float distance = x1 - x0;
    //    float nextX = Mathf.MoveTowards(transform.position.x, x1, m_Speed * Time.deltaTime);
    //    float baseY = Mathf.Lerp(m_StartPosition.y, destination.y, (nextX - x0) / distance);
    //    float arc = m_HeightArc * (nextX - x0) * (nextX - x1) / (-0.25f * distance * distance);
    //    Vector3 nextPosition = new Vector3(nextX, baseY + arc, transform.position.z);

    //    transform.rotation = LookAt2D(nextPosition - transform.position);
    //    transform.position = nextPosition;


    //    if (nextPosition == destination)
    //        Arrived();

    //}

    //void Arrived()
    //{
    //    Debug.Log("도착");
    //    //Destroy(gameObject);
    //}

    //Quaternion LookAt2D(Vector2 forward)
    //{
    //    return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    //}

}




