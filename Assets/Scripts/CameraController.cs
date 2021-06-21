using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    public Transform startLimit; // GameObject that indicates start of map
    private float offset; // initial x-offset between camera and Mario
    private float offsetY; // initial y-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;

    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject coinCountText;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject rules;

    private Vector2 scoreTextOffset; // initial offset between scoreText and camera
    private Vector2 coinCountOffset; // initial offset between coinCountText and camera
    private Vector2 homeScreenOffset; // initial offset between homeScreen and camera
    private Vector2 quitButtonOffset; // initial offset between quitButton and camera
    private Vector2 rulesOffset; // initial offest between rules and camera

    private bool playerWarped = false;

    private float originalCameraY;

    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        // get position of score and gameover text
        // scoreTextLocation = scoreText.transform.position;
        // gameoverLocation = gameover.transform.position;

        offset = this.transform.position.x - player.position.x;
        offsetY = this.transform.position.y - player.position.y;

        originalCameraY = this.transform.position.y;

        startX = startLimit.transform.position.x + viewportHalfWidth; // this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;

        scoreTextOffset = new Vector2(scoreText.transform.position.x - this.transform.position.x, scoreText.transform.position.y - this.transform.position.y);
        coinCountOffset = new Vector2(coinCountText.transform.position.x - this.transform.position.x, coinCountText.transform.position.y - this.transform.position.y);
        homeScreenOffset = new Vector2(homeScreen.transform.position.x - this.transform.position.x, homeScreen.transform.position.y - this.transform.position.y);
        quitButtonOffset = new Vector2(quitButton.transform.position.x - this.transform.position.x, quitButton.transform.position.y - this.transform.position.y);
        rulesOffset = new Vector2(rules.transform.position.x - this.transform.position.x, rules.transform.position.y - this.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerWarped)
        {
            float desiredX = player.position.x + offset;
            // check if desiredX is within startX and endX
            if (desiredX > startX && desiredX < endX)
                this.transform.position = new Vector3(desiredX, originalCameraY, this.transform.position.z);
        }
        else
        {
            float desiredX = player.position.x + offset;
            float desiredY = player.position.y + offsetY / 2;
            this.transform.position = new Vector3(desiredX, desiredY, this.transform.position.z);
        }

        // move score text to follow camera
        Vector2 desiredScorePosition = new Vector2(this.transform.position.x, this.transform.position.y) + scoreTextOffset;
        scoreText.transform.position = desiredScorePosition;

        // move coin count to follow camera
        Vector2 desiredCoinCountPosition = new Vector2(this.transform.position.x, this.transform.position.y) + coinCountOffset;
        coinCountText.transform.position = desiredCoinCountPosition;

        // move quit button to follow camera
        Vector2 desiredQuitButtonPosition = new Vector2(this.transform.position.x, this.transform.position.y) + quitButtonOffset;
        quitButton.transform.position = desiredQuitButtonPosition;
    }

    public void GameOverUpdate()
    {
        Vector2 desiredHomeScreenPosition = new Vector2(this.transform.position.x, this.transform.position.y) + homeScreenOffset;
        homeScreen.transform.position = desiredHomeScreenPosition;

        Vector2 desireRulesPosition = new Vector2(this.transform.position.x, this.transform.position.y) + rulesOffset;
        rules.transform.position = desireRulesPosition;
    }

    public void PlayerWarpedToToilet()
    {
        playerWarped = true;
    }

    public void PlayerWarpedBack()
    {
        playerWarped = false;
    }
}
