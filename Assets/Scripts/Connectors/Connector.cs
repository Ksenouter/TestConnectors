using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Connectors
{
    public class Connector : MonoBehaviour
    {
    
        [SerializeField] private Color activeColor, idleColor;
        [SerializeField] private GameObject connectionLinePrefab;
    
        public enum State { Inactive, Idle, Active }
        private State _currentState;
    
        private static readonly int MaterialColor = Shader.PropertyToID("_Color");
    
        private Renderer _renderer;
        private Color _defaultColor;

        private Connector _lastActiveConnector;
        private readonly List<Connector> _connections = new List<Connector>();
    
        private bool _connectorClick;
        private float _clickTimer;

        #region MonoBehaviour CallBacks
    
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultColor = _renderer.material.GetColor(MaterialColor);
        
            SetState(State.Inactive);
            SubscribeOnEvents();
        }
    
        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }
    
        private void Update()
        {
            CheckConnectorClick();
        }
    
        private void OnMouseDown()
        {
            _connectorClick = true;
            EventsManager.ConnectorClick.Publish(this);
        
            switch (_currentState)
            {
                case State.Inactive:
                    SetState(State.Active);
                    break;
                case State.Idle:
                    CreateConnect(_lastActiveConnector);
                    SetState(State.Inactive);
                    break;
                case State.Active:
                    SetState(State.Inactive);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        #endregion
    
        #region Private Methods

        private void SubscribeOnEvents()
        {
            EventsManager.ConnectorClick.Subscribe(OnConnectorClick);
        }
    
        private void UnsubscribeFromEvents()
        {
            EventsManager.ConnectorClick.Unsubscribe(OnConnectorClick);
        }
    
        private void CheckConnectorClick()
        {
            if (!_connectorClick) return;

            _clickTimer += Time.deltaTime;
            if (_clickTimer >= MouseDragTimer)
            {
                StopClickTimer();
                OnConnectorMouseDrag();
            }
        
            if (Input.GetMouseButtonUp(0))
                StopClickTimer();
        }

        private void StopClickTimer()
        {
            _connectorClick = false;
            _clickTimer = 0f;
        }

        private void OnConnectorMouseDrag()
        {
            if (_currentState != State.Active) return;
        
            var connectionLine = Instantiate(connectionLinePrefab, transform);
            connectionLine.AddComponent<MouseConnectionLine>().SetTargets(new []{transform});
            connectionLine.AddComponent<MouseConnector>().SetParentConnector(this);
        }
    
        private void OnConnectorClick(Connector sender)
        {
            if (sender == this) return;
            if (sender == null)
            {
                SetState(State.Inactive);
                return;
            }
            SetState(sender._currentState == State.Inactive ? State.Idle : State.Inactive);
            
            if (sender._currentState == State.Inactive)
                _lastActiveConnector = sender;
        }

        private void SetColor(Color newColor)
        {
            _renderer.material.SetColor(MaterialColor, newColor);
        }

        #endregion
    
        #region Interface
    
        public void SetState(State newState)
        {
            if (_currentState == newState) return;
            _currentState = newState;

            switch (_currentState)
            {
                case State.Inactive:
                    SetColor(_defaultColor);
                    break;
                case State.Idle:
                    SetColor(idleColor);
                    break;
                case State.Active:
                    SetColor(activeColor);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    
        public void CreateConnect(Connector connector)
        {
            if (connector == null || connector == this) return;
            if (CheckConnectionExists(connector) || connector.CheckConnectionExists(this)) return;
        
            var connectionLine = Instantiate(connectionLinePrefab, transform);
            connectionLine.AddComponent<ConnectionLine>().SetTargets(new []{transform, connector.transform});
            _connections.Add(connector);
        }
    
        public bool CheckConnectionExists(Connector connector)
        {
            return _connections.Contains(connector);
        }
    
        #endregion
    
        private const float MouseDragTimer = 0.15f;
    
    }
}
