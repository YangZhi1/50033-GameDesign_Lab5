using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab; // the spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark
    public Sprite originalBox;

    private Animator questionBoxAnimator;

    private bool hit = false;

    private Vector2 boxSize;

    // Start is called before the first frame update
    void Start()
    {
        questionBoxAnimator = GetComponent<Animator>();
        boxSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            foreach (ContactPoint2D hitPos in col.contacts)
            {
                Vector2 hitPoint = hitPos.point;

                if (hitPoint.y < transform.position.y - boxSize.y / 3) 
                {
                    hit = true;
                    questionBoxAnimator.SetBool("isHit", hit);

                    // ensure that we move this object sufficiently 
                    //rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);
                    // spawn the mushroom prefab slightly above the box
                    Instantiate(consummablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
                    StartCoroutine(DisableHittable());
                }
                return;
            }
        }
    }

    // this will return true when the object is stationary
    // we need to be sure that the object has moved before
    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false; // disable spring
    }

    public void onGameRestart()
    {
        hit = false;
        spriteRenderer.sprite = originalBox;
        questionBoxAnimator.SetBool("isHit", hit);
    }
}
