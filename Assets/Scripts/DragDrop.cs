using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    //Pega o componente RectTransform
    [SerializeField] private RectTransform reactTransform;
    //Pega o canvas para localizar o mouse
    [SerializeField] private Canvas canvas;
    //Pega o canvas group para poder desativar o block raycast enquanto arrasta o objeto da UI
    [SerializeField] private CanvasGroup canvasGroup;
    //Pega o componente da Imagem para mudar a cor
    public Image image;
    //Pega o script Game
    public Game ScriptGame;
    //Variavel para salvar a cor atual do objeto da UI
    private Color corUI;
    void Awake(){
        //Atualiza a cor salva no script Game com a cor definida inicialmente
        corUI = ScriptGame.cor;
        //Muda a cor da engrenagem da UI com a cor definida no editor
        image.color = corUI;
    }
    //Roda quando ativa o objeto
    void OnEnable(){
        //Captura a cor atual do objeto na UI
        corUI = image.color;
        //Captura a cor no Script Game
        ScriptGame.cor = corUI;
    }
    //Roda quando começa a arrastar um objeto da UI
    public void OnBeginDrag(PointerEventData eventData){
        //Desativa o bloqueio de raycast
        canvasGroup.blocksRaycasts = false;
    }
    //Roda quando larga o objeto da UI que estava sendo arrastado
    public void OnEndDrag(PointerEventData eventData){
        //Reativa o block raycast para poder selecionar o objeto novamente
        canvasGroup.blocksRaycasts = true;
        //Devolve o objeto da UI de volta para o lugar inicial
        reactTransform.anchoredPosition = new Vector2(0,0);
        //Testa no que o collider está acertando e segue se for um espaço de engrenagem da cena
        if(ScriptGame.hit.collider != null && ScriptGame.hit.collider.tag == Tags.GearMap){
            //Muda a cor do filho do objeto que o collider acertou
            ScriptGame.hit.collider.transform.GetChild(0).GetComponent<SpriteRenderer>().color = corUI;
            //Ativa o filho do objeto que o collider acertou
            ScriptGame.hit.collider.transform.GetChild(0).gameObject.SetActive(true);
            //Desativa esse objeto da UI
            this.gameObject.SetActive(false);
            //Roda o script para somar a quantidade de engrenagens na cena
            ScriptGame.Girar(true);
        }
    }
    //Roda quando está arrastando um objeto da UI
    public void OnDrag(PointerEventData eventData){
        //Move o objeto da UI para a posição do mouse na tela
        reactTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Roda para atualizar o raycast e saber no que está batendo
        ScriptGame.Hit();
    }
}
