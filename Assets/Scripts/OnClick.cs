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
        board_to_matrix.add("D4", true);
        board_to_matrix.add("E5", true);
        board_to_matrix.add("D5", false);
        board_to_matrix.add("E4", false);

    }
	
	// Update is called once per frame
    void Update()
    {
        //dicas
        //board_to_matrix.validmoves(p1turn);

        if (Input.GetMouseButtonDown(0))
            CastRay();
        

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

                //tabuleiro
                if (board_to_matrix.add(hit.collider.name, p1turn))  {

                    //coloca a peca na casa selecionada com uma distancia para visualizacao
                    Vector3 distance = new Vector3(0, 0, (float)-0.5);
                    Instantiate(Piece, hit.collider.transform.position + distance, hit.collider.transform.rotation);

                    //desabilita a opcao de jogar naquela casa novamente
                    hit.collider.enabled = false;

                    //muda o turno
                    p1turn = !p1turn;
                }
            }
        }
    }


}
