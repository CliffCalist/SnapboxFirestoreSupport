using System;
using System.Reflection;
using System.Threading.Tasks;
using Firebase.Firestore;

namespace WhiteArrow.Snapbox.FirestoreSupport
{
    public class FirestoreSnapshotLoader : ISnapshotLoader
    {
        public async Task<object> LoadAsync(ISnapshotMetadata metadata)
        {
            try
            {
                if (metadata is not FirestoreSnapshotMetadata castedMetadata)
                    throw new InvalidOperationException($"Expected metadata of type {nameof(FirestoreSnapshotMetadata)}, but received {metadata.GetType()}");

                var docRef = castedMetadata.CastedFolderPath.Document(castedMetadata.SnapshotName);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return null;

                var method = typeof(DocumentSnapshot)
                    .GetMethod(
                        "ConvertTo",
                        BindingFlags.Public | BindingFlags.Instance, null,
                        Type.EmptyTypes, null
                    );

                if (method == null)
                    throw new InvalidOperationException("Firestore API changed: ConvertTo<T>() method not found.");

                var genericMethod = method.MakeGenericMethod(castedMetadata.SnapshotType);
                var result = genericMethod.Invoke(snapshot, null);

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error while loading snapshot from Firestore: {ex.Message}", ex);
            }
        }
    }
}
