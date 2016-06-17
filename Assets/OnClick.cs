using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {

    bool p1turn = true;
    public GameObject Piece;
    public Material P1Color;
    public Material P2Color;
    public Material TipColor;



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

    }

    void CastRay()    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            //Debug.DrawLine(ray.origin, hit.point);
            //Debug.Log("Hit object: " + hit.collider.name);
            //GameObject.Find("NOME").GetComponent<Renderer>().enabled = false;

            if (hit.collider.tag == "board")  {

                //nomeia e colore a nova peca que sera criaca
                Piece.name = "Piece_" + hit.collider.name;
                if (p1turn) Piece.GetComponent<Renderer>().material = P1Color;
                else Piece.GetComponent<Renderer>().material = P2Color;

                //dica
                Tips(hit.collider.name);

                //coloca a peca na casa selecionada com uma distancia para visualizacao
                Vector3 distance = new Vector3(0, 0, (float)-0.5);
                Instantiate(Piece, hit.collider.transform.position+distance, hit.collider.transform.rotation);
                
                //desabilita a opcao de jogar naquela casa novamente
                hit.collider.enabled = false;

                //muda o turno
                p1turn = !p1turn;
            }
        }
    }

    //D7 >> d8
    void Tips(string piece) {
        char row = piece[0]; //letra
        char line = piece[1]; //numero
        //line ++;
        //GameObject.Find( row + line +"").GetComponent<Renderer>().material = TipColor;
        GameObject.Find( row + "8").GetComponent<Renderer>().material = TipColor;
        GameObject.Find(row + "1").GetComponent<Renderer>().material = TipColor;
    }
}
