using System;
using System.Collections.Generic;
using System.Linq;
using Improbable.Gdk.Core;
using Improbable.Gdk.PlayerLifecycle;
using Improbable.Gdk.TransformSynchronization;
using Improbable.Worker;
using Improbable.Worker.Core;


public static class PlayerTemplate
{
    public const string WorkerType = "UnityGameLogic";

    private static readonly List<string> AllWorkerAttributes =
           new List<string> { BlankProject.UnityClientConnector.WorkerType, WorkerType };

    public static EntityTemplate CreatePlayerEntityTemplate(string workerId, Improbable.Vector3f position)
    {
        var clientAttribute = $"workerId:{workerId}";
        var serverAttribute = WorkerType;

        var entityBuilder = EntityBuilder.Begin()
            .AddPosition(UnityEngine.Random.Range(-8, 8), 0, 0, serverAttribute)
            .AddMetadata("Player", serverAttribute)
            .SetPersistence(false)
            .SetReadAcl(AllWorkerAttributes)
            .SetEntityAclComponentWriteAccess(serverAttribute)
            .AddComponent(Player.PlayerEntity.Component.CreateSchemaComponentData(true), serverAttribute)
            .AddPlayerLifecycleComponents(workerId, clientAttribute, serverAttribute)
            .AddTransformSynchronizationComponents(clientAttribute);

        return entityBuilder.Build();
    }
}

