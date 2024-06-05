using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerPurchaseButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int towerCost; // Set this in the inspector for each tower's button
    public GameObject towerPrefab; // Prefab da torre que será instanciada
    private Image buttonImage;
    private CoinManager coinManager;
    private GameObject towerPreview;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        coinManager = FindObjectOfType<CoinManager>(); // Finds the CoinManager in the scene
        UpdateButtonState();
    }

    void Update()
    {
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        if (coinManager.currentCoins >= towerCost)
        {
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 1f);
        }
        else
        {
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, 0.5f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (coinManager.currentCoins >= towerCost)
        {
            Debug.Log("OnBeginDrag: Criando torre de visualização.");
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0; // Zerar a posição Z para manter no plano 2D
            towerPreview = Instantiate(towerPrefab, spawnPosition, Quaternion.identity);

            // Verifica se o prefab tem um SpriteRenderer
            SpriteRenderer sr = towerPreview.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = new Color(1, 1, 1, 0.5f); // Deixa a torre transparente
            }
        }
        else
        {
            Debug.Log("OnBeginDrag: Moedas insuficientes.");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (towerPreview != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Zerar a posição Z para manter no plano 2D
            towerPreview.transform.position = mousePosition;
            Debug.Log("OnDrag: Movendo torre de visualização.");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (towerPreview != null)
        {
            Debug.Log("OnEndDrag: Soltando torre.");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("dot"))
            {
                Debug.Log("OnEndDrag: Torre solta em um Dot válido.");
                hit.collider.GetComponent<dot>().PlaceTower(towerPrefab, towerCost);
            }
            else
            {
                Debug.Log("OnEndDrag: Torre solta em uma posição inválida.");
            }
            Destroy(towerPreview);
        }
    }
}
