using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {

    public GameObject Piece;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
        void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click, casting ray.");
            CastRay();
        }


        //TESTE - BUSCAR OBJETO NA TELA
        if ( Input.GetKeyDown(KeyCode.L) ) {
            GameObject.Find("A7").GetComponent<Renderer>().enabled = false;
        }
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hit object: " + hit.collider.name);

            if (hit.collider.tag == "piece")
                hit.collider.GetComponent<Renderer>().enabled = !hit.collider.GetComponent<Renderer>().enabled;

            if (hit.collider.tag == "board")
            {
                Vector3 distance = new Vector3(0, 0, (float)-0.5);
                Instantiate(Piece, hit.collider.transform.position+distance, hit.collider.transform.rotation);
            }
        }
    }
}
