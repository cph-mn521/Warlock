using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed;
    public CharacterController controller;
    public Rigidbody rb;
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    void FixedUpdate()
    {

        #region Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3 (x, 0.0f, z);
        rb.AddForce(movement * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);

        #endregion
        //Vector3 toMove = new Vector3(x, 0f, z) * Time.deltaTime * movementSpeed;
        //controller.Move(toMove);
        #region Rotation
         //Get the Screen positions of the object
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
         
         //Get the Screen position of the mouse
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
         //Get the angle between the points
         float angle = -AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
         transform.rotation =  Quaternion.Euler (new Vector3(0f, angle + -90f, 0f));
        
        #endregion
        
        ClientSend.PlayerMovement(movement);
        
    }

}
