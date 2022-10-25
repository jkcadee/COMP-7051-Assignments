using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{

    /**
     This script represents the score system, which is to be applied to a ball.
     */

    //Represents the Rigidbody of the ball.
    Rigidbody rb;

    // Represents the win popup (which states which player has won)
    public GameObject win_popup;

    //Represents the text that carries player 1's score.
    public TMP_Text p1Score;

    //Represents player 1's score.
    int p1_score_value;

    //Represents the text that carries player 2's score.
    public TMP_Text p2Score;

    //Represents player 2's score.
    int p2_score_value;

    //Represents the maximum number of points it takes to trigger win condition.
    int max_point;

    //Represents the text generated on the win condition, depending on which player wins.
    public TMP_Text win_text;

    //Represents the speed of the ball.
    public float speed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        //Win Screen is disabled as a precaution.
        Disable_Win_Screen();

        //Gets the ball's rigidbody component.
        rb = GetComponent<Rigidbody>();

        //Max points are set to 3.
        max_point = 3;

        //Player scores are set to zero at the start.
        p1_score_value = 0;
        p2_score_value = 0;

        //Updates the score texts accordingly.
        update_value();
    }


    /**
     Is triggered when the ball collides with an object.
    The function attempts to determine whether or not the ball
    hits a player's goal post. If it does, the opposite player
    receives one point. If that point is that player's 3rd point,
    it will trigger the win condition.
    @param other

     */
    private void OnCollisionEnter(Collision other)
    {
        //If ball collides with player 1's goal post, increment player 2's score by one.
        if (other.gameObject.tag == "P1_Goal") {
            p2_score_value += 1;
            update_value();
            //If player 2's score is 3 or more, trigger the win condition.
            if (p2_score_value >= max_point)
            {
                win_condition("Player 2");
            }
            //If not, place the ball back in the center, and trigger the next round.
            else
            {
                transform.position = new Vector3(1.0f, 1.0f, 1.0f);
                stop_ball();
                start_ball_movement(1);
            }
        }

        //If ball collides with player 2's goal post, increment player 1's score by one.
        if (other.gameObject.tag == "P2_Goal") {
            p1_score_value += 1;
            update_value();
            //If player 1's score is 3 or more, trigger the win condition.
            if (p1_score_value >= max_point)
            {
                win_condition("Player 1");
            }
            //If not, place the ball back in the center, and trigger the next round.
            else
            {
                transform.position = new Vector3(1.0f, 1.0f, 1.0f);
                stop_ball();
                start_ball_movement(-1);
            }
        }

    }

    /** 
     This function is called when the game is won by either
    player 1 or 2. It takes in a string that indicates which
    player has won the game. It takes that text, then applies
    it to the text object, which will display the winner on screen.
    @param result
     */
    private void win_condition(string result) {

        //Change the text to show who wins.
        win_text.text = result + " Wins!";
        //Show the win screen.
        Enable_Win_Screen();
        //Stop the ball.
        stop_ball();
        //PlayerConfigManager.Instance.ClearPlayerConfigs();

    }

    /** 
     Changes the score text on the screen to the current scores of the players
        in the script.
     */

    private void update_value()
    {
        p1Score.text = "Player 1: " + p1_score_value;
        p2Score.text = "Player 2: " + p2_score_value;
    }

    /** 
     Stops the ball and puts it back in the center of the screen.
     */

    private void stop_ball() {

        rb.position = new Vector3(0, 0.5f, 0);
        rb.velocity = new Vector3(0, 0, 0);

    }

    /** 
     Starts the ball's motion again after being stopped.
     */

    void start_ball_movement(int direction)
    {
        Disable_Win_Screen();
        Vector3 v3 = new Vector3(direction, 0, Random.Range(-0.9f, 0.9f)) * speed;
        rb.AddForce(v3, ForceMode.VelocityChange);
    }

    /** 
     Makes the win screen visible in the game.
     */

    public void Enable_Win_Screen()
    {
        win_popup.SetActive(true);
    }

    /** 
        Makes the win screen invisible in the game.
    */

    public void Disable_Win_Screen()
    {
        win_popup.SetActive(false);
    }

    /** 
        Restarts the game after a game has been stopped.
    */

    public void Restart_Game()
    {
        p1_score_value = 0;
        p2_score_value = 0;
        Disable_Win_Screen();
        start_ball_movement(1);
        update_value();
    }
}
