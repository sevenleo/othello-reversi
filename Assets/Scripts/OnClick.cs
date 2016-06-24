using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnClick : MonoBehaviour {

    
    bool p1turn = true;
    public Material[,] Materialboard = new Material[10, 10];
    public GameObject Piece;
    public Material P1Color;
    public Material P2Color;
    public Material TipColor;

    

    // Use this for initialization
    void Start () {
        int i, j;
        board_to_matrix.add("D4", true);
        board_to_matrix.add("E5", true);
        board_to_matrix.add("D5", false);
        board_to_matrix.add("E4", false);

        
        for (i = 1; i < 9; i++)
            for (j = 1; j < 9; j++)
                Materialboard[i, j] = GameObject.Find (board_to_matrix.matrix2board(new position(i,j))).GetComponent<Renderer>().material;


    }
	
	// Update is called once per frame
    void Update()
    {
        //dicas

        
        board_to_matrix.valid_moves(p1turn).ForEach(item =>
            GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = TipColor
        );

        board_to_matrix.not_valid_moves(p1turn).ForEach(item =>
            GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = Materialboard[item.x,item.y]
        );

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
