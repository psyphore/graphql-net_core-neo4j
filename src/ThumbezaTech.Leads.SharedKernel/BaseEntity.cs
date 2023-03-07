﻿namespace ThumbezaTech.Leads.SharedKernel;

// This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public static List<BaseDomainEvent> Events => new();
}