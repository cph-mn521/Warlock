using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float AngleBetweenTwoPoints (Vector3 a, Vector3 b) {
        return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    public Camera _camera;
    void FixedUpdate () {

        #region Movement
        float x = Input.GetAxisRaw ("Horizontal");
        float z = Input.GetAxisRaw ("Vertical");

        Vector3 movement = new Vector3 (x, 0.0f, z);
        //rb.AddForce(movement * movementSpeed * Time.deltaTime, ForceMode.VelocityChange);

        #endregion

        #region Rotation
        //Get the Screen positions of the object
        Vector2 positionOnScreen = _camera.WorldToViewportPoint (transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2) _camera.ScreenToViewportPoint (Input.mousePosition);

        //Get the angle between the points
        float angle = -AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);
        transform.rotation = Quaternion.Euler (new Vector3 (0f, angle + -90f, 0f));

        #endregion        
        ClientSend.PlayerMovement (movement);

        if (Input.GetMouseButtonUp (0)) {
            Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit)) {

                ClientSend.castSpell (0, hit.point);
            }

        }
        if (Input.GetKey ("space")) {
            Ray ray = _camera.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit)) {

                ClientSend.castSpell (1,hit.point);

            }
        }

    }

}