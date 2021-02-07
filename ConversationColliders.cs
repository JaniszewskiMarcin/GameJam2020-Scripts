using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationColliders : MonoBehaviour
{
    [SerializeField] DialogeEditor dialogeEditor;
    public int howMany = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && other == other.gameObject.GetComponent<BoxCollider2D>())
        {
            DialogeEditor.inConversation = true;
            DialogeEditor.howManySentences += howMany;
            Debug.Log(DialogeEditor.howManySentences);
            dialogeEditor.StartAgain();
            Destroy(gameObject);
        }
        
    }
}
