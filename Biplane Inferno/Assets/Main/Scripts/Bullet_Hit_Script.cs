using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Hit_Script : MonoBehaviour {
    [SerializeField]
    Texture2D DamageMap;

    [SerializeField]
    Texture2D refMap;
    Collider2D myCollider;
    SpriteRenderer myRenderer;
    Sprite FixedDamageMap;
    Plane_Details myDetails;

    [SerializeField]
    GameObject myParticleSystem;

    private void Start()
    {;
        myDetails = GetComponent<Plane_Details>();
        refMap = new Texture2D(DamageMap.width, DamageMap.height);
        for (int i = 0; i < refMap.width; i++)
        {
            for (int j = 0; j < refMap.height; j++)
            {
                refMap.SetPixel(i, j, DamageMap.GetPixel(i, j));
            }
        }
        refMap.Apply();
        myCollider = GetComponent<PolygonCollider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        FixedDamageMap = myRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            float rootX = transform.position.x - myCollider.bounds.extents.x;
            float baseX = transform.position.x + myCollider.bounds.extents.x;
            float rootY = transform.position.y - myCollider.bounds.extents.y;
            float baseY = transform.position.y + myCollider.bounds.extents.y;
            foreach (ContactPoint2D col in collision.contacts)
            {
                if (col.collider.gameObject.tag == "Bullet")
                {
                    float hitPointX = transform.position.x - col.point.x;
                    float hitPointY = transform.position.y - col.point.y;
                    float normValX = 1 - (hitPointX - rootX) / (baseX - rootX);
                    float normValY = 1 - (hitPointY - rootY) / (baseY - rootY);

                    int tempX = (int)(normValX * refMap.width);
                    int tempY = (int)(normValY * refMap.height) - 25;
                    for (int i = -60; i < 60; i++)
                    {
                        for (int j = -60; j < 60; j++)
                        {
                            if (refMap.GetPixel(tempX + i, tempY + j).a != 0 && Vector2.Distance(new Vector2(tempX, tempY), new Vector2(tempX + i, tempY + j)) < 60)
                            {
                                refMap.SetPixel(tempX + i, tempY + j, refMap.GetPixel(tempX + i, tempY + j).linear + Color.white * 12 / Vector2.Distance(new Vector2(tempX, tempY), new Vector2(tempX + i, tempY + j)));
                            }
                        }
                    }
                    GameObject temp = Instantiate(myParticleSystem, new Vector3(col.point.x, col.point.y, transform.position.z - 1), Quaternion.identity);
                    temp.GetComponent<ParticleSystem>().Play();
                    Destroy(temp, 1f);
                }
            }
            refMap.Apply();
            //Sprite myNewSprite = Sprite.Create(refMap, new Rect(0, 0, refMap.width, refMap.height), new Vector2(.5f, .5f));
            myRenderer.material.SetTexture("_Damage", refMap);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Plane")
        {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Pickup")
        {
            myDetails.GetPickupData(collision.gameObject.GetComponent<Pickup_Base>());
        }
    }
}
