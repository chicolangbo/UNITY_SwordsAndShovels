using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask clickableLayer; // layermask used to isolate raycasts against clickable layers

    public Texture2D pointer; // normal mouse pointer
    public Texture2D target; // target mouse pointer
    public Texture2D doorway; // doorway mouse pointer
    public Texture2D sword;

    public GameObject attacker;

    public UnityEvent<Vector3> OnClickEnvironment;
    public UnityEvent<GameObject> OnClickAttackable;

    void Update()
    {
        // Raycast into scene
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            IAttackable attackable = hit.collider.GetComponent<IAttackable>();

            if (attackable != null)
            {
                Cursor.SetCursor(sword, Vector2.zero, CursorMode.Auto);
            }
            else if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                Vector3 destination = hit.point;
                
                if(door)
                {
                    if(hit.transform.GetChild(0) == null)
                    {
                        Debug.Log("door destination is not set");
                    }
                    destination = hit.transform.GetChild(0).transform.position;
                }

                else if(attackable != null)
                {
                    OnClickAttackable.Invoke(hit.collider.gameObject);
                }

                else
                {

                    OnClickEnvironment.Invoke(destination);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

