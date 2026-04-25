# Mediator

Lightweight mediator pattern implementation for Unity.

Designed for clean architecture, decoupled systems and dependency injection workflows.

Supports synchronous commands, synchronous queries and asynchronous requests.

---

## Features

- Commands (`Send`)
- Queries (`Query`)
- Async requests (`Request`)
- Strongly typed handlers
- Easy integration with VContainer
- Lightweight API
- Useful for gameplay, UI and services

---

## Installation

Install via `Packages/manifest.json`

## Dependencies

This package requires com.tiredsiren.vcontainer.

Install both packages in `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.tiredsiren.mediator": "https://github.com/ZemMezzer/Mediator.git?path=Assets/Mediator#x.x.x",
    "com.tiredsiren.vcontainer": "https://github.com/ZemMezzer/VContainer.git?path=VContainer/Assets/VContainer#1.18.x"
  }
}
```
---

## Dependency Injection

This package is designed to integrate with VContainer.

Register mediator services and handlers inside your `LifetimeScope`.

```csharp
protected override void Configure(IContainerBuilder builder)
{
    // Required:
    // Register IMediatorResolver implementation.
    // Mediator uses resolver to locate handlers.
    builder.Register<MediatorResolver>(Lifetime.Singleton)
        .AsImplementedInterfaces();

    builder.Register<Mediator>(Lifetime.Singleton)
        .AsImplementedInterfaces();

    builder.Register<WriteBytesCommandHandler>(Lifetime.Singleton)
        .AsImplementedInterfaces();

    builder.Register<ReadBytesQueryHandler>(Lifetime.Singleton)
        .AsImplementedInterfaces();

    builder.Register<ReadBytesAsyncRequestHandler>(Lifetime.Singleton)
        .AsImplementedInterfaces();
}
```

---

# Operations

| Type | Purpose | Returns |
|---|---|---|
| Command | Execute action | Nothing |
| Query | Read data synchronously | Result |
| Request | Execute async operation | Task<TResult> |

---

## Commands

Use commands when no return value is needed.

```csharp
using TiredSiren.Mediator.Operations;

public record WriteBytes(byte[] Bytes, string Path) : ICommand;

public class WriteBytesCommandHandler : ICommandHandler<WriteBytes>
{
    public void Handle(WriteBytes command)
    {
        File.WriteAllBytes(command.Path, command.Bytes);
    }
}
```

Execute:

```csharp
_mediator.Send(new WriteBytes(bytes, path));
```

---

## Queries

Use queries for synchronous data retrieval.

```csharp
using TiredSiren.Mediator.Operations;

public record ReadBytes(string Path) : IQuery<byte[]>;

public class ReadBytesQueryHandler : IQueryHandler<ReadBytes, byte[]>
{
    public byte[] Handle(ReadBytes query)
    {
        return File.Exists(query.Path)
            ? File.ReadAllBytes(query.Path)
            : Array.Empty<byte>();
    }
}
```

Execute:

```csharp
var result = _mediator.Query<ReadBytes, byte[]>(new ReadBytes(path));
```

---

## Requests

Use requests for asynchronous operations.

```csharp
using TiredSiren.Mediator.Operations;

public record ReadBytesAsync(string Path) : IRequest<byte[]>;

public class ReadBytesAsyncRequestHandler : IRequestHandler<ReadBytesAsync, byte[]>
{
    public async Task<byte[]> Handle(ReadBytesAsync request)
    {
        return File.Exists(request.Path)
            ? await File.ReadAllBytesAsync(request.Path)
            : Array.Empty<byte>();
    }
}
```

Execute:

```csharp
var result = await _mediator.Request<ReadBytesAsync, byte[]>(
    new ReadBytesAsync(path)
);
```

---

# Why Use Mediator?

Instead of coupling systems directly:

```text
UI -> Inventory
UI -> Audio
UI -> SaveSystem
UI -> Analytics
```

Use mediator:

```text
UI -> IMediator -> Handler
```

Benefits:

- Cleaner dependencies
- Better testability
- Easier maintenance
- Better scalability

---
