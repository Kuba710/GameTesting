using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] CharacterController characterController;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
        Vector3 moveVelocity = moveDirection * speed;
        characterController.Move(moveVelocity * Time.deltaTime);

        if (Input.GetButtonDown("Attack"))
            Debug.LogError("Attack");
        if (Input.GetButtonDown("Inventory"))
            InventoryController.Instance.ToggleActivity(); 
        if (Input.GetButtonDown("Equipment"))
            EquipmentController.Instance.ToggleActivity(); 
    }
}