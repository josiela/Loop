using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Gravity : MonoBehaviour
{

    IEnumerable<Rigidbody2D> rigidbodyList;

    [SerializeField]
    private float forceRadius = 10f;
    public float ForceRadius {
        get { return forceRadius; }
        set { forceRadius = value; }
    }
    public float gravity = 9f;

    void FixedUpdate() {
        rigidbodyList = Physics2D.OverlapCircleAll(this.transform.position, ForceRadius)
                    .Where(c => c.attachedRigidbody != null)
                    .Select(c => c.attachedRigidbody);

        ApplyAlignment2D(rigidbodyList);

        foreach (var rigid in rigidbodyList) {
            rigid.gravityScale = 0f;

            ApplyForce(rigid, this.transform);
        }
    }

    public void ApplyForce(Rigidbody2D rigid, Transform trans) {
        Vector2 force = new Vector2(rigid.gameObject.transform.position.x, rigid.gameObject.transform.position.y).normalized;
        rigid.AddForce(-Vector2.Scale(force, new Vector2(gravity, gravity)), ForceMode2D.Force);
    }

    void ApplyAlignment2D(IEnumerable<Rigidbody2D> list) {
        foreach (var item in list) {
            var newRotation = Quaternion.LookRotation((item.transform.position - this.transform.position).normalized, Vector3.back);

            newRotation.x = 0.0f;
            newRotation.y = 0.0f;

            item.transform.rotation = Quaternion.Slerp(item.transform.rotation, newRotation, 1);
        }
    }
}
