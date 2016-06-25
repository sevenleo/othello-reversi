using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnClick : MonoBehaviour {

    
    
    public Material[,] Materialboard = new Material[10, 10];
    public GameObject Piece;
    public Material P1Color;
    public Material P2Color;
    public Material TipColor;
    public Canvas canvas;
    

    // Use this for initialization
    void Start () {

        //insere as pecas iniciais
        board_to_matrix.startwith("D4", true);
        board_to_matrix.startwith("E5", true);
        board_to_matrix.startwith("D5", false);
        board_to_matrix.startwith("E4", false);

        //guarda as cores/materiais de cada bloco do tabuleiro
        for (int i = 1; i < 9; i++)
            for (int j = 1; j < 9; j++)
                Materialboard[i, j] = GameObject.Find (board_to_matrix.matrix2board(new position(i,j))).GetComponent<Renderer>().material;


    }



    void Update()
    {
        //indica qual o jogador da vez
        if (board_to_matrix.Turn) {
            GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().fontStyle = FontStyle.Bold;
            GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().fontStyle = FontStyle.Normal;
        }
        else {
            GameObject.FindGameObjectWithTag("player1score").GetComponent<Text>().fontStyle = FontStyle.Normal;
            GameObject.FindGameObjectWithTag("player2score").GetComponent<Text>().fontStyle = FontStyle.Bold;
        }

        //colore as jogadas validas com a tipcolor
        //board_to_matrix.valid_moves().ForEach(item =>
        //    GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = TipColor
        //);

        //remove a tipcolor do tabuleiro colorindo com as cores originais do tabuleiro (preto/branco) salvas na matriz 
        board_to_matrix.not_valid_moves().ForEach(item =>
            GameObject.Find(board_to_matrix.matrix2board(item)).GetComponent<Renderer>().material = Materialboard[item.x,item.y]
        );

        //click no tabuleiro
        if (Input.GetMouseButtonDown(0))
            CastRay();
        

    }

    void CastRay()    {
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //raio que vai da camera ate a posicao do mouse
        if (Physics.Raycast(ray, out hit, 100))
        {
            //se o objeto de colisao for o tabuleiro
            if (hit.collider.tag == "board")  {

         
                //nomeia e colore a nova peca que sera criada
                Piece.name = "Piece_" + hit.collider.name;
                if (board_to_matrix.Turn) Piece.GetComponent<Renderer>().material = P1Color;
                else Piece.GetComponent<Renderer>().material = P2Color;

                //tenta adicionar a peca ao tabuleiro add() é booleana
                if (board_to_matrix.add(hit.collider.name))
                {

                    //coloca a peca na casa selecionada com uma distancia para visualizacao
                    Vector3 distance = new Vector3(0, 0, (float)-0.5);
                    Instantiate(Piece, hit.collider.transform.position + distance, hit.collider.transform.rotation);

                    //desabilita o colisor/desabilita a jogada naquela casa
                    //novos cliques nao farao efeito
                    hit.collider.enabled = false;

                }
                else Debug.Log("Jogada inválida");
            }
        }
    }


}
