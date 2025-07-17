# Snapbox Firestore Support

**Snapbox Firestore Support** is an official extension for [Snapbox](https://github.com/CliffCalist/Snapbox) that enables Firebase Firestore as a backend for saving and loading snapshots.

It provides seamless integration with Firestore collections, with full support for metadata, type-safe snapshot conversion, and async operations.

---

## Features

- Firebase Firestore as a remote storage backend for Snapbox
- Fully asynchronous saving, loading, and deletion
- Generic snapshot conversion with zero boilerplate
- Drop-in compatible with all Snapbox systems (scene and entity handlers)
- Runtime-safe metadata validation with clear exceptions

---

## Installing

> This is an add-on and requires the base Snapbox package:  
> [https://github.com/CliffCalist/Snapbox](https://github.com/CliffCalist/Snapbox)

In your `manifest.json`:

```json
{
  "com.white-arrow.snapbox": "https://github.com/CliffCalist/Snapbox.git",
  "com.white-arrow.snapbox.firestore-support": "https://github.com/CliffCalist/SnapboxFirestoreSupport.git"
}
```

> You also need to have Firebase Firestore SDK installed and initialized in your Unity project.

---

## Usage

### Step 1: Create Metadata

To store a snapshot in a Firestore document, use `FirestoreSnapshotMetadata`:

```csharp
var metadata = new FirestoreSnapshotMetadata(
    dataName: "player_wallet",
    snapshotConverter: new FirestoreSnapshotConverter<WalletData>(),
    folderPath: FirebaseFirestore.DefaultInstance.Collection("wallets")
);

snapbox.AddMetadata(metadata);
```

- `dataName`: becomes the document ID in the collection
- `snapshotConverter`: simply use the generic version with your snapshot type
- `folderPath`: reference to the Firestore collection

> No need to implement your own converter — the generic one handles type tracking internally.

---

### Step 2: Configure Snapbox

```csharp
var snapbox = new Snapbox(
    new FirestoreSnapshotLoader(),
    new FirestoreSnapshotSaver()
);
```

You can now use the full Snapbox API as usual:

```csharp
await snapbox.SaveAllSnapshotsAsync();
var wallet = snapbox.GetSnapshot<WalletData>("player_wallet");
snapbox.SetSnapshot("player_wallet", new WalletData { Balance = 100 });
```

---

## Notes

- Only asynchronous operations are supported:  
  `SaveAsync`, `LoadAsync`, and `DeleteAsync`
- Calling `Save()` or `Delete()` will throw `NotSupportedException`
- Deleting a snapshot (setting to `null`) deletes the corresponding document
- If a document does not exist, `LoadAsync` returns `null`

---

## Example: Registering from Entity Handler

You can register Firestore metadata inside an `EntityStateHandler` like this:

```csharp
protected override void RegisterSnapshotMetadataCore()
{
    var metadata = new FirestoreSnapshotMetadata(
        "wallet",
        new FirestoreSnapshotConverter<WalletData>(),
        FirebaseFirestore.DefaultInstance.Collection("wallets")
    );

    _sceneContext.Database.AddMetadata(metadata);
}
```
