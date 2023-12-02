using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;
    public Transform segmentPrefab;
    private void Start(){
        _segments = new List<Transform>();
        _segments.Add(this.transform);
    }
    private void Update(){
        if (Input.GetKeyDown(KeyCode.RightArrow))
            _direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            _direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            _direction = Vector2.up;
    }
    private void FixedUpdate(){
        for (int i = _segments.Count - 1; i > 0; i--){
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }
    private void Grow(){
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }
    private void ResetState(){
        for (int i = 1; i < _segments.Count; i++){
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        this.transform.position = Random.insideUnitCircle * 10.0f;

    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Jedzenie"){
            Grow();
        }
        else if (other.tag == "Ściana"){
            ResetState();
        }
    }
}