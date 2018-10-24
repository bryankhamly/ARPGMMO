using System.Collections.Generic;
using Improbable.Gdk.GameObjectRepresentation;
using UnityEngine;
using Player;
using BlankProject;

[WorkerType(UnityClientConnector.WorkerType)]
public class PlayerClientVisibility : MonoBehaviour
{
    [Require] private PlayerEntity.Requirable.Reader playerEntityReader;

    private MeshRenderer myMeshRenderer;

    private void OnEnable()
    {
        myMeshRenderer = GetComponentInChildren<MeshRenderer>();
        playerEntityReader.ComponentUpdated += OnPlayerEntityComponentUpdated;
        UpdateVisibility();
    }

    private void UpdateVisibility()
    {
        myMeshRenderer.enabled = playerEntityReader.Data.IsActive;
    }

    private void OnPlayerEntityComponentUpdated(PlayerEntity.Update update)
    {
        UpdateVisibility();
    }
}
